using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace IEC104_dotnet
{
    public class IEC104DeviceBaseAsync   /*IEC104 masters play as TCP client role*/
    {

        //==================main class=============================
        private const int BASE_TICK_MS = 50; // 50ms each timer tick
        const int RECEIVE_PERIOD_MS = 200;
        const int TIMEOUT_PERIOD_MS = 1000;

        public CancellationTokenSource cts;
        public IEC104ProtocolAsync iec104protocol;
        public bool isRuning = false;


        public IEC104DeviceBaseAsync(string remoteIP, int remotePort, int commonAddr)
        {
            iec104protocol = new IEC104ProtocolAsync(remoteIP, remotePort, commonAddr);
            isRuning = false;
            // reset_base_timer_para();
            // initWorkerThread();
        }

        // class destructor
        ~IEC104DeviceBaseAsync()
        {
             //destroyWorkerThread();
            // stop_base_timer();

        }

        private async void receiveLoop(CancellationToken token, int period_ms)
        {

            while (true)
            {
                // Poll on this property if you have to do 
                // other cleanup before throwing. 
                if (token.IsCancellationRequested)
                {
                   // token.ThrowIfCancellationRequested();
                    iec104protocol.onCommunicationLog(iec104protocol, true, "---> Finish receive Task in Checking time");

                    return;
                }
                try
                {
                    await Task.Delay(period_ms, token);
                    iec104protocol.receive_hdl();
                }
                catch (OperationCanceledException)
                {
                    iec104protocol.onCommunicationLog(iec104protocol, true, "---> Finish receive Task in Delay time");

                    return;
                }
            }

        }

        private async void timeoutLoop(CancellationToken token, int period_ms)
        {

            while (true)
            {
                // Poll on this property if you have to do 
                // other cleanup before throwing. 
                if (token.IsCancellationRequested)
                {
                    //token.ThrowIfCancellationRequested();
                    iec104protocol.onCommunicationLog(iec104protocol, true, "---> Finish timeout Task in checking time");

                    return;
                }
                // Wait 1 second before trying the test again
                try
                {
                    await Task.Delay(period_ms, token);
                    iec104protocol.timeout_hdl();
                }
                catch (OperationCanceledException)
                {
                    iec104protocol.onCommunicationLog(iec104protocol, true, "---> Finish timeout Task in Delay time");
                    return;
                }
               
            }

        }

   

        public int start(int try_cnt = 5)
        {
            if (isRuning == false) {
                int ret =  iec104protocol.connect(try_cnt);
                if (ret < 0) return ret;
                startTask();
                return ret;
            }
            return -1;
        }



    
        public void setup_callback(IEC104ProtocolAsync.OnIncommingData onIncommingData,
                                    IEC104ProtocolAsync.OnCommunicationLog onCommunicationLog,
                                    IEC104ProtocolAsync.OnErrorConnection onErrorConnection)
        {
            iec104protocol.onCommunicationLog += onCommunicationLog;
            iec104protocol.onErrorConnection += onErrorConnection;
            iec104protocol.onIncommingData += onIncommingData;
        }



        public void stop()
        {
            if(isRuning) cts.Cancel();
            isRuning = false;
            Thread.Sleep(500);
            iec104protocol.disconnect();
        }



        // ===================for thread handler Block Start=============================
        /*
         * 
         */


        private void startTask(){
            isRuning = true;
            cts = new CancellationTokenSource();
            var token = cts.Token;

            var receiveTask = Task.Factory.StartNew(
                () =>
                {
                    receiveLoop(token, RECEIVE_PERIOD_MS);
                },
                token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
            );

            var timeoutTask = Task.Factory.StartNew(
               () =>
               {
                   timeoutLoop(token, TIMEOUT_PERIOD_MS);
               },
               token,
               TaskCreationOptions.LongRunning,
               TaskScheduler.Default
           );
            List<Task> listTask = new List<Task>();
            listTask.Add(receiveTask);
            listTask.Add(timeoutTask);


            //handler as anonymus function by delagate()
            try
            {
                // wait here for cancel event
                // await await receiveTask;
                Task.WaitAll(listTask.ToArray());
                //  receiveTask.Wait();
                // if the code run to here mean the task is complete by cancel token
              //  cts.Dispose();
               // isRuning = false;
               // iec104protocol.disconnect();
               // destroyWorkerThread();

            }
            catch (AggregateException ae)
            {
                // catch inner exception 
            }
            catch (Exception crap)
            {
                // catch something else
            }
        
        }
    
        
        /*
        private Thread workerThread;

        private void startWorkerThread()
        {
            
             //https://openplanning.net/10553/csharp-multithreading
             // Thread require the handle function must be avaiable before start. So there are only 3 ways to create the handler function for thread task:
             //       1. handler function as static function ---> always availabe
             //       2. handler function as a function of available instance. ==> must call new class instance to make class instance avaiilable before using the function of this instance as thread handler
             //       3. handler as anonymus function by delagate()
             

            workerThread = new Thread(
                async delegate()
                {

                    isRuning = true;
                    cts = new CancellationTokenSource();
                    var token = cts.Token;
                    
                    var receiveTask = Task.Factory.StartNew(
                        () =>
                        {
                            receiveLoop(token, RECEIVE_PERIOD_MS);
                        },
                        token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Default
                    );

                    var timeoutTask = Task.Factory.StartNew(
                       () =>
                       {
                           timeoutLoop(token, TIMEOUT_PERIOD_MS);
                       },
                       token,
                       TaskCreationOptions.LongRunning,
                       TaskScheduler.Default
                   );

                    List<Task> listTask = new List<Task>();
                    listTask.Add(receiveTask);
                    listTask.Add(timeoutTask);


                    //handler as anonymus function by delagate()
                    try
                    {
                        // wait here for cancel event
                       // await await receiveTask;
                        Task.WaitAll(listTask.ToArray());
                      //  receiveTask.Wait();
                        // if the code run to here mean the task is complete by cancel token
                        cts.Dispose();
                        isRuning = false;
                        iec104protocol.disconnect();
                        destroyWorkerThread();

                    }
                    catch (AggregateException ae)
                    {
                        // catch inner exception 
                    }
                    catch (Exception crap)
                    {
                        // catch something else
                    }


                }
            );
            // this thread will be off along side with main
            workerThread.IsBackground = true;
            // start worker
            workerThread.Start();

        }


        private void destroyWorkerThread()
        {
            
            // Once you have aborted your thread, you cannot start it again.
            // But your actual problem is that you are aborting your thread. You should never use Thread.Abort().
            //When you are using this Abort , you need to reinit the Thread by calling  initWorkerThread()
            // If your thread should be paused and continued several times,
            // you should consider using other mechanisms (like AutoResetEvent or simple volatile flag on and off to resume).
            // call this function pauseWorkerThread.

             

            workerThread.Abort();
        }

    */
    
    }
    

}

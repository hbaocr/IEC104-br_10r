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
    public class IEC104DeviceBase   /*IEC104 masters play as TCP client role*/
    {

        //==================main class=============================
        private const int BASE_TICK_MS = 50; // 50ms each timer tick
        const int RECEIVE_PERIOD_MS = 200;
        const int TIMEOUT_PERIOD = 1000;
        

        public IEC104Protocol iec104protocol;

        public IEC104DeviceBase(string remoteIP, int remotePort, int commonAddr)
        {
            iec104protocol = new IEC104Protocol(remoteIP, remotePort, commonAddr);
            reset_base_timer_para();
            initWorkerThread();
        }

        // class destructor
        ~IEC104DeviceBase()
        {
            destroyWorkerThread();
            stop_base_timer();

        }

        public void setup_callback( IEC104Protocol.OnIncommingData onIncommingData,
                                    IEC104Protocol.OnCommunicationLog onCommunicationLog,
                                    IEC104Protocol.OnErrorConnection onErrorConnection)
        {
            iec104protocol.onCommunicationLog += onCommunicationLog;
            iec104protocol.onErrorConnection += onErrorConnection;
            iec104protocol.onIncommingData += onIncommingData;
        }

        public int start(int try_cnt=5)
        {
 
            reset_base_timer_para();
            int ret = iec104protocol.connect(try_cnt);
            if (ret < 0) return ret;
            start_base_timer(BASE_TICK_MS);
            startWorkerThread();
            return ret;

        }

        public void stop()
        {
            stop_base_timer();
            pauseWorkerThread();
            iec104protocol.disconnect();
            reset_base_timer_para();

        }



        // ===================for thread handler Block Start=============================
        /*
         * 
         */

        private Thread workerThread;
        private volatile bool _isWorkerThreadStart = false;
        
        private void initWorkerThread()
        {
            /*
             * https://openplanning.net/10553/csharp-multithreading
              Thread require the handle function must be avaiable before start. So there are only 3 ways to create the handler function for thread task:
                    1. handler function as static function ---> always availabe
                    2. handler function as a function of available instance. ==> must call new class instance to make class instance avaiilable before using the function of this instance as thread handler
                    3. handler as anonymus function by delagate()
             */

            workerThread = new Thread(
                delegate() { //handler as anonymus function by delagate()
                    while (true)
                    {
                       
                        try
                        {
                            Thread.Sleep(50);

                            if (_isWorkerThreadStart == false) continue; // skip

                            if(iec104protocol != null) // if already init the protocal
                            {
                                if (isCheckReceiveFlag)
                                {
                                    iec104protocol.receive_hdl();
                                    isCheckReceiveFlag = false;

                                }
                                if (isCheckTimeoutFlag)
                                {
                                    iec104protocol.timeout_hdl();
                                    isCheckTimeoutFlag = false;
                                }

                            }

                        }
                        catch(Exception e)
                        {

                        }
                        
                    }

                }
            );

            // start worker
            workerThread.Start();
            pauseWorkerThread();

        }


        private void startWorkerThread()
        {
           
            _isWorkerThreadStart = true;
        }
        private void pauseWorkerThread()
        {
            // this will use global volatile bool flag to paus and resuse
            _isWorkerThreadStart = false;
        }

        private void destroyWorkerThread()
        {
            /*
             Once you have aborted your thread, you cannot start it again.
             But your actual problem is that you are aborting your thread. You should never use Thread.Abort().
             When you are using this Abort , you need to reinit the Thread by calling  initWorkerThread()
             If your thread should be paused and continued several times,
             you should consider using other mechanisms (like AutoResetEvent or simple volatile flag on and off to resume).
             call this function pauseWorkerThread.

             */
            pauseWorkerThread();
            workerThread.Abort();
        }


        // =================== thread handler Block End=============================






        // ===================for timer base start=============================
        private System.Timers.Timer base_timer;
        private int base_ms = 100;
        // if your operation in timer loop can take more time then interval_ms,if this flag is true
        // next timer tick will be disable until operation finish in loop, then it enable again
        public volatile bool is_timer_loop_wait_task = true;
        /* volatile : keyword
         * https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2008/7a2f3ay4(v=vs.90)?redirectedfrom=MSDN
         * The volatile keyword alerts the compiler that multiple threads will access the is_timer_loop_wait_task data member, 
         * and therefore it should not make any optimization assumptions about the state of this member.
         * For more information, see volatile (C# Reference).
         */

        // check timeout to send cmd
        private volatile bool isCheckTimeoutFlag = false;
        // check isReceiveFlag to parse incoming data data
        private volatile bool isCheckReceiveFlag = false;


        private int recvDataTick = 0;
        private int timeoutTick = 0;


        private void base_period_task(object sender, System.Timers.ElapsedEventArgs e)
        {
          
            recvDataTick++;
            timeoutTick++;

           
            if(is_timer_loop_wait_task) base_timer.AutoReset = false; // disable timer to start new period


            //oÏnPeriodTask(iec104master_helper, eslap_time);
            //heavy task here.To make sure ==> set flag here, and another thread will check these to react these flag
            if ((recvDataTick * base_ms) > RECEIVE_PERIOD_MS)
            {
                isCheckReceiveFlag = true; // enable flag --> trigger recv parser
                recvDataTick = 0;

            }

            if ((timeoutTick * base_ms) > TIMEOUT_PERIOD)
            {
                isCheckTimeoutFlag = true;// enable flag --> trigger timeout sender
                timeoutTick = 0;

            }


            if (is_timer_loop_wait_task) base_timer.Enabled = true;//re-enable timer to start new period



        }

        public long get_total_ms_now()
        {
            return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
        private void reset_base_timer_para()
        {
            recvDataTick = 0;
            timeoutTick = 0;
            // check timeout to send cmd
            isCheckTimeoutFlag = false;
            // check isReceiveFlag to parse incoming data data
            isCheckReceiveFlag = false;
        }
        private void start_base_timer(int period_ms_eslap)
        {

            reset_base_timer_para();
            base_timer = new System.Timers.Timer();
            this.base_ms = period_ms_eslap;
            base_timer.Interval = this.base_ms;
            base_timer.Elapsed += new System.Timers.ElapsedEventHandler(base_period_task);
            base_timer.Enabled = true; // force start timert
        }

        private void stop_base_timer()
        {
            base_timer.Enabled = false; // force start timert
            reset_base_timer_para();

        }

        // ===================for timer base end block=============================

    }

    

}

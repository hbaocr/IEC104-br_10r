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
    public class IEC104Device   /*IEC104 masters play as TCP client role*/
    {

        //==================main class=============================
        
        public IEC104ProtocolHdl IEC104ProtocolHdl;

        public IEC104Device(string remoteIP, int remotePort, int commonAddr)
        {
            IEC104ProtocolHdl = new IEC104ProtocolHdl(remoteIP, remotePort, commonAddr);
            initWorkerThread();
        }

        // class destructor
        ~IEC104Device()
        {
            destroyWorkerThread();

        }

        public void setup_callback( IEC104ProtocolHdl.OnIncommingData onIncommingData,
                                    IEC104ProtocolHdl.OnCommunicationLog onCommunicationLog,
                                    IEC104ProtocolHdl.OnErrorConnection onErrorConnection)
        {
            IEC104ProtocolHdl.onCommunicationLog += onCommunicationLog;
            IEC104ProtocolHdl.onErrorConnection += onErrorConnection;
            IEC104ProtocolHdl.onIncommingData += onIncommingData;
        }

        public int start(int try_cnt=5)
        {
 
            int ret = IEC104ProtocolHdl.connect(try_cnt);
            if (ret < 0) return ret;
            startWorkerThread();
            return ret;

        }

        public void stop()
        {
          
            //pauseWorkerThread();
            destroyWorkerThread();
            IEC104ProtocolHdl.disconnect();
         

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
                            Thread.Sleep(1000);

                            if (_isWorkerThreadStart == false) continue; // skip

                            if(IEC104ProtocolHdl != null) // if already init the protocal
                            {
                                IEC104ProtocolHdl.iec104_1sec_tick_hdl();
                               
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




    }

    

}

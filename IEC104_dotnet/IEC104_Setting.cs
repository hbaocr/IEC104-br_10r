using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    class IEC104_Setting
    {
        /*Connection parameter*/
        /*connnection setting*/
        public int ca_size = 2;
        /*
            https://www.fit.vut.cz/research/publication-file/11570/TR-IEC104.pdf
            In some document 2 byte COT : include
                1 LS byte : the COT itself 
                1 MS byte : Originator address(ORG) (local Addr) default =0
            The ORG is usefull when there are multi master on the line. THis will distinct these master.
            Usually on 1 line ( or 1 IP), there are only one IEC104 master, so this ORG is not used and usually set to 0
        */
        public int cot_size = 2;
        public int ioa_size = 3;

        /* http://www.thefullwiki.org/IEC_60870-5-104
         * Control field data of IEC104 contains various types of formats /mechanisms for effective
         * handling of network data synchronization
                1. I Format – It is used to perform numbered information transfer. It contains send-sequence number 
         *     and receive-sequence number. The transmitter station increases send-sequence number when it sends any 
         *     data and receiver increases receive-sequence number when it receives any data. The sending station
         *     has to hold the send APDUs in the buffer until it receives back the send sequence numbers as the 
         *     receive sequence number from destination station.
         *     
         *     2. S Format – It is used to perform numbered supervisory functions. In any cases where the data 
         *     transfer is only in a single direction, S-format APDUs has to be send in other direction before 
         *     timeout (t2), buffer overflow or when it has crossed maximum number of allowed I format APDUs
         *     without acknowledgement (w).
         *     
         *     3. U Format – It is used to perform unnumbered control functions. This is used for activation 
         *     and confirmation mechanisms of STARTDT (start data transfer) & STOPDT (stop data transfer) & 
         *     TESTFR (test APDU).
         *    
         *     4. Test Procedure – Open but unused connections must be tested periodically (when it has 
         *     crossed ‘t3’ after the last message) by sending TESTFR frames, which need to be acknowledged,
         *     by the destination station. The connection needs to be closed when there is no reply for the 
         *     test message after timeout (t1) or when there are more numbers of I-format APDUs than the 
         *     specified ‘k’.more information. t3 thoi gian giua 2 lan send S frame
         *
         */
      
        /*Maximum difference ( k ) between the received sequence number ( RSN ) 
         * and the last acknowledged RSN.The transmitter stops the transmission at k unacknowledged I frames*/
        public int k_para = 12;

        /*This parameter ( w ) indicates the number of received I frames after the RSN 
         * will be acknowledged at latest from APP with a S frame ( A C K R S N ).
         */
        public int w_para = 8;

        /*t0 :Network connection establishment timeout Timeout for the establishment of 
         * the connection with the server
         */
        public int t0_sec = 15;//30;

        //t2<t1<t3
        /*
         * t1 :Response timeout .This parameter defines the time in seconds that APP 
         * waits maximum for an acknowledge from the server
         */
       // public int t1_sec = 8;//15;

        /*t2 in case of no data message: A S-format frame ( ACKRSN ) will be sent at 
         * the latest after this time starting from the last received telegram from the server*/
      //  public int t2_sec = 5;//10;

        /*t3 :Used for sending test frames.An U-format frame ( TESTFR ) will be sent at the 
         * latest after this time starting from the last received telegram from the server
         */
       // public int t3_sec = 10;//20;

        public int t3_testfr = 10;
        public int t2_supervisory = 8;
        public int t1_startdtact = 6;

        public int gi_period = 5 * 60 + 30; // minimum time for request between GI's
        public int gi_retry_time = 45; // wait time to retry when requested a GI and not responded

        public IEC104_Setting(int ca_size, int cot_size, int ioa_size)
        {
            this.ca_size = ca_size;
            this.cot_size = cot_size;
            this.ioa_size = ioa_size;        
        }
        public IEC104_Setting(int ca_size, int cot_size, int ioa_size, int w,int k,int t0,int t1,int t2,int t3):this(ca_size,cot_size,ioa_size) {
            this.w_para = w;
            this.k_para = k;
            this.t0_sec = t0;
            this.t1_startdtact = t1;
            this.t2_supervisory = t2;
            this.t3_testfr = t3;
        }


    }
}

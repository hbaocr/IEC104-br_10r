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
    public class IEC104ProtocolHdl   /*IEC104 masters play as TCP client role*/
    {
        public delegate void OnIncommingData(IEC104ProtocolHdl sender);
        public delegate void OnErrorConnection(IEC104ProtocolHdl sender, string message);
        public delegate void OnCommunicationLog(IEC104ProtocolHdl sender, bool is_tx_frame, string hexframe);

        public enum RequestStage
        {
            WAIT_STAGE = 0,
            HAND_SHAKE_STAGE = 1,
            SEND_S_STAGE = 2,
            SEND_TESTFR_STAGE = 3,
            SEND_INTERROGATION_STAGE = 4
        };

        public List<RespValue> listNewIncommingData = new List<RespValue>();

        public OnIncommingData onIncommingData;
        public OnCommunicationLog onCommunicationLog;
        public OnErrorConnection onErrorConnection;



        public const int ERR = -1;
        public const int OK = 0;
        private IPAddress remoteIP;
        private int remotePort;
        public TcpClient client;

        private IEC104_Setting remoteSetting = new IEC104_Setting(2, 2, 3);


        private int remoteCommonAddress = 5;

        //private Socket tcpSocket = null;
        // private NetworkStream tcpStream = null;
        // private BinaryReader tcpReaderStream = null;
        //  private BinaryWriter tcpWriterStream = null;


        //  private TimerPeriodTask iec104TimeOutHDL;
        //  private TimerPeriodTask iec104ReceiveHDL;


        private bool isConnect = false;

        private bool TxOk = false; // ready to transmit state (STARTDTCON received)
        private int tout_startdtact = 0; // timeout control
        private int tout_supervisory = 0;  // countdown to send supervisory window control
        private int tout_gi = 0; // countdown to send general interrogation
        private int tout_testfr = 0; // countdown to send test frame

        int VS;  // sender packet control counter
        int VR;  // receiver packet control counter
        /*This parameter ( w ) indicates the number of received I frames after the RSN 
         * will be acknowledged at latest from APP with a S frame ( A C K R S N ).
         */
        // private int w_Iframe_cnt = 0;

        /*Maximum difference ( k ) between the received sequence number ( RSN ) 
         * and the last acknowledged RSN.The transmitter stops the transmission at k unacknowledged I frames*/
        // private int k_Iframe_cnt = 0;

        /* S-format APDUs has to be send in other direction before timeout (t2), buffer overflow or
         * when it has crossed maximum number of allowed I format APDUs without acknowledgement (w)*/
        // private int sframe_cnt = 0;
        /* Open but unused connections must be tested periodically (when it has crossed ‘t3’ after the last message) 
         * by sending TESTFR frames, which need to be acknowledged, by the destination station         */

        /*
         * The connection needs to be closed when there is no reply for the test message after timeout (t1) or when
         * there are more numbers of I-format APDUs than the specified ‘k’.more information
         */
        //  private int snd_frm_cnt = 0;

        // private int tick_sec = 0;
        // private int t_waitcmd_tick_sec = 0;
        private bool is_recv_validframe = false;
        // private int t2_cnt = 0;
        //private int t3_cnt = 0;
        //private int t1_cnt = 0;
        // private int t4_cnt = 0;//force to update
        /************************************************************         
         result store
        ************************************************************/
        private byte[] recvBuff;// = new byte[255];
        private IEC104_ASDU_Para.ApciType apciType_last_resp;//response apci
        private IEC104_ASDU_Para.ApciType apciType_resp;//response apci
        private IEC104_ASDU_Para.ApciType apciType_req;//request apci



        private byte recv_len = 0;
        private int receiveSeqNum = 0;
        private int sendSeqNum = 0;

        private IEC104_ASDU_Para.IOA_TypeID ioa_type_id;
        private bool IsSequenceOfElements = false;
        private int sequenceLength = 0; //number of IOA in recv buff
        private IEC104_ASDU_Para.COT_Id causeOfTransmission;

        //C_RP_NA_1 : reset process command
        private bool isEnableSendResetProcessCMD = false;

        //Counter interrogation command C_CI_NA_1
        private bool isEnableSendCICmd = false;

        // read IOA cmd
        private bool isEnableSendReadIOACmd = false;
        private int readcmdIOA = 0;

        //Single CMD test
        private bool isEnableSendSingleCmdTest = false;
        private int singleCMDTestOnOffState = 0;
        private int singleCmdTestTimeoutSec = 2; //wait for confirm --->timeout
        private int singleCmdTestIOA = 0;
        private bool isSingleCMDExe = false;

        //Double CMD test
        private bool isEnableSendDoubleCmdTest = false;
        private int doubleCMDTestOnOffState = 0;
        private int doubleCmdTestTimeoutSec = 2; //wait for confirm --->timeout
        private int doubleCmdTestIOA = 0;
        private bool isDoubleCMDExe = false;



        //Single CMD
        private bool isSingleCmdConfirmResp = false;
        private bool isSingleCmdConfirmTerminate = false;//finsih select/excute proceaa
        private int singleCMDOnOffState = 0;
        private int singleCmdTimeoutSec = 2; //wait for confirm --->timeout
        private int singleCmdIOA;

        private bool isEnableSendSingleCmd = false;

        //Double Cmd
        private bool isDoubleCmdConfirmResp = false;
        private bool isDoubleCmdConfirmTerminate = false;//finsih select/excute proceaa
        private int doubleCMDOnOffState = 0;
        private int doubleCmdTimeoutSec = 2; //wait for confirm --->timeout
        private int doubleCmdIOA;

        private bool isEnableSendDoubleCmd = false;



        private bool test;
        private bool negativeConfirm;
        private int recvCommonAdrr;
        private int oa;

        //support value;
        private IEC104_ASDU_Para.CauseOfTransmission cause_of_transmission_val;

        private IEC104_ASDU_Para.M_SP_NA_1_SinglePointWithoutTime M_SP_NA_1_val;
        private IEC104_ASDU_Para.M_SP_TA_1_SinglePointWithTime24 M_SP_TA_1_val;
        private IEC104_ASDU_Para.M_SP_TB_1_SinglePointWithTime56 M_SP_TB_1_val;

        private IEC104_ASDU_Para.M_DP_NA_1_DoublePointWithoutTime M_DP_NA_1_val;
        private IEC104_ASDU_Para.M_DP_TA_1_DoublePointWithTime24 M_DP_TA_1_val;
        private IEC104_ASDU_Para.M_DP_TB_1_DoublePointWithTime56 M_DP_TB_1_val;

        private IEC104_ASDU_Para.M_ST_NA_1_StepPositionInformationWithoutTime M_ST_NA_1_val;
        private IEC104_ASDU_Para.M_ST_TA_1_StepPositionInformationWithTime24 M_ST_TA_1_val;
        private IEC104_ASDU_Para.M_ST_TB_1_StepPositionInformationWithTime56 M_ST_TB_1_val;

        private IEC104_ASDU_Para.M_ME_NA_1_MeasuredNormalizedValue M_ME_NA_1_val;
        private IEC104_ASDU_Para.M_ME_TA_1_MeasuredNormalizedValueWithTime24 M_ME_TA_1_val;
        private IEC104_ASDU_Para.M_ME_TD_1_MeasuredNormalizedValueWithTime56 M_ME_TD_1_val;

        private IEC104_ASDU_Para.M_ME_NB_1_MeasuredScaledValue M_ME_NB_1_val;
        private IEC104_ASDU_Para.M_ME_TB_1_MeasuredScaledValueWithTime24 M_ME_TB_1_val;
        private IEC104_ASDU_Para.M_ME_TE_1_MeasuredScaledValueWithTime56 M_ME_TE_1_val;

        private IEC104_ASDU_Para.M_ME_NC_1_MeasuredShortFloatingPointValue M_ME_NC_1_val;
        private IEC104_ASDU_Para.M_ME_TC_1_MeasuredShortFloatingPointWithTime24 M_ME_TC_1_val;
        private IEC104_ASDU_Para.M_ME_TF_1_MeasuredShortFloatingPointWithTime56 M_ME_TF_1_val;

        private IEC104_ASDU_Para.M_IT_NA_1_IntegratedTotalsValue M_IT_NA_1_val;
        private IEC104_ASDU_Para.M_IT_TB_1_IntegratedTotalsValuetWithTime24 M_IT_TB_1_val;
        private IEC104_ASDU_Para.M_IT_TA_1_IntegratedTotalsValueWithTime56 M_IT_TA_1_val;

        private IEC104_ASDU_Para.C_SC_NA_1_SinglePointCMD C_SC_NA_1_val;

        //==========================================================



        public IEC104ProtocolHdl(string remoteIP, int commonAddr)
        {
            try
            {
                this.remoteIP = IPAddress.Parse(remoteIP);
                this.remotePort = 2404;
                this.remoteCommonAddress = commonAddr;
                //for handler
                // int IEC104_INTERVAL_MS = 200;
                reset_para();
                //  iec104ReceiveHDL = new TimerPeriodTask(this, IEC104_INTERVAL_MS);//IEC104_INTERVAL_MS to check 1 period
                // iec104ReceiveHDL.onPeriodTask += iec104_receive_hdl;//iec104_periodhdl will be call each IEC104_INTERVAL_MS

                // iec104TimeOutHDL = new TimerPeriodTask(this, 1000);//1sec to check 1 period
                // iec104TimeOutHDL.onPeriodTask += iec104_timeout_hdl;//iec104_timeout_hdl will be call each 1sec
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public IEC104ProtocolHdl(string remoteIP, int remotePort, int commonAddr)
            : this(remoteIP, commonAddr)
        {

            try
            {
                this.remotePort = remotePort;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public void setupRemoteDevPara(int ca, int ca_size, int cot_size, int ioa_size)
        {
            this.remoteCommonAddress = ca;
            this.remoteSetting = new IEC104_Setting(ca_size, cot_size, ioa_size);
        }
        public void setRemoteCommonAddress(int ca)
        {
            this.remoteCommonAddress = ca;
        }


        /*=====================Request U frame start communication activation========================== */
        private Object lock_tcp_send = new Object();
        private int tcpSendBuff(byte[] buff, int len)
        {
            if (client == null) return -1;
            lock (lock_tcp_send) //make sure this finish 
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    if (stream != null)
                    {
                        stream.Write(buff, 0, len);
                        return len;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    onErrorConnection(this, "Connection Error: " + e.Message.ToString());
                    return -1;
                }
            }
        }

        public int sendStartDtAct()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.STARTDT_ACT, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.STARTDT_ACT;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "StartDT_Act handshake Activation : " + s);
            // tout_startdtact = remoteSetting.t1_startdtact;
            return tcpSendBuff(buff, len);
        }

        public int sendStartDtCon()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.STARTDT_CON, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.STARTDT_CON;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "StartDT_Con handshake Confirmation : " + s);

            return tcpSendBuff(buff, len);
        }


        /*=====================Request U frame stop communication activation========================== */

        public int sendStopDtAct()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.STOPDT_ACT, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.STOPDT_ACT;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "StopAct Handshake Activation: " + s);
            return tcpSendBuff(buff, len);
        }
        public int sendStopDtCon()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.STOPDT_CON, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.STOPDT_CON;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "StopAct Handshake Confirm: " + s);

            return tcpSendBuff(buff, len);
        }
        /*=====================Request U frame Test activation========================== */

        public int sendTestfrAct()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.TESTFR_ACT, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.TESTFR_ACT;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Test activation Frame : " + s);
            return tcpSendBuff(buff, len);
        }

        public int sendTestfrCon()
        {
            byte[] buff = new byte[16];
            var apdu = new IEC104_APDU(remoteSetting, 0, 0, IEC104_APDU.ApciType.TESTFR_CON, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.TESTFR_CON;
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Test Confirm Frame : " + s);
            return tcpSendBuff(buff, len);
        }


        /*=====================Request Sframe========================== */

        public int sendSupervisory()
        {
            byte[] buff = new byte[16];
            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768); /*sendSeqNum : number of I frame transmit from server of recv client*/
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.S_FORMAT, null);
            int len = apdu.byte_encode(buff);
            apciType_req = IEC104_ASDU_Para.ApciType.S_FORMAT;

            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Supervisory frame : " + s);
            return tcpSendBuff(buff, len);
        }

        /*=====================Request interrogation========================== */
        //102: C_RD_NA_1
        public int sendReadIOACommand(int ioa)
        {
            byte[] buff = new byte[300];
            // this cmd only accept COT = REQUEST (page 280 Practical modem scada protocol DNP3)
            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.REQUEST, 0);

            IEC104_ASDU_Para.C_RD_NA_1_ReadIOACmd IOcmd = new IEC104_ASDU_Para.C_RD_NA_1_ReadIOACmd(remoteSetting, ioa);
            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_RD_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Send C_RD_NA_1 readIOA(" + ioa + ") cmd : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;
            tout_gi = remoteSetting.gi_retry_time;
            return tcpSendBuff(buff, len);
        }



        public int sendGeneralCounterInterrogation()
        {
            IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement.FRZ_code frz = IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement.FRZ_code.Read_Without_Freeze_Or_Reset;
            IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement.RQT_code rqt = IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement.RQT_code.GLOBAL_COUNTER_INTERROGATION;
            byte[] buff = new byte[300];
            int ioa = 0;// only accept IOA=0 page 268 practical modem scada protocol dnp3
            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.ACTIVATION, 0);
            IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement qcc = new IEC104_ASDU_Para.QCC_QualifierOfCounterInterrogationElement(frz, rqt);

            //hbaocr
            // IEC104_ASDU_Para.C_IC_NA_1_InterrogationCMD IOcmd = new IEC104_ASDU_Para.C_IC_NA_1_InterrogationCMD(remoteSetting, ioa,
            //     new IEC104_ASDU_Para.QOI_QualifierOfInterrogationElement((byte)IEC104_ASDU_Para.QOI_QualifierOfInterrogationElement.QOI_VALUE.GLOBAL_STATION_INTERROGATION));
            IEC104_ASDU_Para.C_CI_NA_1_CounterInterrogationCMD IOcmd = new IEC104_ASDU_Para.C_CI_NA_1_CounterInterrogationCMD(remoteSetting, ioa, qcc);
            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_IC_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Send counter Iterrogation frame : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;
            tout_gi = remoteSetting.gi_retry_time;
            return tcpSendBuff(buff, len);
        }


        /*9.1.1 Station initialization General procedure : page 287 Practical modem Scada protocol
                Station initialization is required when a station is first powered up, or after a reset. Its purpose is to ensure the orderly commencement of monitoring and control operations. The procedure followed is basically to reset the link layer first, re-establish link commun- ications, and then commence application level services. When the initialization process of a controlled station is completed, it will send an end of initialization ASDU to the controlling station so that control and monitoring functions can begin. The procedure involves both application and link level functions.
                Initialization of controlling station:
                        • Link layer establishes links with controlled stations
                        • Controlling station sends C_EI (end of initialization) ASDU to the active
                        controlled stations to tell them that they may commence sending process
                        information
                        • Controlled stations may commence sending process data if available
                        • Controlling station performs a general interrogation
                        • Controlling station may perform clock synchronization
                Initialization of controlled station:
                        • Link layer establishes communication with controlling station
                        • Once the application layer is ready, controlled station may send a M_EI (end
                        of initialization) ASDU to the controlling station
                        • The controlled station now responds to general interrogation or other
                        commands from the controlling station
               
         * Controlling Station Functions
                Reset remote station request
         
         
         */
        ////105: C_SC_NA_1 reset process command
        public int sendResetProcessCMD()
        {
            byte[] buff = new byte[300];
            int ioa = 0;
            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.ACTIVATION, 0);
            IEC104_ASDU_Para.QRP_QualifierOfResetProcessElement qrp = new IEC104_ASDU_Para.QRP_QualifierOfResetProcessElement((byte)IEC104_ASDU_Para.QRP_QualifierOfResetProcessElement.QRP_VALUE.GENERAL_RESET_OF_PROCESS);
            IEC104_ASDU_Para.C_RP_NA_1_ResetCMD IOcmd = new IEC104_ASDU_Para.C_RP_NA_1_ResetCMD(remoteSetting, ioa, qrp);

            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_RP_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Send Reset C_RP frame : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;
            tout_gi = remoteSetting.gi_retry_time;
            return tcpSendBuff(buff, len);
        }



        public int sendInterrogation()
        {
            byte[] buff = new byte[300];
            int ioa = 0;
            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.ACTIVATION, 0);

            IEC104_ASDU_Para.C_IC_NA_1_InterrogationCMD IOcmd = new IEC104_ASDU_Para.C_IC_NA_1_InterrogationCMD(remoteSetting, ioa,
                new IEC104_ASDU_Para.QOI_QualifierOfInterrogationElement((byte)IEC104_ASDU_Para.QOI_QualifierOfInterrogationElement.QOI_VALUE.GLOBAL_STATION_INTERROGATION));

            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_IC_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Send Iterrogation frame : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;
            //tout_gi = remoteSetting.gi_retry_time;
            return tcpSendBuff(buff, len);
        }

        /*
         First send select then send execute --> two phase cmd
         */
        public const int SELECT_CMD_T = 1;
        public const int EXECUTE_CMD_T = 0;
        public const int CMD_WAIT_RESP = 2;
        public const int NO_CMD = 3;
        public const int ON_STATE = 1;
        public const int OFF_STATE = 0;






        //45: C_SC_NA_1
        public int sendSingleCmd(int ioa, int select_execute_cmd, int on_off_state)
        {
            byte[] buff = new byte[300];
            IEC104_ASDU_Para.SCO_SingleCommandElement.SE_code select_execute;
            IEC104_ASDU_Para.SCO_SingleCommandElement.SCS_code on_off;
            // int ioa = 0;
            select_execute = (select_execute_cmd == EXECUTE_CMD_T) ? IEC104_ASDU_Para.SCO_SingleCommandElement.SE_code.EXECUTE : IEC104_ASDU_Para.SCO_SingleCommandElement.SE_code.SELECT;
            on_off = (on_off_state == OFF_STATE) ? IEC104_ASDU_Para.SCO_SingleCommandElement.SCS_code.CMD_OFF : IEC104_ASDU_Para.SCO_SingleCommandElement.SCS_code.CMD_ON;
            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.ACTIVATION, 0);
            IEC104_ASDU_Para.SCO_SingleCommandElement sco_val = new IEC104_ASDU_Para.SCO_SingleCommandElement(select_execute, IEC104_ASDU_Para.SCO_SingleCommandElement.QU_code.Persistent_output, on_off);
            IEC104_ASDU_Para.C_SC_NA_1_SinglePointCMD IOcmd = new IEC104_ASDU_Para.C_SC_NA_1_SinglePointCMD(remoteSetting, ioa, sco_val);
            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_SC_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Single CMD frame : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;

            isSingleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
            isSingleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()

            return tcpSendBuff(buff, len);
        }

        //46: C_DC_NA_1
        /* Practical Modern SCADA Protocols: DNP3, 60870.5 and Related Systems at  8.6.2 page 237
         * QOC(Qualifier of command) ( default=0 for BR-10R: ben tre)
            <0> = No additional definition
            <1> = Short pulse duration
            <2> = Long duration pulse
            <3> = Persistent output
         */
        public int sendDoubleCmd(int ioa, int select_execute_cmd, int on_off_state,int qoc=0)
        {
            byte[] buff = new byte[300];

            //IEC104_ASDU_Para.SCO_SingleCommandElement.SE_code select_execute;
            //IEC104_ASDU_Para.SCO_SingleCommandElement.SCS_code on_off;
            IEC104_ASDU_Para.DCO_DoubleCommandElement.SE_code select_execute;
            IEC104_ASDU_Para.DCO_DoubleCommandElement.DCS_code on_off;

            // int ioa = 0;
            select_execute = (select_execute_cmd == EXECUTE_CMD_T) ? IEC104_ASDU_Para.DCO_DoubleCommandElement.SE_code.EXECUTE : IEC104_ASDU_Para.DCO_DoubleCommandElement.SE_code.SELECT;

            on_off = (IEC104_ASDU_Para.DCO_DoubleCommandElement.DCS_code)on_off_state;
            //on_off = (on_off_state == OFF_STATE) ? IEC104_ASDU_Para.DCO_DoubleCommandElement.DCS_code.CMD_OFF : IEC104_ASDU_Para.DCO_DoubleCommandElement.DCS_code.CMD_ON;

            IEC104_ASDU_Para.CauseOfTransmission cot = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, false, false, IEC104_ASDU_Para.COT_Id.ACTIVATION, 0);


            //IEC104_ASDU_Para.SCO_SingleCommandElement sco_val = new IEC104_ASDU_Para.SCO_SingleCommandElement(select_execute, IEC104_ASDU_Para.SCO_SingleCommandElement.QU_code.Persistent_output, on_off);
            //IEC104_ASDU_Para.C_SC_NA_1_SinglePointCMD IOcmd = new IEC104_ASDU_Para.C_SC_NA_1_SinglePointCMD(remoteSetting, ioa, sco_val);

            IEC104_ASDU_Para.DCO_DoubleCommandElement.QU_code QU = (IEC104_ASDU_Para.DCO_DoubleCommandElement.QU_code)qoc;
            IEC104_ASDU_Para.DCO_DoubleCommandElement dco_val = new IEC104_ASDU_Para.DCO_DoubleCommandElement(select_execute, QU, on_off);

            //IEC104_ASDU_Para.DCO_DoubleCommandElement dco_val = new IEC104_ASDU_Para.DCO_DoubleCommandElement(select_execute, IEC104_ASDU_Para.DCO_DoubleCommandElement.QU_code.Persistent_output, on_off); // for Noja opt

            IEC104_ASDU_Para.C_DC_NA_1_DoublePointCMD IOcmd = new IEC104_ASDU_Para.C_DC_NA_1_DoublePointCMD(remoteSetting, ioa, dco_val);
            //hbaocr
            var asdu = new IEC104_ASDU(remoteSetting, IEC104_ASDU_Para.IOA_TypeID.C_DC_NA_1, cot, remoteCommonAddress, (IEC104_ASDU_Para.InformationObjBase)IOcmd);

            int master_tx_num = receiveSeqNum % (32768);
            int master_rx_num = (sendSeqNum + 1) % (32768);
            var apdu = new IEC104_APDU(remoteSetting, master_tx_num, master_rx_num, IEC104_APDU.ApciType.I_FORMAT, asdu);
            int len = apdu.byte_encode(buff);
            string s = MyUltil.byteArrayToHexString(buff, len);
            onCommunicationLog(this, true, "Double CMD frame : " + s);
            apciType_req = IEC104_ASDU_Para.ApciType.I_FORMAT;

            isDoubleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
            isDoubleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()

            return tcpSendBuff(buff, len);
        }

        public int counterInterrogationCMD()
        {
            isEnableSendCICmd = true;
            return 1;
        }

        public int resetProcessCMD()
        {
            isEnableSendResetProcessCMD = true;
            return 1;
        }

        public int readIOA(int ioa)
        {
            isEnableSendReadIOACmd = true;
            readcmdIOA = ioa;
            return 1;
        }
        public int singleCMDTest(int ioa, bool is_exe, int on_off, int timeout_sec = 2)
        {
            //doi voi NOJA : dia chi dong mo la ioa=2001
            //singleCmdTimeoutSec = timeout_sec;
            //singleCMDOnOffState = on_off;
            //singleCmdIOATest = ioa;
            singleCmdTestIOA = ioa;
            singleCMDTestOnOffState = on_off;
            singleCmdTestTimeoutSec = timeout_sec;
            isEnableSendSingleCmdTest = true;//enable flag to do step1 and step 2 in period send request
            isSingleCMDExe = is_exe;
            //stage_single_cmd = SELECT_CMD_T;
            return 1;
        }

        public int doubleCMDTest(int ioa, bool is_exe, int on_off, int timeout_sec = 2)
        {
            //doi voi NOJA : dia chi dong mo la ioa=2001
            //singleCmdTimeoutSec = timeout_sec;
            //singleCMDOnOffState = on_off;
            //singleCmdIOATest = ioa;
            doubleCmdTestIOA = ioa;
            doubleCMDTestOnOffState = on_off;
            doubleCmdTestTimeoutSec = timeout_sec;
            isEnableSendDoubleCmdTest = true;//enable flag to do step1 and step 2 in period send request
            isDoubleCMDExe = is_exe;
            //stage_single_cmd = SELECT_CMD_T;
            return 1;
        }



        /**
         trip_close  using select/execute singlecmd  to trigger reclose on/off.
            Step 1 : SELECT TASK : send SELECT_CMD_T to IOA then wait confirm from recloser by check isSingleCmdConfirmResp.
                     this flag is set true if reclose response confirm (CauseTx=7 ACTIVE CONFIRM) in parseAsdu() function
                     if timeout without any confirm -->task failed --> return -1; 
                     if isSingleCmdConfirmResp confirm true --> Select Task is OK. change to step 2 : EXECUTE TASK

            Step 2 : EXECUTE TASK : send EXECUTE_CMD_T to IOA then wait confirm from recloser by check isSingleCmdConfirmResp.
                     this flag is set true if reclose response confirm (CauseTx=7 ACTIVE CONFIRM) in parseAsdu() function
                    if timeout without any confirm -->task failed --> return -1; 
                    if isSingleCmdConfirmResp confirm true --> EXECUTE_CMD is OK. change to step 2 : EXECUTE TASK
           
            Step 3(optional) : Recloser response another confirm with CauseTx = 10 ACTIVE TERMINATION to tell the select/execute 
                               process is terminated by recloser .if that the flag isSingleCmdConfirmTerminate is set true  
                               in parseAsdu() function

            all cmd using QU = persistent output 
            @para : 
                ioa : information address of on/off recloser
                on_off :    ON_STATE (= 1)  --->turn on recloser
                            OFF_STATE( = 0) --->turn off recloser
                timeout_sec : in sec the time wait to check flag ( default =2 sec)
            @return : 1  : if OK
                      -1 : if timeout
                                
         */

        public int tripCloseCmd(int ioa, int on_off, int timeout_sec = 2)
        {
            //doi voi NOJA : dia chi dong mo la ioa=2001
            singleCmdTimeoutSec = timeout_sec;
            singleCMDOnOffState = on_off;
            singleCmdIOA = ioa;
            isEnableSendSingleCmd = true;//enable flag to do step1 and step 2 in period send request
            stage_single_cmd = SELECT_CMD_T;
            return 1;
        }



        long ms_singlecmd = 0;
        int stage_single_cmd = NO_CMD;
        public int tripCloseProccessHandle(int ioa_tripclose, int on_off, int timeout_sec)
        {

            switch (stage_single_cmd)
            {

                //step 1 : send select cmd

                // isSingleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
                //isSingleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()
                case SELECT_CMD_T:
                    onCommunicationLog(this, true, "Send Single CMD select");
                    sendSingleCmd(ioa_tripclose, SELECT_CMD_T, on_off);
                    ms_singlecmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    stage_single_cmd = CMD_WAIT_RESP;
                    break;
                //step 2 and 3 --> bo qua viec check confirm va terminate cmd resp
                case EXECUTE_CMD_T:
                    onCommunicationLog(this, true, "Send Single CMD execute");
                    sendSingleCmd(ioa_tripclose, EXECUTE_CMD_T, on_off);
                    ms_singlecmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    stage_single_cmd = NO_CMD;
                    return 1;
                    break;

                case CMD_WAIT_RESP:
                    long ms_now_cmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;

                    if ((ms_now_cmd - ms_singlecmd) > timeout_sec * 1000)//time out cmd
                    {
                        onErrorConnection(this, "Send Single CMD timeout");
                        return -2;/*time out cmd*/
                    }
                    if (isSingleCmdConfirmResp)
                    {
                        stage_single_cmd = EXECUTE_CMD_T;
                    }

                    break;
                default:
                    break;
            }


            return 0;

        }




        public int tripCloseDoubleCmd(int ioa, int on_off, int timeout_sec = 2)
        {
            //doi voi NOJA : dia chi dong mo la ioa=2001
            doubleCmdTimeoutSec = timeout_sec;
            doubleCMDOnOffState = on_off;
            doubleCmdIOA = ioa;
            isEnableSendDoubleCmd = true;//enable flag to do step1 and step 2 in period send request
            stage_double_cmd = SELECT_CMD_T;
            return 1;
        }

        long ms_doublecmd = 0;
        int stage_double_cmd = NO_CMD;
        public int tripCloseDoubleCMDProccessHandle(int ioa_tripclose, int on_off, int timeout_sec)
        {

            switch (stage_double_cmd)
            {

                //step 1 : send select cmd

                // isSingleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
                //isSingleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()
                case SELECT_CMD_T:
                    onCommunicationLog(this, true, "Send Double CMD select");
                    sendDoubleCmd(ioa_tripclose, SELECT_CMD_T, on_off);
                    ms_doublecmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    stage_double_cmd = CMD_WAIT_RESP;
                    break;
                //step 2 and 3 --> bo qua viec check confirm va terminate cmd resp
                case EXECUTE_CMD_T:
                    onCommunicationLog(this, true, "Send Double CMD execute");
                    sendDoubleCmd(ioa_tripclose, EXECUTE_CMD_T, on_off);
                    ms_doublecmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    stage_double_cmd = NO_CMD;
                    return 1;
                    break;

                case CMD_WAIT_RESP:
                    long ms_now_cmd = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;

                    if ((ms_now_cmd - ms_doublecmd) > timeout_sec * 1000)//time out cmd
                    {
                        onErrorConnection(this, "Send Double CMD timeout");
                        return -2;/*time out cmd*/
                    }
                    if (isDoubleCmdConfirmResp)
                    {
                        stage_double_cmd = EXECUTE_CMD_T;
                    }

                    break;
                default:
                    break;
            }


            return 0;

        }


        /*
        public int doTripCloseProcess(int ioa_tripclose, int on_off, int timeout_sec)
        {
            //step 1

            // isSingleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
            //isSingleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()
            int ioa = ioa_tripclose;
            t_waitcmd_tick_sec = 0;
            sendSingleCmd(ioa, SELECT_CMD_T, on_off);
            while (true)
            {
                if (isSingleCmdConfirmResp)
                    break;
                if (t_waitcmd_tick_sec > timeout_sec)
                {
                    return -1;
                }
                Thread.Sleep(50);
            }

            //step 2 and 3

            t_waitcmd_tick_sec = 0;
            sendSingleCmd(ioa, EXECUTE_CMD_T, on_off);
            while (true)
            {
                if (isSingleCmdConfirmResp)
                    break;
                if (isSingleCmdConfirmTerminate)
                    break;
                if (t_waitcmd_tick_sec > timeout_sec)
                {
                    return -1;
                }
                Thread.Sleep(50);
            }

            return 1;

        }*/
        /*    public int tripCloseCmd(int ioa, int on_off, int timeout_sec=2)
            {
                //step 1

                // isSingleCmdConfirmResp = false;//set to false -->if resp confirm -->it is set true in parseAsdu()
                //isSingleCmdConfirmTerminate = false;//set to false -->if resp confirm termiate process-->it is set true in parseAsdu()

                t_waitcmd_tick_sec = 0;
                singleCmdTimeoutSec= timeout_sec;
                sendSingleCmd(ioa, SELECT_CMD_T, on_off);
                while (true)
                {
                    if (isSingleCmdConfirmResp)
                        break;
                    if (t_waitcmd_tick_sec > singleCmdTimeoutSec)
                    {
                        return -1;
                    }
                    Thread.Sleep(50);
                }

                //step 2 and 3

                t_waitcmd_tick_sec = 0;
                sendSingleCmd(ioa, EXECUTE_CMD_T, on_off);
                while (true)
                {
                    if (isSingleCmdConfirmResp)
                        break;
                    if (isSingleCmdConfirmTerminate)
                        break;
                    if (t_waitcmd_tick_sec > singleCmdTimeoutSec)
                    {
                        return -1;
                    }
                    Thread.Sleep(50);
                }

                return 1;
            }
            */

        /*=====================Create connection========================== */
        /*
         * 
                  
         */


        int parseResponse()
        {
            
            if (client == null) return -2;

        
            NetworkStream stream = client.GetStream();
           
            if (stream == null) return -3;

            if ((stream.CanRead == false) || (stream.DataAvailable == false))
            {
                return -4;
            }
           
            //client.Available: Gets the amount of data that has been received from the network and is available to be read.
            int received_sz = client.Available;
            if (received_sz < 4)
            {
                if (onCommunicationLog != null) onCommunicationLog(this, true, "Received Data is not enough size: received_sz =" + received_sz.ToString());
                return -4;
            }


            int tmp = stream.ReadByte();
            if (tmp < 0) return -4;
            if (tmp != 0x68) return -5;

            int length = stream.ReadByte();
            if (length < 4 || length > 253)
            {
                // throw new IOException("APDU contain invalid length: " + length);
                onErrorConnection(this, "APDU contain invalid length: " + length);
                return -1;
            }
            recv_len = (byte)length;
            recvBuff = new byte[length + 2];
            recvBuff[0] = 0x68;
            recvBuff[1] = recv_len;
            // tcpReaderStream.ReadBytes(length).CopyTo(recvBuff, 2);
            tmp = stream.Read(recvBuff, 2, recv_len);
            if (tmp < 4) {
                // throw new IOException("APDU contain invalid length: " + length);
                onErrorConnection(this, "APDU contain invalid length: " + tmp);
                return -6; 
            }
            //=================Parse buffer from TCP===========================================

            int res = parseApdu();

           
            if (onCommunicationLog != null)
            {
                string s = MyUltil.byteArrayToHexString(recvBuff);
                onCommunicationLog(this, false, "New Incoming data,  Send_SEQ=" + sendSeqNum.ToString() + " ,  receive_SEQ=" + receiveSeqNum.ToString());
                onCommunicationLog(this, false, s);
            }
            //===============Process Protocol Stage=============================================
            if (res != -1) /*make sure Valid APCI,but not sure valid ASDU*/
            {
                is_recv_validframe = true;
                return 1;

            }
            return -1;

        }

        public int connect(int try_cnt)
        {

            int res = ERR;
            while ((try_cnt--) > 0)
            {
                try
                {
                    reset_para();
                    client = new TcpClient(this.remoteIP.ToString(), this.remotePort);
                    if (client != null)
                    {
                        isConnect = true;
                        sendSeqNum = 0;
                        receiveSeqNum = 0;
                        
                        // reset command before init 
                        sendStopDtAct();
                        Thread.Sleep(500);
                        sendResetProcessCMD();
                        Thread.Sleep(1500);

                        //=========First connect cmd

                        sendStartDtAct();//send  first handshake
                        //iec104ReceiveHDL.execute(true);
                        //iec104TimeOutHDL.execute(true);

                        res = OK;
                        break;

                    }


                }
                catch (Exception e)
                {
                    isConnect = false;
                    onErrorConnection(this, "TCP_IP connect failed : " + e.Message.ToString());
                }

            }
            return res;
        }



        /*=====================Close connection========================== */

        public void disconnect()
        {
            try
            {
                // iec104PeriodHandler.execute(false);
                reset_para();
                //iec104ReceiveHDL.execute(false);
                //iec104TimeOutHDL.execute(false);

                isConnect = false;
                client.Close();
                reset_para();

                if (onCommunicationLog != null) onCommunicationLog(this, true, "Force To Disconnect TCP");
            }
            catch (Exception e)
            {
                if (onCommunicationLog != null) onCommunicationLog(this, true, "Force To Disconnect TCP Err: " + e.Message.ToString());
            }
        }




        void reset_para()
        {
            //tout_startdtact = -1;
            //tout_supervisory = -1;
            if (onCommunicationLog != null)  onCommunicationLog(this, false, "reset_param ");
            tout_supervisory = remoteSetting.t2_supervisory;
            tout_startdtact = remoteSetting.t1_startdtact;

            /*
             * To make sure send first GI command 5 sec after connection to query status of devices.
             * After first periode , this will be  remoteSetting.gi_retry_time = 45sec;
            */
            const int first_gi_period = 5;
            tout_gi = first_gi_period;


            TxOk = false;
            VS = 0;
            VR = 0;

            isEnableSendSingleCmd = false;
            stage_single_cmd = NO_CMD;

            isEnableSendDoubleCmd = false;
            stage_double_cmd = NO_CMD;

            receiveSeqNum = 0;
            sendSeqNum = 0;

        }
        /*support parse function*/
        /*=====================parse entire of APDU recv buffer========================== */
        public int parseApdu()
        {
            int res = -1;

            //============APCI parse==============================
            res = parseApci();
            if (res < 0) return res;

            apciType_last_resp = apciType_resp;

            switch (apciType_resp)
            {

                case IEC104_ASDU_Para.ApciType.STARTDT_ACT:
                    sendStartDtCon();
                    break;
                case IEC104_ASDU_Para.ApciType.STARTDT_CON:
                    tout_startdtact = -1; // flag confirmation of STARTDT, not to timeout
                    TxOk = true;
                    tout_gi = 15; // request GI when communication starts
                    if (onCommunicationLog != null) onCommunicationLog(this, false, " receive STARTDT_CON ");
                    break;
                case IEC104_ASDU_Para.ApciType.TESTFR_ACT:
                    sendTestfrCon();
                    break;
                case IEC104_ASDU_Para.ApciType.STOPDT_ACT:
                    sendStopDtCon();
                    break;
                //============ASDU parse==============================
                case IEC104_ASDU_Para.ApciType.I_FORMAT:
                    try
                    {
                        parseAsdu();
                    }
                    catch (Exception e)
                    {
                        // MyUltil.pushlog("Invalid Frame format");
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Invalid Frame format :" + e.Message.ToString());
                        res = -2;
                    }
                    break;
                default:
                    break;

            }

            return res;
        }
        /*=============================parse APCI recv buffer========================== */
        private int parseApci()
        {
            int res = -1;
            byte[] acpi_buff = new byte[4];
            Array.Copy(recvBuff, 2, acpi_buff, 0, 4);

            if ((acpi_buff[0] & 0x01) == 0)
            {
                apciType_resp = IEC104_ASDU_Para.ApciType.I_FORMAT;
                sendSeqNum = ((acpi_buff[0] & 0xfe) >> 1) + ((acpi_buff[1] & 0xff) << 7);
                receiveSeqNum = ((acpi_buff[2] & 0xfe) >> 1) + ((acpi_buff[3] & 0xff) << 7);
                res = (int)(IEC104_ASDU_Para.ApciType.I_FORMAT);
            }
            else if ((acpi_buff[0] & 0x02) == 0)
            {
                apciType_resp = IEC104_ASDU_Para.ApciType.S_FORMAT;
                receiveSeqNum = ((acpi_buff[2] & 0xfe) >> 1) + ((acpi_buff[3] & 0xff) << 7);
                res = (int)(IEC104_ASDU_Para.ApciType.S_FORMAT);
            }
            else
            {
                if (acpi_buff[0] == 0x83)
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.TESTFR_CON;
                    res = (int)(IEC104_ASDU_Para.ApciType.TESTFR_CON);
                }
                else if (acpi_buff[0] == 0x43)
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.TESTFR_ACT;
                    res = (int)(IEC104_ASDU_Para.ApciType.TESTFR_ACT);
                }
                else if (acpi_buff[0] == 0x23)
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.STOPDT_CON;
                    res = (int)(IEC104_ASDU_Para.ApciType.STARTDT_CON);
                }
                else if (acpi_buff[0] == 0x13)
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.STOPDT_ACT;
                    res = (int)(IEC104_ASDU_Para.ApciType.STOPDT_ACT);
                }
                else if (acpi_buff[0] == 0x0B)
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.STARTDT_CON;
                    res = (int)(IEC104_ASDU_Para.ApciType.STARTDT_CON);
                }
                else
                {
                    apciType_resp = IEC104_ASDU_Para.ApciType.STARTDT_ACT;
                    res = (int)(IEC104_ASDU_Para.ApciType.STARTDT_ACT);
                }
            }

            return res;


        }
        /*=============================parse ASDU recv buffer========================== */
        private int parseAsdu()
        {
            int idx = 6;
            byte tid = recvBuff[idx++];
            ioa_type_id = (IEC104_ASDU_Para.IOA_TypeID)tid;
            byte vsq = recvBuff[idx++];


            IsSequenceOfElements = (vsq & 0x80) == 0x80;

            int numberOfSequenceElements;
            int numberOfInformationObjects;

            sequenceLength = vsq & 0x7f;
            if (IsSequenceOfElements)
            {
                numberOfSequenceElements = sequenceLength;
                numberOfInformationObjects = 1;
            }
            else
            {
                numberOfInformationObjects = sequenceLength;
                numberOfSequenceElements = 1;
            }

            cause_of_transmission_val = new IEC104_ASDU_Para.CauseOfTransmission(remoteSetting, recvBuff, idx);

            byte cot_byte = recvBuff[idx++];//8
            causeOfTransmission = (IEC104_ASDU_Para.COT_Id)(cot_byte & 0x3f);
            test = (cot_byte & 0x80) == 0x80;
            negativeConfirm = (cot_byte & 0x40) == 0x40;
            if (remoteSetting.cot_size == 2)
            {
                oa = recvBuff[idx++];//9
            }
            else
            {
                oa = -1;
            }

            if (remoteSetting.ca_size == 1)
            {
                recvCommonAdrr = recvBuff[idx++];
            }
            else
            {
                recvCommonAdrr = recvBuff[idx++] + (recvBuff[idx++] << 8);
            }
            int ioa_begin = idx;
            if (onCommunicationLog != null) onCommunicationLog(this, false, "------>> IOA_T  " + ioa_type_id.ToString());
            switch (ioa_type_id)
            {

                //for trip/close -->cmd -->wait for confirm
                case IEC104_ASDU_Para.IOA_TypeID.C_SC_NA_1: /*confirm from last single cmd request*/
                    if (causeOfTransmission == IEC104_ASDU_Para.COT_Id.ACTIVATION_CON)
                    {
                        isSingleCmdConfirmResp = true;
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Single CMD ACT confirmation ");
                    }

                    if (causeOfTransmission == IEC104_ASDU_Para.COT_Id.ACTIVATION_TERMINATION)
                    {
                        isSingleCmdConfirmResp = true; //execute task confirm active termination
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Single CMD ACT termination ");
                    }


                    break;

                //for trip/close -->cmd -->wait for confirm
                case IEC104_ASDU_Para.IOA_TypeID.C_DC_NA_1: /*confirm from last single cmd request*/
                    if (causeOfTransmission == IEC104_ASDU_Para.COT_Id.ACTIVATION_CON)
                    {
                        isDoubleCmdConfirmResp = true;
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Double CMD ACT confirmation ");
                    }

                    if (causeOfTransmission == IEC104_ASDU_Para.COT_Id.ACTIVATION_TERMINATION)
                    {
                        isDoubleCmdConfirmResp = true; //execute task confirm active termination
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Double CMD ACT termination ");
                    }


                    break;


                case IEC104_ASDU_Para.IOA_TypeID.C_IC_NA_1:/*cmd interrogation res*/
                    if (causeOfTransmission == IEC104_ASDU_Para.COT_Id.ACTIVATION_TERMINATION)
                    {
                        if (onCommunicationLog != null) onCommunicationLog(this, false, "Termination of Interrogation CMD");
                    }


                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_EI_NA_1:/*end of inital*/
                    //   sendSupervisory();
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "End of initation : M_EI_NA_1");
                    //Thread.Sleep(500);
                    //  sendInterrogation();
                    break;
                //===========================single point parse========================================
                case IEC104_ASDU_Para.IOA_TypeID.M_SP_NA_1:/*single point without timetag*/
                    M_SP_NA_1_val = new IEC104_ASDU_Para.M_SP_NA_1_SinglePointWithoutTime(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    int num_infs = M_SP_NA_1_val.SIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_SP_NA_1_val.ioa_list[i];
                        float val = (M_SP_NA_1_val.SIQ_list[i].spi == true ? 1 : 0);
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, ""));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_SP_NA_1 SinglePoint Recv");
                    onIncommingData(this);

                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_SP_TA_1:/*single point info with timetag CP24Time2a*/
                    M_SP_TA_1_val = new IEC104_ASDU_Para.M_SP_TA_1_SinglePointWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_SP_TA_1_val.SIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_SP_TA_1_val.ioa_list[i];
                        float val = (M_SP_TA_1_val.SIQ_list[i].spi == true ? 1 : 0);
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, ""));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_SP_TA_1 SinglePoint With Time24");
                    onIncommingData(this);

                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_SP_TB_1:/*single point info with timetag CP56Time2a*/
                    M_SP_TB_1_val = new IEC104_ASDU_Para.M_SP_TB_1_SinglePointWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_SP_TB_1_val.SIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_SP_TB_1_val.ioa_list[i];
                        float val = (M_SP_TB_1_val.SIQ_list[i].spi == true ? 1 : 0);
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        string t = M_SP_TB_1_val.TimeTag_list[i].ToString();
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_SP_TB_1 SinglePoint With Time56");
                    onIncommingData(this);

                    break;
                //===========================double point parse========================================

                case IEC104_ASDU_Para.IOA_TypeID.M_DP_NA_1:/*double point without timetag*/
                    M_DP_NA_1_val = new IEC104_ASDU_Para.M_DP_NA_1_DoublePointWithoutTime(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_DP_NA_1_val.DIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_DP_NA_1_val.ioa_list[i];
                        float val = (float)(M_DP_NA_1_val.DIQ_list[i].dpi);
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        string t = "";
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_DP_NA_1 DoublePoint");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_DP_TA_1:/*double point info with timetag CP24Time2a*/
                    M_DP_TA_1_val = new IEC104_ASDU_Para.M_DP_TA_1_DoublePointWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_DP_TA_1_val.DIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_DP_TA_1_val.ioa_list[i];
                        float val = (float)(M_DP_TA_1_val.DIQ_list[i].dpi);
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        string t = "";
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_DP_TA_1 DoublePoint With Time24");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_DP_TB_1:/*double point info with timetag CP56Time2a*/
                    M_DP_TB_1_val = new IEC104_ASDU_Para.M_DP_TB_1_DoublePointWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_DP_TB_1_val.DIQ_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_DP_TB_1_val.ioa_list[i];
                        float val = (float)(M_DP_TB_1_val.DIQ_list[i].dpi);
                        string t = M_DP_TB_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_DP_TB_1 DoublePoint With Time56");
                    onIncommingData(this);
                    break;

                //===========================step point parse========================================
                case IEC104_ASDU_Para.IOA_TypeID.M_ST_NA_1:/*step pos info without timetag*/
                    M_ST_NA_1_val = new IEC104_ASDU_Para.M_ST_NA_1_StepPositionInformationWithoutTime(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ST_NA_1_val.VTI_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ST_NA_1_val.ioa_list[i];
                        float val = (float)(M_ST_NA_1_val.VTI_list[i].vti_val);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ST_NA_1 StepPoint");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ST_TA_1:/*step pos info with timetag CP24Time2a*/
                    M_ST_TA_1_val = new IEC104_ASDU_Para.M_ST_TA_1_StepPositionInformationWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ST_TA_1_val.VTI_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ST_TA_1_val.ioa_list[i];
                        float val = (float)(M_ST_TA_1_val.VTI_list[i].vti_val);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ST_TA_1 StepPoint Time24");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ST_TB_1:/*step pos info with timetag CP56Time2a*/
                    M_ST_TB_1_val = new IEC104_ASDU_Para.M_ST_TB_1_StepPositionInformationWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ST_TB_1_val.VTI_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ST_TB_1_val.ioa_list[i];
                        float val = (float)(M_ST_TB_1_val.VTI_list[i].vti_val);
                        string t = M_ST_TB_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ST_TB_1 StepPoint Time56");
                    onIncommingData(this);
                    break;

                //===========================Nomalize parse========================================
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_NA_1:/*Measured value,normalized value*/
                    M_ME_NA_1_val = new IEC104_ASDU_Para.M_ME_NA_1_MeasuredNormalizedValue(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_NA_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_NA_1_val.ioa_list[i];
                        //  float val = (float)(M_ME_NA_1_val.value_list[i].scaleValue);
                        float val = (float)(M_ME_NA_1_val.value_list[i].IValue);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_NA_1_MeasuredNormalizedValue");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TA_1:/*Measured value,normalized value*/
                    M_ME_TA_1_val = new IEC104_ASDU_Para.M_ME_TA_1_MeasuredNormalizedValueWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TA_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TA_1_val.ioa_list[i];
                        // float val = (float)(M_ME_TA_1_val.value_list[i].scaleValue);
                        float val = (float)(M_ME_TA_1_val.value_list[i].IValue);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TA_1_val Time24");
                    onIncommingData(this);

                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TD_1:/*Measured value,normalized value*/
                    M_ME_TD_1_val = new IEC104_ASDU_Para.M_ME_TD_1_MeasuredNormalizedValueWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TD_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TD_1_val.ioa_list[i];
                        // float val = (float)(M_ME_TD_1_val.value_list[i].scaleValue);
                        float val = (float)(M_ME_TD_1_val.value_list[i].IValue);

                        string t = M_ME_TD_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TD_1_MeasuredNormalizedValueWithTime56");
                    onIncommingData(this);
                    break;

                //===========================Scaled parse========================================
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_NB_1:/*Measured value,scaled value*/
                    M_ME_NB_1_val = new IEC104_ASDU_Para.M_ME_NB_1_MeasuredScaledValue(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_NB_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_NB_1_val.ioa_list[i];
                        float val = (float)(M_ME_NB_1_val.value_list[i].IValue);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_NB_1_MeasuredScaledValue");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TB_1:/*Measured value,scaled value*/
                    M_ME_TB_1_val = new IEC104_ASDU_Para.M_ME_TB_1_MeasuredScaledValueWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TB_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TB_1_val.ioa_list[i];
                        float val = (float)(M_ME_TB_1_val.value_list[i].IValue);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TB_1_MeasuredScaledValueWithTime24");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TE_1:/*Measured value,scaled value*/
                    M_ME_TE_1_val = new IEC104_ASDU_Para.M_ME_TE_1_MeasuredScaledValueWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TE_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TE_1_val.ioa_list[i];
                        float val = (float)(M_ME_TE_1_val.value_list[i].IValue);
                        string t = M_ME_TE_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TE_1_MeasuredScaledValueWithTime56");
                    onIncommingData(this);
                    break;

                //===========================Single float parse========================================
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_NC_1:/*Measured value,Single float value*/
                    M_ME_NC_1_val = new IEC104_ASDU_Para.M_ME_NC_1_MeasuredShortFloatingPointValue(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_NC_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_NC_1_val.ioa_list[i];
                        float val = (float)(M_ME_NC_1_val.value_list[i].GetValue());
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_NC_1_MeasuredShortFloatingPointValue");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TC_1:/*Measured value,Single float value*/
                    M_ME_TC_1_val = new IEC104_ASDU_Para.M_ME_TC_1_MeasuredShortFloatingPointWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TC_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TC_1_val.ioa_list[i];
                        float val = (float)(M_ME_TC_1_val.value_list[i].GetValue());
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TC_1_MeasuredShortFloatingPointWithTime24");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_ME_TF_1:/*Measured value,Single float value*/
                    M_ME_TF_1_val = new IEC104_ASDU_Para.M_ME_TF_1_MeasuredShortFloatingPointWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_ME_TF_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_ME_TF_1_val.ioa_list[i];
                        float val = (float)(M_ME_TF_1_val.value_list[i].GetValue());
                        string t = M_ME_TF_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_ME_TF_1_MeasuredShortFloatingPointWithTime56");
                    onIncommingData(this);
                    break;

                case IEC104_ASDU_Para.IOA_TypeID.M_IT_NA_1:/*Integrated val*/
                    M_IT_NA_1_val = new IEC104_ASDU_Para.M_IT_NA_1_IntegratedTotalsValue(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_IT_NA_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_IT_NA_1_val.ioa_list[i];
                        float val = (float)(M_IT_NA_1_val.value_list[i].counterReading);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_IT_NA_1_IntegratedTotalsValue");
                    onIncommingData(this);
                    break;

                case IEC104_ASDU_Para.IOA_TypeID.M_IT_TB_1:/*Integrated val*/
                    M_IT_TB_1_val = new IEC104_ASDU_Para.M_IT_TB_1_IntegratedTotalsValuetWithTime24(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_IT_TB_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_IT_TB_1_val.ioa_list[i];
                        float val = (float)(M_IT_TB_1_val.value_list[i].counterReading);
                        string t = "";
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_IT_TB_1_IntegratedTotalsValuetWithTime24");
                    onIncommingData(this);
                    break;
                case IEC104_ASDU_Para.IOA_TypeID.M_IT_TA_1:/*Integrated val*/
                    M_IT_TA_1_val = new IEC104_ASDU_Para.M_IT_TA_1_IntegratedTotalsValueWithTime56(remoteSetting, IsSequenceOfElements, sequenceLength, recvBuff, idx);
                    num_infs = M_IT_TA_1_val.ioa_list.Count;
                    listNewIncommingData.Clear();
                    for (int i = 0; i < num_infs; i++)
                    {
                        int ioa = M_IT_TA_1_val.ioa_list[i];
                        float val = (float)(M_IT_TA_1_val.value_list[i].counterReading);
                        string t = M_IT_TA_1_val.TimeTag_list[i].ToString();
                        int cot = (int)cause_of_transmission_val.cot_id;
                        int ioa_tid = (int)ioa_type_id;
                        listNewIncommingData.Add(new RespValue(ioa, val, ioa_tid, cot, t));
                    }
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "M_IT_TA_1_IntegratedTotalsValueWithTime56");
                    onIncommingData(this);
                    break;

                default:
                    if (onCommunicationLog != null) onCommunicationLog(this, false, "-------> TYPE ID of IOA (" + ioa_type_id.ToString() + " ) not support");
                    //MyUltil.pushlog("Current IOA do not support ioa_tid yet : " + ioa_type_id);
                    break;

            }


            return idx;


        }



        // to ping and keep connection
        private void iec104_control_frame_hdl()
        {

            /*
             * the tout_startdtact must be sent first to start communication with device. This sendStartDtAct must send only once when new tcp connection
             * when devices confirm for sendStartDtAct by IEC104_ASDU_Para.ApciType.STARTDT_CON==> set tout_startdtact = -1 < 0 ==> skip sending sendStartDtAct
             */
            if (tout_startdtact >= 0)
            {
                if (tout_startdtact == 0)
                {
                    sendStartDtAct();
                    tout_startdtact = remoteSetting.t1_startdtact;
                }

            }

            //onCommunicationLog(this, false, "tout_supervisory : " + tout_supervisory);
            // will wait t2 seconds or n messages to send supervisory window control
            if (--tout_supervisory <= 0)
            {
                sendSupervisory();
                tout_supervisory = remoteSetting.t2_supervisory;//8s
            }

            /*if connected (device confirm with STARTDT_CON ==> TxOK = true) 
             * and no data received( whenever new data is comming tout_testfr is set =  remoteSetting.t3_testfr >0) 
             * if this tout_testfr  --> 0  ==> no incomming data from devices  ===> send sendTestfrAct
             */

            // the tout_testfr will be equal to remoteSetting.t3_testfr in iec104_receive_hdl whenever new data come. We do't need to send testfr when real data already come.
            if ((--tout_testfr <= 0) && (TxOk == true))
            {
                sendTestfrAct();
                tout_testfr = remoteSetting.t3_testfr;

            }

            /*
             * send GI to update device status each 45 sec except first period 15ec 
             */

            if (--tout_gi <= 0)
            {
                sendInterrogation();//solicitGI();  
                tout_gi = remoteSetting.gi_retry_time; //45 sec
            }


        }

        // to receive data


        private void handle_stateless_cmd() {
            if (isEnableSendResetProcessCMD)
            {
                onCommunicationLog(this, true, "Send Reset Process C_RP_NA_1 cmd ");
                sendResetProcessCMD();
                isEnableSendResetProcessCMD = false;
            }

            if (isEnableSendCICmd)
            {
                onCommunicationLog(this, true, "Send Read C_CI_NA_1 cmd ");
                sendGeneralCounterInterrogation();
                isEnableSendCICmd = false;
            }

            if (isEnableSendReadIOACmd)
            {
                onCommunicationLog(this, true, "Send Read IOA cmd C_RD_NA_1 " + readcmdIOA);
                sendReadIOACommand(readcmdIOA);

                isEnableSendReadIOACmd = false;
            }

            if (isEnableSendSingleCmdTest)
            {
                if (isSingleCMDExe)
                {
                    onCommunicationLog(this, true, "Send Single CMD Exec");
                    sendSingleCmd(singleCmdTestIOA, EXECUTE_CMD_T, singleCMDTestOnOffState);
                }
                else
                {
                    onCommunicationLog(this, true, "Send Single CMD Select");
                    sendSingleCmd(singleCmdTestIOA, SELECT_CMD_T, singleCMDTestOnOffState);
                }

                isEnableSendSingleCmdTest = false;

            }


            if (isEnableSendDoubleCmdTest)
            {
                if (isDoubleCMDExe)
                {
                    onCommunicationLog(this, true, "Send Double CMD Exec");
                    sendDoubleCmd(doubleCmdTestIOA, EXECUTE_CMD_T, doubleCMDTestOnOffState);
                }
                else
                {
                    onCommunicationLog(this, true, "Send Double CMD Select");
                    sendDoubleCmd(doubleCmdTestIOA, SELECT_CMD_T, doubleCMDTestOnOffState);
                }

                isEnableSendDoubleCmdTest = false;

            }
        
        }
        private void iec104_receive_hdl()
        {
            int ret = -1;

            try
            {
                ret = parseResponse();
                handle_stateless_cmd();
                if (ret > 0)
                {


                    //do trip close single cmd

                    if (isEnableSendSingleCmd)
                    {
                        int is_ok = tripCloseProccessHandle(singleCmdIOA, singleCMDOnOffState, singleCmdTimeoutSec);
                        if (is_ok != 0)
                        {
                            isEnableSendSingleCmd = false;//finish stage
                            stage_single_cmd = NO_CMD;
                        }

                    }


                    //do trip close double cmd

                    if (isEnableSendDoubleCmd)
                    {
                        //int is_ok = tripCloseProccessHandle(singleCmdIOA, singleCMDOnOffState, singleCmdTimeoutSec);
                        int is_ok = tripCloseDoubleCMDProccessHandle(doubleCmdIOA, doubleCMDOnOffState, doubleCmdTimeoutSec);
                        if (is_ok != 0)
                        {
                            isEnableSendDoubleCmd = false;//finish stage
                            stage_double_cmd = NO_CMD;
                        }

                    }


                    // reset test_frame tick to prevent sending out testframe when no data is coming
                    tout_testfr = remoteSetting.t3_testfr;

                   
                }
            }
            catch (Exception e)
            {
                if (isConnect)
                {
                    onErrorConnection(this, "TCP ERR Receive :" + e.Message.ToString());
                    disconnect();
                    connect(2);
                }

            }


        }




        /*this will be call out side as the timer clock of iec104 protocol*/
        public void iec104_1sec_tick_hdl()
        {
            try
            {
                if ((isConnect) && (client != null))
                {
                    //onCommunicationLog(this, true, "-----------tick clock");
                    iec104_receive_hdl();
                    iec104_control_frame_hdl();
                }
            }
            catch (Exception e)
            {
                // MyUltil.pushlog("Invalid Frame format");
                if (onCommunicationLog != null) onCommunicationLog(this, false, "iec104_1sec_tick_hdl err :" + e.Message.ToString());
            }

        }

    }

}

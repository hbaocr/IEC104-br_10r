using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    class IEC104_APDU
    {
        private static readonly byte[] TestfrActBuffer = { 0x68, 0x04, 0x43, 0x00, 0x00, 0x00 };
        private static readonly byte[] TestfrConBuffer = { 0x68, 0x04, 0x83, 0x00, 0x00, 0x00 };

        private static readonly byte[] StopdtActBuffer = { 0x68, 0x04, 0x13, 0x00, 0x00, 0x00 };
        private static readonly byte[] StopdtConBuffer = { 0x68, 0x04, 0x23, 0x00, 0x00, 0x00 };

        private static readonly byte[] StartdtActBuffer = { 0x68, 0x04, 0x07, 0x00, 0x00, 0x00 };
        private static readonly byte[] StartdtConBuffer = { 0x68, 0x04, 0x0b, 0x00, 0x00, 0x00 };



        public enum ApciType
        {
            I_FORMAT,
            S_FORMAT,
            TESTFR_CON,/*U format*/
            TESTFR_ACT,/*U format*/
            STOPDT_CON,/*U format*/
            STOPDT_ACT,/*U format*/
            STARTDT_CON,/*U format*/
            STARTDT_ACT/*U format*/
        }


        private ApciType apciType;
        public int receiveSeqNum = 0;
        public int sendSeqNum = 0;
        IEC104_ASDU aSdu;
        IEC104_Setting setting;

        public IEC104_APDU(IEC104_Setting _setting, int sendSeqNum, int receiveSeqNum, ApciType apciType, IEC104_ASDU aSdu)
        {
            this.sendSeqNum = sendSeqNum;
            this.receiveSeqNum = receiveSeqNum;
            this.apciType = apciType;
            this.aSdu = aSdu;
            this.setting = _setting;
        }
        public int byte_encode(byte[] buffer)
        {
            int len = 0;
            switch (apciType)
            {
                case ApciType.S_FORMAT:
                    buffer[len++] = (byte)0x68;
                    buffer[len++] = (byte)0x04;
                    buffer[len++] = (byte)0x01;
                    buffer[len++] = (byte)0x00;
                    buffer[len++] = (byte)((receiveSeqNum << 1) & 0xFE);
                    buffer[len++] = (byte)((receiveSeqNum >> 7) & 0x00FF);
                    break;
                case ApciType.STARTDT_ACT:
                    StartdtActBuffer.CopyTo(buffer, 0);
                    len = StartdtActBuffer.Length;
                    break;
                case ApciType.STOPDT_ACT:
                    StopdtActBuffer.CopyTo(buffer, 0);
                    len = StopdtActBuffer.Length;
                    break;
                case ApciType.TESTFR_ACT:
                    TestfrActBuffer.CopyTo(buffer, 0);
                    len = TestfrActBuffer.Length;
                    break;
                case ApciType.I_FORMAT:
                    buffer[len++] = (byte)0x68;
                    buffer[len++] = (byte)0x00;/*tmp len*/
                    buffer[len++] = (byte)((sendSeqNum << 1) & 0xFE);; //number of I-frame transmit
                    buffer[len++] = (byte)((sendSeqNum >> 7) & 0x00FF);
                    buffer[len++] = (byte)((receiveSeqNum << 1) & 0xFE);//number of I-frame response from Copper
                    buffer[len++] = (byte)((receiveSeqNum >> 7) & 0x00FF);
                    len += aSdu.byte_encode(buffer, len);
                    buffer[1] = (byte)(len-2);//chieu dai khong tinh byte dau va byte len                    
                    break;
                default:
                    /*IEC104 master not support*/
                    len = 0;
                    break;
            }
            return len;
        }


    }
}

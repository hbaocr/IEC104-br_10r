using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    class IEC104_ASDU
    {
        private IEC104_Setting setting;       
        private IEC104_ASDU_Para.IOA_TypeID tid = IEC104_ASDU_Para.IOA_TypeID.C_IC_NA_1;/*global interrogation*/
        byte VSQ = 1;/*sq false and 1 IOA*/
        IEC104_ASDU_Para.CauseOfTransmission cot;
        int CA;
        private byte[] InformationObject_buffers = new byte[255];
        private IEC104_ASDU_Para.InformationObjBase InformationObj =null;
        public IEC104_ASDU(IEC104_Setting setting,IEC104_ASDU_Para.IOA_TypeID tid, bool SQ, byte numberofIOA,
            IEC104_ASDU_Para.CauseOfTransmission cot, int CA, byte[] IObuffs)
        {
            this.setting = setting;
            this.tid = tid;        
           
            if (SQ)
            {
                VSQ = (byte)(numberofIOA | (0x80));
            }
            else
            {
                VSQ = (byte)(numberofIOA & (0x7F));
            }

            this.cot = cot;
            this.CA = CA;
            IObuffs.CopyTo(InformationObject_buffers, 0);            
        }

        public IEC104_ASDU(IEC104_Setting setting, IEC104_ASDU_Para.IOA_TypeID tid, 
           IEC104_ASDU_Para.CauseOfTransmission cot, int CA, IEC104_ASDU_Para.InformationObjBase infoObj)
        {
            this.setting = setting;
            this.tid = tid;
            bool SQ = false;
            byte numberofIOA = 1;
            if (SQ)
            {
                VSQ = (byte)(numberofIOA | (0x80));
            }
            else
            {
                VSQ = (byte)(numberofIOA & (0x7F));
            }

            this.cot = cot;
            this.CA = CA;
            this.InformationObj = infoObj;
        }

        public int byte_encode(byte[] buffer, int pos = 0)
        {
            
            var i = pos;
            buffer[i++] = (byte)tid;
            buffer[i++] = (byte)VSQ;
            int l=cot.byte_encode(buffer, i);
            i = i + l;

            buffer[i++] = (byte)CA;
            if (setting.ca_size == 2)
            {
                buffer[i++] = (byte)(CA >> 8);
            }

            if (InformationObj == null)
            {
                for (int k = 0; k < InformationObject_buffers.Length; k++)
                {
                    buffer[i + k] = InformationObject_buffers[k];
                }
                i = i + InformationObject_buffers.Length;
                return (i - pos);
            }
            else
            {
                int s=InformationObj.byte_encode(buffer, i);
                return (i +s- pos);
            }
        
        }

    }
}

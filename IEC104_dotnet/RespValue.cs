using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    public class RespValue
    {
       
            public int ioa;
            public float value;
            public int tid;
            public int last_cot;
            public string time;
            public RespValue(int ioa, float value, int tid, int last_cot, string time)
            {
                this.ioa = ioa;
                this.value = value;
                this.tid = tid;
                this.last_cot = last_cot;
                this.time = time;
            }

            public RespValue(RespValue resp) {
                this.ioa = resp.ioa;
                this.value = resp.value;
                this.tid = resp.tid;
                this.last_cot = resp.last_cot;
                this.time = resp.time;
            }
    }
}

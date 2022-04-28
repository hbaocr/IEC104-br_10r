using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC104_dotnet
{
    class IEC104_IOValue_HashMap
    {
        public Dictionary<int, RespValue> informationObjValueTables = new Dictionary<int, RespValue>();
        public void UpdateTableInfos(RespValue resp)
        {
            int key = resp.ioa;
            if (informationObjValueTables.ContainsKey(key))
            {
                informationObjValueTables[key] = resp;
            }
            else
            {
                informationObjValueTables.Add(key, resp);
            }
        }
        public void Clear()
        {
            informationObjValueTables.Clear();
        }

        public int getLength() { return informationObjValueTables.Count; }
        public RespValue getElementAt(int idx)
        {
            return informationObjValueTables.ElementAt(idx).Value;
        }

        public string ToString()
        {
            var list = informationObjValueTables.Keys.ToList();
            list.Sort();
            string str = "";
            foreach (var key in list)
            {
                RespValue respval = informationObjValueTables[key];
                str += "IOA= " + respval.ioa.ToString("D4");
                str += "      TypeID= " + respval.tid.ToString("D2");
                
                str += "      COT= " + respval.last_cot.ToString("D4");
                str += "      VALUE= " + respval.value.ToString();
                if (respval.time.Length > 0) {
                    str += "      TIME= " + respval.time.Trim();
                }
                str = str + MyUltil.new_line;
            }
            return str;
        }

    }
}

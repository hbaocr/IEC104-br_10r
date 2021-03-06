using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace IEC104_dotnet
{
    public partial class Form1 : Form
    {
        //IEC104MasterHelper app2;
        //IEC104DeviceBase app3;
        IEC104DeviceBaseAsync app3;

        IEC104_IOValue_HashMap tablesInfos = new IEC104_IOValue_HashMap();

        //public void onErrorConnection(IEC104MasterHelper sender,string message)
        //{
        //     MyUltil.AppendText(txtReceive, "Erro Msg: " + message);
        //}

        ////=======call when Incomming data=============================

        ////==========================call when log======================
        //public void onCommunicationLog(IEC104MasterHelper sender,bool is_tx_frame, string hexframe)
        //{
        //    string pre_fix = (is_tx_frame ? "<---TX :" : "--->RX :");
        //    MyUltil.pushlog(pre_fix + hexframe);
        //    MyUltil.AppendText(txtReceive, pre_fix + hexframe);
        //}

        //public void onIncommingData(IEC104MasterHelper sender)
        //{
        //    for (int i = 0; i < sender.listNewIncommingData.Count; i++)
        //    {
        //        RespValue val = sender.listNewIncommingData[i];
        //        tablesInfos.UpdateTableInfos(new RespValue(val));
        //    }
        //    MyUltil.SetText(txtResult, tablesInfos.ToString());          
        //}


        public void onErrorConnection(IEC104ProtocolAsync sender, string message)
        {
            MyUltil.AppendText(txtReceive, "Erro Msg: " + message);
        }

        //=======call when Incomming data=============================

        //==========================call when log======================
        public void onCommunicationLog(IEC104ProtocolAsync sender, bool is_tx_frame, string hexframe)
        {
            string pre_fix = (is_tx_frame ? "<---TX :" : "--->RX :");
            MyUltil.pushlog(pre_fix + hexframe);
            MyUltil.AppendText(txtReceive, pre_fix + hexframe);
        }

        public void onIncommingData(IEC104ProtocolAsync sender)
        {
            for (int i = 0; i < sender.listNewIncommingData.Count; i++)
            {
                RespValue val = sender.listNewIncommingData[i];
                tablesInfos.UpdateTableInfos(new RespValue(val));
            }
            MyUltil.SetText(txtResult, tablesInfos.ToString());
        }


        bool isconnect = false;
        public Form1()
        {

            InitializeComponent();
        }


        


        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isconnect == false)
            {
                string ip = txtIP.Text.Trim().ToString();
                int port = int.Parse(txtPort.Text);
                int ca = int.Parse(txtCA.Text);

                //app2 = new IEC104MasterHelper(ip, port, ca);
                //app2.onCommunicationLog += onCommunicationLog;
                //app2.onErrorConnection += onErrorConnection;
                //app2.onIncommingData += onIncommingData;

                //btnConnect.Enabled = false;
                //if (app2.connect(1) >= 0)
                //{
                //    isconnect = true;
                //    btnConnect.Text = "Disconnect";
                //}

                app3 = new IEC104DeviceBaseAsync(ip, port, ca);
                app3.setup_callback(onIncommingData, onCommunicationLog, onErrorConnection);
                int trycnt = 2;
                int ret = app3.start(trycnt);
                btnConnect.Enabled = false;
                if (ret>= 0)
                {
                    isconnect = true;
                    btnConnect.Text = "Disconnect";
                }
                btnConnect.Enabled = true;

            }
            else
            {
                btnConnect.Enabled = false;
                btnConnect.Text = "Connect";
                //app2.disconnect();
                app3.stop();
                isconnect = false;
                btnConnect.Enabled = true;

            }

        }

        private void btnTRIP_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOAAddr.Text);
            //app2.tripCloseCmd(ioa, 0, 5);
            //app2.tripCloseDoubleCmd(ioa, 1, 5);
            app3.iec104protocol.tripCloseDoubleCmd(ioa, 1, 5);
        }

        private void btnCLOSE_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOAAddr.Text);
            //app2.tripCloseCmd(ioa, 1, 5);
            //app2.tripCloseDoubleCmd(ioa, 2, 5);
            app3.iec104protocol.tripCloseDoubleCmd(ioa, 2, 5);
        }

        private void btnSingleTRIP_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtSingleIOA.Text);
            // app2.tripCloseCmd(ioa, 0, 5);
            app3.iec104protocol.tripCloseCmd(ioa, 0, 5);
            //app2.tripCloseDoubleCmd(ioa, 0, 5);
        }

        private void btnSingleClose_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtSingleIOA.Text);
            //app2.tripCloseCmd(ioa, 1, 5);
            app3.iec104protocol.tripCloseCmd(ioa, 1, 5);
            //app2.tripCloseDoubleCmd(ioa, 1, 5);
        }

        private void btnDoubleTestSelect_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOATestDouble.Text);
            int val = int.Parse(txtDoubleTestValue.Text);
            //app2.doubleCMDTest(ioa, false, val, 5);
            app3.iec104protocol.doubleCMDTest(ioa, false, val, 5);

        }

        private void btnDoubleTestExec_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOATestDouble.Text);
            int val = int.Parse(txtDoubleTestValue.Text);
            //app2.doubleCMDTest(ioa, true, val, 5);
            app3.iec104protocol.doubleCMDTest(ioa, true, val, 5);
        }

        private void btnSingleTestSel_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOATestSingle.Text);
            int val = int.Parse(txtSingleTestValue.Text);
            //app2.singleCMDTest(ioa, false, val, 5);
            app3.iec104protocol.singleCMDTest(ioa, false, val, 5);
        }

        private void btnSingleTestExec_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOATestSingle.Text);
            int val = int.Parse(txtSingleTestValue.Text);
            //app2.singleCMDTest(ioa, true, val, 5);
            app3.iec104protocol.singleCMDTest(ioa, true, val, 5);

        }

        private void btnReadIOA_Click(object sender, EventArgs e)
        {
            int ioa = int.Parse(txtIOARead.Text);
            //app2.readIOA(ioa);
            app3.iec104protocol.readIOA(ioa);

        }

        private void btnCI_Click(object sender, EventArgs e)
        {
            //app2.counterInterrogationCMD();
            app3.iec104protocol.counterInterrogationCMD();
        }

        private void btnResetProcess_Click(object sender, EventArgs e)
        {
            //app2.resetProcessCMD();
            app3.iec104protocol.sendResetProcessCMD();
        }

       
   
    }
}

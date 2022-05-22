```C#
    //1. declare instance
    IEC104Device app5;



    //2. Define the callback function

    public void onErrorConnection(IEC104ProtocolHdl sender, string message)
    {
        MyUltil.AppendText(txtReceive, "Erro Msg: " + message);
    }

    //=======call when Incomming data=============================

    //==========================call when log======================
    public void onCommunicationLog(IEC104ProtocolHdl sender, bool is_tx_frame, string hexframe)
    {
        var dt = DateTime.Now;
        string pre_fix = (is_tx_frame ? "<---TX-v2 :" : "--->RX-v2 :");
        pre_fix = dt.ToString() + " : " + pre_fix;
        MyUltil.pushlog(pre_fix + hexframe);
        MyUltil.AppendText(txtReceive, pre_fix + hexframe);
    }

    public void onIncommingData(IEC104ProtocolHdl sender)
    {
        for (int i = 0; i < sender.listNewIncommingData.Count; i++)
        {
            RespValue val = sender.listNewIncommingData[i];
            tablesInfos.UpdateTableInfos(new RespValue(val));
        }
        MyUltil.SetText(txtResult, tablesInfos.ToString());
    }


    //3. Init the function with callback and iec para
    app5 = new IEC104Device(ip, port, ca);
    app5.setup_callback(onIncommingData, onCommunicationLog, onErrorConnection);
    int trycnt = 2;
    int ret = app5.start(trycnt);
    if (ret>= 0)
    {
        isconnect = true;
        btnConnect.Text = "Disconnect";
    }

    //4. to stop call 
    app5.stop();

```
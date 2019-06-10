using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Demo.Service {

public class ConnectServerInfo {

    public string db_Ip;
    public string db_Port;
    public string db_Name;

    public string server_Ip;

    public ConnectServerInfo() {
        server_Ip = "35.201.142.203";
        db_Ip = "35.201.142.203";
        db_Port = "3306";
        db_Name = "ncku_hw";
    }
}
}
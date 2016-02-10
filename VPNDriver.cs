using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace caudalrio.VPNManager
{
    public class VPNDriver
    {
        #region --Const--
        /// <summary>
        /// Where the rasdial.exe lives
        /// </summary>
        private const string RASDIAL = "C:\\Windows\\System32\\rasdial.exe";

        #endregion

        #region --Fields--
        #endregion

        #region --Constructors--
        public VPNDriver()
        { }
        #endregion

        #region --Public Methods--
        /// <summary>
        /// Connect to VPN Server
        /// </summary>
        /// <history>
        ///     [Yamir Ramirez]   02/02/2016  Created
        /// </history>
        public void Connect(string vpnName, string vpnUsername, string vpnPassword)
        {
            try
            {
                string cadena = string.Format("\"{0}\" {1} {2}", vpnName, vpnUsername, vpnPassword);
                Process process = Process.Start(RASDIAL, cadena);
                process.WaitForExit();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        public void Disconnect(string vpnName)
        {
            try
            {
                string cadena = string.Format("\"{0}\" /DISCONNECT", vpnName);
                Process process = Process.Start(RASDIAL, cadena);
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsConnected(string vpnName)
        {
            //inpired in http://stackoverflow.com/questions/12227540/how-can-i-know-whether-a-vpn-connection-is-established-or-not
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface Interface in interfaces)
                {
                    if (Interface.OperationalStatus == OperationalStatus.Up)
                    {
                        if ((Interface.NetworkInterfaceType == NetworkInterfaceType.Ppp) && (Interface.NetworkInterfaceType != NetworkInterfaceType.Loopback))
                        {
                            IPv4InterfaceStatistics statistics = Interface.GetIPv4Statistics();
                            if (Interface.Name == vpnName)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void ConnectAndVerify(string vpnName, string vpnUsername, string vpnPassword, ref TextBox textErrorBox)
        {
            this.Connect(vpnName, vpnUsername, vpnPassword);

            if (!this.IsConnected(vpnName))
            {
                textErrorBox.Text = "No se pudo conectar a la VPN";
                throw new Exception("VPN Connection Failed");
            }
        }
        #endregion
    }
}

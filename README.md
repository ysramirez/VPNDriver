# VPNDriver
C# VPN Driver

##Examples:
```c#
vpnDriver = new VPNDriver();

vpnDriver.Connect("MyVPNName", "Username", "Password");

vpnDriver.IsConnected("MyVPNName");

vpnDriver.ConnectAndVerify("MyVPNName", "Username", "Password", ref errorTextBox); //show the error in the textbox

vpnDriver.Disconnect("MyVPNName");
```

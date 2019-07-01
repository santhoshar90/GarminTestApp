<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBack.aspx.cs" Inherits="CHBase_FitBit_TestProject.Garmin.Pages.CallBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    In CallBack Page,.
    </div>
        <div>
            Click <a id="lnkActivities" runat="server" onserverclick="lnkActivities_ServerClick">here</a> to get Activities
        </div>
    </form>
</body>
</html>

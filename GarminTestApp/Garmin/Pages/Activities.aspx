<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activities.aspx.cs" Inherits="CHBase_FitBit_TestProject.Garmin.Pages.Activities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnFetchBackLogActivities" runat="server" Text="Fetch Activities" OnClick="btnFetchBackLogActivities_Click" />
     <asp:Button ID="btnGetUserId" runat="server" Text="GetUserId" OnClick="btnGetUserId_Click" />
    </div>
    </form>
</body>
</html>

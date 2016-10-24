<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SSO.General.Authorize.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WebForm</title>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js" type="text/javascript"></script>
    <script src="SSO.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Authorize</h3>
            <asp:TextBox ID="txtUserData" runat="server"></asp:TextBox>
            <asp:Button Text="注销" runat="server" ID="SignOut" OnClick="SignOut_Click" />
        </div>
    </form>
</body>
</html>

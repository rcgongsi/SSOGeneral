<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SSO.General.Web1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Web1</h3>
            <asp:TextBox ID="txtUserData" runat="server"></asp:TextBox>
            <asp:Button Text="注销" runat="server" ID="SignOut" OnClick="SignOut_Click" />
            <a href="http://localhost:56765/WebForm1.aspx">链接到Web2</a>
        </div>
    </form>
</body>
</html>
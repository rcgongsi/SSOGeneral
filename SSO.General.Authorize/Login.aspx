<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSO.General.Authorize.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户登录</title>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js" type="text/javascript"></script>
    <script src="SSO.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>用户登录</legend>
                <span>用户名：</span><input id="txtUserName" runat="server" type="text" />
                <span>密码：</span><input id="txtPassword" runat="server" type="password" />
                <br />
                <asp:Button ID="btnSubmit" runat="server" Text="登录" OnClick="btnSubmit_Click" />
            </fieldset>
        </div>
    </form>
</body>
</html>
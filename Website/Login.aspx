<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rewards For Racing</title>
    <link href="css/layout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="controls">
            <div class="login">
                <div class="username">Username:</div><div class="input"><asp:Textbox ID="txtUsername" TextMode="SingleLine" runat="server" ToolTip="Username" /></div>
                <div class="clear"></div>
                <div class="password">Password:</div><div class="input"><asp:Textbox ID="txtPassword" TextMode="Password" runat="server" ToolTip="Password" /></div>
            </div>
            <div class="action">
                <asp:Button Width="100%" Height="100%" ID="btnLogin" runat="server" Text="Login" ToolTip="Click to login" OnClick="btnLogin_Click" />
            </div>
        </div>
        <asp:Label ID="lblError" runat="server" Text="" CssClass="error" />
    </form>
</body>
</html>

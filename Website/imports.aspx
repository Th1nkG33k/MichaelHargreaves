<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imports.aspx.cs" Inherits="imports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Imports</title>
    <link href="css/layout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Heading">Imports</div>
        <hr />
        <div class="messsage">
            <asp:Label ID="lblMessage" runat="server" Text="" />
        </div>
        <div>
            <asp:Repeater ID="rptImports" runat="server" OnItemCommand="rptImports_ItemCommand" OnItemDataBound="rptImports_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Email</th>
                                <th>Country</th>
                                <th></th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="itemRow">
                        <td class="datacellID"><asp:Literal ID="litID" runat="server" Text="1" /></td>
                        <td class="datacellFN"><asp:Literal ID="litFName" runat="server" Text="Michael" /></td>
                        <td class="datacellLN"><asp:Literal ID="litLName" runat="server" Text="Hargreaves" /></td>
                        <td class="datacellEM"><a id="mailLink" href="#"><asp:Literal ID="litEmail" runat="server" Text="MHargreaves@hotmail.com" /></a></td>
                        <td class="datacellCN"><asp:Literal ID="litCountry" runat="server" Text="United Kingdom" /></td>
                        <td class="datacell"><asp:LinkButton ID="btnCommand" runat="server" Text="delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this event?');" CausesValidation="true" UseSubmitBehavior="true" /></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="altitemRow">
                        <td class="datacellID"><asp:Literal ID="litID" runat="server" Text="1" /></td>
                        <td class="datacellFN"><asp:Literal ID="litFName" runat="server" Text="Michael" /></td>
                        <td class="datacellLN"><asp:Literal ID="litLName" runat="server" Text="Hargreaves" /></td>
                        <td class="datacellEM"><a id="mailLink" href="#"><asp:Literal ID="litEmail" runat="server" Text="MHargreaves@hotmail.com" /></a></td>
                        <td class="datacellCN"><asp:Literal ID="litCountry" runat="server" Text="United Kingdom" /></td>
                        <td class="datacell"><asp:LinkButton ID="btnCommand" runat="server" Text="delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this event?');" CausesValidation="true" UseSubmitBehavior="true"/></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="commit">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel Changes" OnClick="btnCancel_Click" />
                <asp:Button ID="btnCommit" runat="server" Text="Commit Changes" OnClick="btnCommit_Click" />
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="WorkshopBrowser.Master" AutoEventWireup="true" CodeBehind="WorkshopDefault.aspx.cs" Inherits="EventCheckIn.WorkshopDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">
    <div id="body">
        <!-- Responsive Sections -->
        <section class="row single guttered marginalize-ends">
            <div class="column one">
                <h1>Login</h1>
                <table>
                    <tr>
                        <td class="login">WSU Network ID:<br />
                            (myWSU username)</td>
                        <td>
                            <asp:TextBox ID="tbUserName" runat="server" Width="125px"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="tbUserName" ErrorMessage="Blank usernames are not allowed."
                                SetFocusOnError="true"
                                Style="color: #FF3300; font-size: small; font-weight: 700; font-style: italic"
                                Text="**"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td>
                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="tbPassword"
                                ErrorMessage="Blank passwords are not allowed." SetFocusOnError="true"
                                Style="color: #FF3300; font-size: small; font-weight: 700; font-style: italic" Text="**"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblError" runat="server" Style="color: #FF0000; font-family: 'Lucida Sans Unicode'"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click"
                                Text="Login" Height="37px" Width="125px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="3">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                DisplayMode="List" HeaderText="The following errors occured:"
                                ShowMessageBox="false" Style="color: #FF3300; font-weight: 700" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <a href="https://webutil.wsu.edu/apps/myNetworkProfileHelp/forgotuserid.aspx"
                                target="_top">What is my User ID?</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <a href="https://webutil.wsu.edu/apps/password/" target="_top">What is my 
                      Password?</a></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <br />
                If you experience technical difficulties with this application, please contact
      <a href="mailto:ascc.tech.support@wsu.edu">ascc.tech.support@wsu.edu.</a>.
      <br />
                <br />
            </div>
        </section>
    </div>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="EventCheckIn.Confirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="confirmation" style="font-size:25px; text-align:center">
          Thank you <asp:Literal ID="litFirstName" runat="server"></asp:Literal>!
          You've checked in successfully.
          <br />
          <br />
          Please click <asp:LinkButton ID="lbRefresh" runat="server" OnClick="lbRefresh_Click">Here</asp:LinkButton> if the page is not redirecting in 3 seconds.
          </div>
    </div>
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationGradFair.aspx.cs" Inherits="EventCheckIn.ConfirmationGradFair" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="confirmation" style="font-size:25px; text-align:center">          
          <asp:Literal ID="litFirstName" runat="server"></asp:Literal>&nbsp;<br />
          Is this you?<br />
          <asp:Button ID="btnConfirm" runat="server" Text="YES" Width="100px" OnClick="btnConfirm_Click" />
          &nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnStartOver" runat="server" Text="NO" Width="100px" OnClick="btnStartOver_Click" />
          <br />
          <%--Please click <asp:LinkButton ID="lbRefresh" runat="server" OnClick="lbRefresh_Click">Here</asp:LinkButton> if the page is not redirecting in 3 seconds.--%>
          &nbsp;</div>
    </div>
    </form>
</body>
</html>

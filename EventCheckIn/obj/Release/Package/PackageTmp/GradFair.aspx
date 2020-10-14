<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradFair.aspx.cs" Inherits="EventCheckIn.GradFair" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kiosk | ASCC</title>

    <link rel="stylesheet" type="text/css" href="style.css"/>

     <%--Use FastClick (https://github.com/ftlabs/fastclick) to prevent 300 ms delay for all buttons on iPad--%>
    <script type='application/javascript' src='/Scripts/fastclick.js'></script>

    <script type="text/javascript">
        window.addEventListener('load', function () {
            FastClick.attach(document.body);
        }, false);

        function addCode(key) {
            var code = document.getElementById("txtWSUID");
            if (code.value.length < 9) {
                if (key == "-1") // Backspace
                    code.value = code.value.slice(0, -1);
                else
                    code.value = code.value + key;
            }
            if (code.value.length == 9) {
                if (key == "-1")
                    code.value = code.value.slice(0, -1);
            }
        }

        function emptyCode() {
            document.getElementById("txtWSUID").value = "";
        }
</script>    

    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            font-size: x-large;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlCheckIn" runat="server">
          <div id="checkin" onload="emptyCode()">
              <asp:Label ID="lbEventTitle" runat="server"></asp:Label>
              <br />
              Please enter your WSU ID:
              <br /> <br />
              <input type="text" name="code" value="" maxlength="9" class="input" id="txtWSUID" runat="server"/>
              <br />
              <asp:RequiredFieldValidator ID="rfvWSUID" runat="server" ControlToValidate="txtWSUID" ForeColor="Red" ErrorMessage="WSU ID is required."></asp:RequiredFieldValidator>
              <br />
              <asp:RegularExpressionValidator ID="revMinNumbers" runat="server" ControlToValidate="txtWSUID" ErrorMessage="Must be at least 8 numbers." ForeColor="Red" ValidationExpression="^[0-9]{8,9}$"></asp:RegularExpressionValidator>
              <br />
              <asp:Label ID="lbError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
              <br />
              <table id="keypad" cellpadding="10" cellspacing="10">
	            <tr>
    	            <td onclick="addCode('1');">1</td>
                    <td onclick="addCode('2');">2</td>
                    <td onclick="addCode('3');">3</td>
                </tr>
                <tr>
    	            <td onclick="addCode('4');">4</td>
                    <td onclick="addCode('5');">5</td>
                    <td onclick="addCode('6');">6</td>
                </tr>
                <tr>
    	            <td onclick="addCode('7');">7</td>
                    <td onclick="addCode('8');">8</td>
                    <td onclick="addCode('9');">9</td>
                </tr>
                <tr>
    	            <td></td>
                    <td onclick="addCode('0');">0</td>
                    <td onclick="addCode('-1');"><img src="backspace.png" style="display:block;" alt="" height="40" width="90"/></td>
                </tr>
            </table>
              <br />
              <asp:Button class="submitbutton" ID="btnCheckIn" runat="server" Text="Check In" OnClick="btnCheckIn_Click" />
              <br />
              <br />             
               </div>
      </asp:Panel>
    </div>
          <asp:Panel ID="pnlCheckName" Visible="false" runat="server">
                <div class="auto-style1">
                    <span class="auto-style2">Is this you? </span>
                    <asp:Literal ID="litFirst" runat="server"></asp:Literal>
&nbsp;<asp:Literal ID="litLast" runat="server"></asp:Literal>
                    <asp:Button ID="btnFinalSubmit" runat="server" OnClick="btnFinalSubmit_Click" Text="Yes" Font-Size="X-Large" />
                    &nbsp;
                    <asp:Button ID="btnNotStudent" runat="server" OnClick="btnNotStudent_Click" Text="No" Font-Size="X-Large" />
                    <asp:Button ID="btnNotStudent2ndX" runat="server" OnClick="btnNotStudent2ndX_Click" Text="No" Visible="False" Font-Size="X-Large" />
                </div>
            </asp:Panel>
    </form>
    <!-- ANALYTICS -->
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-57794617-1', 'auto');
        ga('send', 'pageview');

</script>
</body>
</html>

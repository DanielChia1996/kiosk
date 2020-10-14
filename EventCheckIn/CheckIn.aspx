<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="EventCheckIn.CheckIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kiosk</title>
    <link rel="shortcut icon" href="https://repo.wsu.edu/spine/1/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="style.css" />

    <%--Use FastClick (https://github.com/ftlabs/fastclick) to prevent 300 ms delay for all buttons on iPad--%>
    <script src="Scripts/jquery-2.1.1.min.js"></script>
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

        document.addEventListener('keydown', function (e) {
            var txtWsuId = document.getElementById('txtSwipeWSUID');
            var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;

            
            if (txtWsuId != null && iOS) {
                if (e.key == 'Enter') {
                    document.querySelector('form').submit();
                } else {
                    if (e.key.length > 1) {
                        // ignore control codes
                    } else {
                        txtWsuId.value += e.key;
                    }
                }
            }
        });
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 100px;
        }

        .auto-style2 {
            width: 100%;
        }

        .auto-style3 {
            height: 39px;
        }

        #txtSwipeWSUID {
            outline: none;
            border: 0;
            opacity: 0.1;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <asp:Panel ID="pnlCheckIn" runat="server">
                <div id="checkin" onload="emptyCode()">
                    <table class="auto-style2">
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="lbEventTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbError0" runat="server" ForeColor="Red" Visible="False">WSU ID is required.</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSwipe" runat="server">
                        Please swipe your Cougar Card<br />
                        <asp:TextBox ID="txtSwipeWSUID" runat="server" AutoCompleteType="Disabled" ForeColor="White" OnTextChanged="txtSwipeWSUID_TextChanged"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnForgotCougarCard" runat="server" class="submitbutton" UseSubmitBehavior="false" OnClick="btnForgotCougarCard_Click" Text="Forgot Cougar Card?" />
                    </asp:Panel>
                    <asp:Panel ID="pnlKeypad" runat="server" Visible="False" Height="534px">
                        Please enter your WSU ID:
                  <br />
                        <asp:Label ID="lbSwipeError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        <br />
                        <input type="text" ontextchange="txtWSUID_TextChanged" name="code" maxlength="9" value="" class="input" id="txtWSUID" runat="server" visible="True" />
                        <br />
                        <br />
                        <table id="keypad" cellpadding="10" cellspacing="10">
                            <tr>
                                <td class="auto-style1" onclick="addCode('1');">1</td>
                                <td onclick="addCode('2');">2</td>
                                <td onclick="addCode('3');">3</td>
                            </tr>
                            <tr>
                                <td class="auto-style1" onclick="addCode('4');">4</td>
                                <td onclick="addCode('5');">5</td>
                                <td onclick="addCode('6');">6</td>
                            </tr>
                            <tr>
                                <td class="auto-style1" onclick="addCode('7');">7</td>
                                <td onclick="addCode('8');">8</td>
                                <td onclick="addCode('9');">9</td>
                            </tr>
                            <tr>
                                <td class="auto-style1"></td>
                                <td onclick="addCode('0');">0</td>
                                <td onclick="addCode('-1');">
                                    <img src="backspace.png" style="display: block;" alt="" height="40" width="90" />
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnCheckIn" runat="server" class="submitbutton" OnClick="btnCheckIn_Click" Text="Check In" />
                        <asp:Button ID="btnBackToSwipe" runat="server" class="submitbutton"  OnClick="btnGoBack_Click" Text="Go Back" />
                    </asp:Panel>

                    <br />
                    <br />
                    <br />
                </div>
            </asp:Panel>
        </div>
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

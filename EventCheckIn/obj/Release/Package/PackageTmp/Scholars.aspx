<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scholars.aspx.cs" Inherits="EventCheckIn.Scholars" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>What Brings You In</title>
    <link rel="shortcut icon" href="https://repo.wsu.edu/spine/1/favicon.ico" />

    <link rel="stylesheet" type="text/css" href="style.css" />
    <style type="text/css">
        body {
            width: 100%;
            height: 100%;
        }

        .lbl {
            cursor: pointer;
            font-size: 24px;
            font-stretch: expanded;
        }

        ul {
            margin: 0;
            padding: 0;
            position: relative;
            padding: 10px 20px;
            width: 300px;
            transform: translateX(400px);
            left: 50%;
            transform: translate(-50%)
        }

            ul li {
                border-radius: 25px;
                list-style: none;
                position: relative;
                padding: 10px 0;
                border: 1px solid rgba(0,0,0,1);
                border-bottom: 1px solid rgba(0,0,0,.5);
                margin-top: 10px;
                margin-bottom: 5px;
                text-align: center;
                transform: translate()
            }

        .regular-checkbox {
            -webkit-appearance: none;
            background-color: #fafafa;
            border: 1px solid #cacece;
            box-shadow: 0 1px 2px rgba(0,0,0,0.05),inset 0px -15px 10px -12px rgba(0,0,0,0.05);
            padding: 9px;
            border-radius: 3px;
            display: inline-block;
            position: relative;
        }

            .regular-checkbox:active, .regular-checkbox:checked:active {
                box-shadow: 0 1px 2px rgba(0,0,0,0.05), inset 0px 1px 3px rgba(0,0,0,0.1);
            }

            .regular-checkbox:checked {
                background-color: #5d646e;
                border: 1px solid #adb8c0;
                box-shadow: 0 1px 2px rgba(0,0,0,0.05),inset 0px -15px 10px -12px rgba(0,0,0,0.05), inset 15px 10px -12px rgba(255,255,255,0.1);
                color: #99a1a7;
            }

                .regular-checkbox:checked:after {
                    background-color: #5d646e;
                    font-size: 14px;
                    position: absolute;
                    top: 0px;
                    left: 3px;
                    color: #99a1a7;
                }

        ul li .regular-checkbox:checked + li {
            background-color: blue;
        }
    </style>


</head>
<body>

    <form id="form1" runat="server">
        <div style="text-align: center;">
            <h2>What Scholar Groups Are You a Member of</h2>
            <p style="font-size: small">Select At Least One</p>
            <ul>
                <li id="li1" runat="server" style="cursor:pointer" visible="false">
                    <asp:Label runat="server" ID="lblOp1" CssClass="lbl" AssociatedControlID="cbOp1" Width="100%">
                        <asp:Literal ID="litOp1" runat="server"></asp:Literal>
                        <%-- <input type="checkbox" name="cbOp1" id="cbOp1" runat="server" class="regular-checkbox"/>--%>
                        <asp:CheckBox ID="cbOp1" runat="server" Style="visibility: hidden" />
                        <span></span>
                    </asp:Label>
                </li>
                <li id="li2" runat="server" style="cursor:pointer" visible="false">
                    <asp:Label runat="server" ID="lblOp2" CssClass="lbl" AssociatedControlID="cbOp2" Width="100%" hight="100%">
                        <asp:Literal ID="litOp2" runat="server"></asp:Literal>
                        <asp:CheckBox ID="cbOp2" runat="server" Style="visibility: hidden" />

                        <span></span>
                    </asp:Label>
                </li>
                <li id="li3" runat="server" style="cursor:pointer" visible="false">

                    <asp:Label runat="server" ID="lblOp3" CssClass="lbl" AssociatedControlID="cbOp3">
                        <asp:Literal ID="litOp3" runat="server"></asp:Literal>
                        <asp:CheckBox runat="server" ID="cbOp3" Style="visibility: hidden" />

                        <span></span>

                    </asp:Label>
                </li>
                <li id="li4" runat="server" style="cursor:pointer" visible="false">
                    <asp:Label runat="server" ID="lblOp4" CssClass="lbl" AssociatedControlID="cbOp4">
                        <asp:Literal ID="litOp4" runat="server"></asp:Literal>
                        <asp:CheckBox runat="server" ID="cbOp4" Style="visibility: hidden" />
                        <span></span>
                    </asp:Label>
                </li>
                <li id="li5" runat="server" style="cursor:pointer" visible="false">
                    <asp:Label runat="server" ID="lblOp5" CssClass="lbl" AssociatedControlID="cbOp5">
                        <asp:Literal ID="litOp5" runat="server"></asp:Literal>
                        <asp:CheckBox runat="server" ID="cbOp5" Style="visibility: hidden" />
                        <span></span>
                    </asp:Label>
                </li>
            </ul>

            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Confirm" CssClass="submitbutton" Style="position: absolute; top: 100%; left: 50%; transform: translate(-50%)" />

            <asp:Label runat="server" ID="lblError"></asp:Label>
        </div>
    </form>


    <script type="text/javascript">
        var cb1 = document.getElementById("cbOp1");
        var cb2 = document.getElementById("cbOp2");
        var cb3 = document.getElementById("cbOp3");
        var cb4 = document.getElementById("cbOp4");
        var cb5 = document.getElementById("cbOp5");

        if (cb1 != null) {
            listEventListener("li1", "cbOp1");
        }
        if (cb2 != null) {
            listEventListener("li2", "cbOp2");
        }
        if (cb3 != null) {
            listEventListener("li3", "cbOp3");
        }
        if (cb4 != null) {
            listEventListener("li4", "cbOp4")
        }
        if (cb5 != null) {
            listEventListener("li5", "cbOp5");
        }

        function listEventListener(listItem, checkbox) {
            document.getElementById(listItem).addEventListener("click", function () {
                var cb = document.getElementById(checkbox);
                if (cb.checked) {
                    this.style.backgroundColor = "white";
                    cb.checked = false;
                }
                else if (!cb.checked) {
                    this.style.backgroundColor = "#babdc2";
                    cb.checked = true;
                }
            });
        }


    </script>
</body>
</html>

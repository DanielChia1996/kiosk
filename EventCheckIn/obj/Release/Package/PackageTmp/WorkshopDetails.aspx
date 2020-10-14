<%@ Page Title="" Language="C#" MasterPageFile="~/WorkshopBrowser.Master" AutoEventWireup="true" CodeBehind="WorkshopDetails.aspx.cs" Inherits="EventCheckIn.WorkshopDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">

    <asp:FormView ID="FormView1" runat="server" DataKeyNames="workshopID" DataSourceID="WorkshopLinqDS" EmptyDataText="Workshop Not Found">
        <ItemTemplate>
            <div class="content-pane">
                <h2 class="page-title"><%# Eval("workshopName") %></h2>
                <div id="event_content">
                    <div class="event-pic">
                        <div id="blank-image" class="event-wrapper">
                            <div class="event-month">
                                <%#Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") %>
                            </div>
                            <div id="num-date" class="event-day">
                                <%#Convert.ToDateTime(Eval("workshopDate")).ToString("dd") %>
                            </div>

                        </div>
                    </div>
                    <div class="content_wrapper">
                        <div class="detail-wrapper">
                            <div class="align">
                                <%# Convert.ToDateTime(Eval("workshopDate")).DayOfWeek + ", " + Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") + " " + Convert.ToDateTime(Eval("workshopDate")).ToString("dd") + ", " +   Convert.ToDateTime(Eval("workshopDate")).ToString("yyyy") %>
                                <div class="list-item-subtext"><%#Convert.ToDateTime(Eval("WorkshopDate")).ToShortTimeString() %></div>
                            </div>
                        </div>
                        <div class="detail-wrapper">
                            <div class="align">
                                <%#Eval("workshopLocation") %>
                            </div>

                        </div>
                        <div class="detail-wrapper">
                            <asp:Label ID="lblAttendance" runat="server" CssClass="Attendance"></asp:Label>
                            <asp:Panel ID="pnlnotSignedin" runat="server" Visible="true"><a href="WorkshopDefault.aspx">Sign in to RSVP to this event</a></asp:Panel>
                            <asp:Panel ID="pnlRSVP" runat="server" Visible="false">
                                <asp:Button ID="btnRSVP" runat="server" Text="RSVP" CssClass="submitbutton" OnClick="btnRSVP_Click" />
                            </asp:Panel>
                            <asp:Panel ID="pnlAlreadyRSVP" runat="server" Visible="false">
                                <asp:Button ID="btnUnRSVP" runat="server" OnClick="btnUnRSVP_Click" Text="Unable to Attend" /><br />
                                <asp:Label runat="server" ID="lblAlreadyRSVP" Text="You Have Already Sent an RSVP for this Workshop"></asp:Label>
                            </asp:Panel>
                        </div>

                    </div>
                    <table>
                        <tr>
                            <td colspan="3">
                                <hr class="style-one" />
                            </td>
                        </tr>
                        <tr style="text-align: center">
                            <td colspan="3" style="font: 20px arial; color: black; align-self: center; align-content: center;">
                                <%# Eval("workshopDescription")%>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>

        </ItemTemplate>


    </asp:FormView>


    <%--    <asp:FormView ID="formWorkshopDetails" runat="server" DataKeyNames="workshopID" DataSourceID="WorkshopLinqDS" EmptyDataText="Workshop Not Found">
        <ItemTemplate>
            <table>
                <tr aria-multiline="false">
                    <td>
                        <h3><%# Eval("workshopName") %></h3>
                    </td>

                </tr>

                <tr>
                    <td style="width: 25%">
                        <strong>Location: </strong>
                    </td>
                    <td>
                        <%#Eval("workshopLocation") %>
                    </td>
                </tr>
                <tr>
                    <td><strong>Date and Time: </strong></td>
                    <td><%#Eval("workshopDate") %></td>
                </tr>
                <tr>
                    <td><strong>Available Seats: </strong></td>
                    <td><%#(Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("studentsRegistered"))).ToString() %></td>
                    <td></td>
                </tr>
        </ItemTemplate>
    </asp:FormView>--%>

    <asp:LinqDataSource ID="WorkshopLinqDS" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="workshops" Where="workshopID == @workshopID">
        <WhereParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="workshopID" QueryStringField="workshopID" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>

    <asp:Label ID="lblError" runat="server"></asp:Label>
</asp:Content>

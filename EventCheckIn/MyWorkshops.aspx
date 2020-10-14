<%@ Page Title="" Language="C#" MasterPageFile="~/WorkshopBrowser.Master" AutoEventWireup="true" CodeBehind="MyWorkshops.aspx.cs" Inherits="EventCheckIn.MyWorkshops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">

    <h2>My Workshops</h2>
    <asp:Panel ID="pnlUpcoming" runat="server">

        <h4>Upcoming Workshops</h4>
        <asp:GridView ID="WorkshopGV" runat="server" AutoGenerateColumns="False" DataKeyNames="workshopID" EmptyDataText="No Workshops Currently Available">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="list-item-container" ControlStyle-CssClass="">
                    <ItemTemplate>
                        <div class="list-item-container">

                            <div style="position: relative; height: 71px;">
                                <div style="transform: translate(0px,0px);">
                                    <div>
                                        <div class="list-item">
                                            <a href="WorkshopDetails.aspx?workshopID=<%#Eval("workshopID").ToString() %> " class="">
                                                <div>
                                                    <div class="list-item-picture-container">
                                                        <div class="list-item-month"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") %></div>
                                                        <div class="list-item-day"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("dd") %></div>
                                                    </div>
                                                    <div class="list-item-info">
                                                        <div class="list-item-title"><%#Eval("workshopName") %></div>
                                                        <div class="list-item-subtext"><%# Convert.ToDateTime(Eval("workshopDate")).ToShortTimeString() %></div>
                                                    </div>
                                                    <div class="list-item-info">
                                                        <div class="list-item-subtext">
                                                            <asp:Panel ID="pnlSeatsAvailable" runat="server">
                                                                <%# (Convert.ToInt32(Eval("Capacity")) == 0) ? "" :
                                                                    ((Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("StudentsRegistered"))) == 0) ? "Full" :
                                                                     "Seats Available:" + (Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("StudentsRegistered"))).ToString() %>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="list-item-subtext" style="position: relative">
                                                        </div>
                                                    </div>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlAttended" runat="server">
        <h4>Workshops Attended</h4>
        <asp:GridView ID="PastWorkshopGV" runat="server" EmptyDataText="You have not attended any Workshops" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="list-item-container">

                            <div style="position: relative; height: 71px;">
                                <div style="transform: translate(0px,0px);">
                                    <div>
                                        <div class="list-item">
                                            <a href="WorkshopDetails.aspx?workshopID=<%#Eval("workshopID").ToString() %> " class="">
                                                <div>
                                                    <div class="list-item-picture-container">
                                                        <div class="list-item-month"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") %></div>
                                                        <div class="list-item-day"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("dd") %></div>
                                                    </div>
                                                    <div class="list-item-info">
                                                        <div class="list-item-title"><%#Eval("workshopName") %></div>
                                                        <div class="list-item-subtext"><%# Convert.ToDateTime(Eval("workshopDate")).ToShortTimeString() %></div>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </asp:Panel>
    <asp:Panel ID="pnlMissed" runat="server">
        <h4>Workshops Missed</h4>
        <asp:GridView ID="MissedWorkshopsGV" runat="server" EmptyDataText="You have not missed any workshops" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="list-item-container">

                            <div style="position: relative; height: 71px;">
                                <div style="transform: translate(0px,0px);">
                                    <div>
                                        <div class="list-item">
                                            <a href="WorkshopDetails.aspx?workshopID=<%#Eval("workshopID").ToString() %> " class="">
                                                <div>
                                                    <div class="list-item-picture-container">
                                                        <div class="list-item-month"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") %></div>
                                                        <div class="list-item-day"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("dd") %></div>
                                                    </div>
                                                    <div class="list-item-info">
                                                        <div class="list-item-title"><%#Eval("workshopName") %></div>
                                                        <div class="list-item-subtext"><%# Convert.ToDateTime(Eval("workshopDate")).ToShortTimeString() %></div>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </asp:Panel>


</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="WorkshopBrowser.Master" AutoEventWireup="true" CodeBehind="WorkshopDashboard.aspx.cs" Inherits="EventCheckIn.WorkshopDashboard"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">


<%--    <asp:GridView ID="GVWorkshops" runat="server" AutoGenerateColumns="False" DataKeyNames="workshopID" DataSourceID="WorkshopsLinqDS" EmptyDataText="No Workshops Currently Available">
        <AlternatingRowStyle CssClass="gray-lighter-back at-large-max-width green-darkest-text padded" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="WorkshopDetails.aspx?workshopID=<%#Eval("workshopID").ToString() %> "><%# Eval("workshopName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="workshopLocation" SortExpression="workshopLocation" />
            <asp:BoundField DataField="workshopDate" SortExpression="workshopDate" />

            <asp:TemplateField>
                <ItemTemplate>
                    <p>Available Seats:   <%# (Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("StudentsRegistered"))).ToString() %> </p>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>--%>

    <asp:GridView ID="TestGV" runat="server" AutoGenerateColumns="False" DataKeyNames="workshopID" DataSourceID="WorkshopsLinqDS" EmptyDataText="No Workshops Currently Available">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="list-item-container" ControlStyle-CssClass="">
                <ItemTemplate>
                    <div class="list-item-container">

                        <div style="position: relative; height: 71px;">
                            <div style="transform: translate(0px,0px);">
                                <div>
                                    <div class="list-item">
                                        <a href="WorkshopDetails.aspx?workshopID=<%#Eval("workshopID").ToString() %> " class="">
                                            <div >
                                                <div class="list-item-picture-container">
                                                    <div class="list-item-month"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("MMM") %></div>
                                                    <div class="list-item-day"><%#Convert.ToDateTime(Eval("workshopDate")).ToString("dd") %></div>
                                                </div>
                                                <div class="list-item-info">
                                                    <div class="list-item-title"><%#Eval("workshopName") %></div>
                                                    <div class="list-item-subtext"><%# Convert.ToDateTime(Eval("workshopDate")).ToShortTimeString() %></div>
                                                </div>
                                                <div class="list-item-info">
                                                    <div class ="list-item-subtext"><asp:Panel ID="pnlSeatsAvailable" runat="server"> <%# (Convert.ToInt32(Eval("Capacity")) == 0) ? "" :
                                                                                                                                              ((Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("StudentsRegistered"))) == 0) ? "Full" :
                                                                                                                                                 "Seats Available:" + (Convert.ToInt32(Eval("Capacity")) - Convert.ToInt32(Eval("StudentsRegistered"))).ToString() %> </asp:Panel>
                                                    </div>
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
    <asp:LinqDataSource ID="WorkshopsLinqDS" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="workshops" OnSelecting="WorkshopsLinqDS_Selecting">
    </asp:LinqDataSource>

    

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Kiosk.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EventCheckIn.Dashboard" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divSAdminselection" runat="server" visible="true">
        <asp:RadioButtonList runat="server" ID="SRadioButtonSelection" AutoPostBack="true" OnSelectedIndexChanged="SRadioButtonSelection_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatColumns="2">
            <asp:ListItem Value="1" Selected="true">Only create kiosk</asp:ListItem>
            <asp:ListItem Value="0">Create Workshop and Kiosk</asp:ListItem>
        </asp:RadioButtonList>

    </div>


    <table>
        <tr>
            <td style="height: 69px">
                <strong>Event Category: </strong>

            </td>
            <td style="height: 69px">
                <asp:RadioButtonList ID="rbListCategory" runat="server" DataSourceID="LinqDSCategory" DataTextField="Category" DataValueField="Category" RepeatColumns="2" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbListCategory_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                <asp:LinqDataSource ID="LinqDSCategory" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="EventCategories" Where="IsActive == @IsActive" OrderBy="Category ASC">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="True" Name="IsActive" Type="Boolean" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="rbListCategory" ErrorMessage="**" ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:CheckBox ID="cbReinstatement" runat="server" Text="Reinstatement Related" Visible="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2">

                <table id="Scholars" runat="server" visible="false">
                    <tr>
                        <td>
                            <h5>Scholars</h5>
                        </td>
                    </tr>
                    <tr>
                        <td class="four-twelfths"><strong>Option 1:</strong></td>
                        <td class="eight-twelfths">
                            <asp:TextBox ID="tbScholarOp1" runat="server" Text="Achievers"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 2:</strong></td>
                        <td>
                            <asp:TextBox ID="tbScholarOp2" runat="server" Text="Governor's/Passport"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 3:</strong></td>
                        <td>
                            <asp:TextBox ID="tbScholarOp3" runat="server" Text="WISH"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 4:</strong></td>
                        <td>
                            <asp:TextBox ID="tbScholarOp4" runat="server" Text="WSOS"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 5:</strong></td>
                        <td>
                            <asp:TextBox ID="tbScholarOp5" runat="server" Text="Other"></asp:TextBox></td>
                    </tr>
                </table>

                <table runat="server" id="Activities" visible="false">
                    <tr>
                        <td class="four-twelfths">
                            <h5>Activities</h5>
                            <p style="font-size:small">If Option Not Needed, Leave Blank</p>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Option 1:</strong></td>
                        <td class="eight-twelfths">
                            <asp:TextBox runat="server" ID="tbActivityOp1" Text="Computer"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td><strong>Option 2:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp2" Text="Printing"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 3:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp3" Text="HW or Study"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 4:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp4" Text="Meetings"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 5:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp5" Text="Food"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 6:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp6" Text="Event"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><strong>Option 7:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp7" Text="Other"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Option 8:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp8" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Option 9:</strong></td>
                        <td>
                            <asp:TextBox runat="server" ID="tbActivityOp9" Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table runat="server" id="tblNonExtraPart1">
                    <tr>
                        <td class="four-twelfths">
                            <asp:Label runat="server" ID="lblEventdate"><strong>Event Date: </strong></asp:Label>
                        </td>
                        <td class="eight-twelfths">
                            <asp:TextBox ID="tbEventDate" runat="server" CssClass="js-date-picker"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEventDate" runat="server" ControlToValidate="tbEventDate" ForeColor="Red" ErrorMessage="**" ValidationGroup="submit"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                </table>
                <table id="tblTime" runat="server">
                    <tr>
                        <td class="four-twelfths">
                            <asp:Panel ID="pnlTime" runat="server" Visible="true">
                                <strong>Event Time:</strong>

                            </asp:Panel>
                        </td>
                        <td class="eight-twelfths">
                            <asp:Panel ID="pnlDDLTime" runat="server" Visible="true">
                                <asp:DropDownList ID="DDLtime" runat="server" DataSourceID="TimeDataSource" DataTextField="timeString" DataValueField="timeString"></asp:DropDownList>
                                <asp:LinqDataSource ID="TimeDataSource" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="timePickers">
                                </asp:LinqDataSource>
                                <asp:RadioButtonList runat="server" ID="AMPMbutton" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="AM" Selected="True">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table id="tblDateAndName" runat="server">
                    <tr>

                        <td class="four-twelfths"><strong>Show Date in Kiosk Title: </strong></td>
                        <td class="eight-twelfths">
                            <asp:CheckBox ID="cbShowDateLabel" runat="server" Text="Show above date in kiosk title." Visible="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Event Name: </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="tbEventName" runat="server" MaxLength="128" Width="300px"></asp:TextBox><%--class="eventtextbox" Width="700px"--%>
                            <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="tbEventName" ForeColor="Red" ErrorMessage="**" ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table id="tblWorskhopDetails" runat="server">
                    <tr>
                        <td class="four-twelfths">
                            <strong>Event Description:</strong>
                        </td>
                        <td class="eight-twelfths">
                            <asp:Panel ID="pnlEventDesc" runat="server" Visible="true">
                                <asp:TextBox ID="txtWorkshopDesc" TextMode="MultiLine" Columns="50" Rows="4" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="valInput"
                                    ControlToValidate="txtWorkshopDesc"
                                    ValidationExpression="^[\s\S]{0,1000}$"
                                    ErrorMessage="Please Enter a maximum of 1000 characters"
                                    Display="Dynamic">*</asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Event Location: </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventLocation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Event Capacity:</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventCapacity" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="valEventCapacity" runat="server"
                                ControlToValidate="txtEventCapacity"
                                ErrorMessage="Please only enter integers"
                                ValidationExpression="/^(\s*|\d+)$/">**</asp:RegularExpressionValidator>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="checkIsVisible" runat="server" Text="Event Visible to Students" />
                        </td>
                    </tr>

                </table>
                <table runat="server" id="tblEventDetails">
                    <tr>
                        <td class="four-twelfths" style="height: 70px"><strong>Event Term:</strong></td>
                        <td class="eight-twelfths" style="height: 70px">
                            <asp:DropDownList ID="ddlTerm" runat="server" DataSourceID="LinqDSTerm" DataTextField="Term" DataValueField="Term">
                            </asp:DropDownList>
                            <asp:LinqDataSource ID="LinqDSTerm" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="AcademicTerms" OrderBy="Term desc">
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="tblNonExtraPart2" runat="server">
                                <tr>
                                    <td class="four-twelfths">
                                        <strong>Presenter: </strong>
                                    </td>
                                    <td class="eight-twelfths">
                                        <asp:DropDownList ID="ddlPresenter" runat="server" DataSourceID="LinqDSPresenters" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                                        <asp:LinqDataSource ID="LinqDSPresenters" runat="server" ContextTypeName="EventCheckIn.EventCheckInDataClassesDataContext" EntityTypeName="" TableName="PresenterLists" Where="IsActive == @IsActive">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="true" Name="IsActive" Type="Boolean" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                        <asp:Button ID="btnAnotherPresenter" runat="server" Text="Add another presenter" OnClick="btnAnotherPresenter_Click" Style="margin-left: 50px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlNewPresenter" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td class="four-twelfths"></td>
                                        <td class="eight-twelfths">
                                            <asp:DropDownList ID="ddlPresenter1" runat="server" DataSourceID="LinqDSPresenters" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>

                    <tr>
                        <td class="four-twelfths"><strong>Use Qualtrics Redirect URL: </strong></td>
                        <td class="eight-twelfths">
                            <asp:RadioButton ID="rbQualtricsYes" GroupName="QualtricsYesNo" AutoPostBack="true" Text="Yes" runat="server" OnCheckedChanged="rbQualtricsYes_CheckedChanged" />
                            &nbsp;
                <asp:RadioButton ID="rbQualtricsNo" GroupName="QualtricsYesNo" Checked="true" AutoPostBack="true" Text="No" runat="server" OnCheckedChanged="rbQualtricsNo_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblQualtrics" runat="server" Font-Bold="true" Text="Enter Qualtrics URL:" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbQualtrics" Visible="false" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>(optional) Email event info to: </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="tbEmail" runat="server" MaxLength="50" Placeholder="                                      @" ForeColor="Gray" onblur="WaterMark(this, event);" onfocus="WaterMark(this, event);" Width="300px"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="revEmail" runat="server" 
                      ControlToValidate="tbEmail" ErrorMessage="Email has an incorrect format." ForeColor="Red"
                      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Default to Number Pad:</strong></td>
                        <td>
                            <asp:CheckBox ID="cbDefaultNumPad" runat="server" Text="Use swipe on kiosk if not checked." Visible="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Include Check-in Verification:</strong>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbSecurePin" runat="server" Text="Create secure pass-code to open kiosk" AutoPostBack="true" OnCheckedChanged="cbSecurePin_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel runat="server" ID="pnlAddedSecurity" Visible="false">
                                <table>
                                    <tr>
                                        <td class="two-twelfths">
                                            <strong>Pin: </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" TextMode="Password" ID="tbPassword" MaxLength="6" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="two-twelfths">
                                            <strong>Confirm Pin: </strong>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" TextMode="Password" ID="tbPasswordConfirmation" MaxLength="6"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <br />

                                <p style="font-size: small; text-align: center">(Pass code may contain numbers and characters, must be 4 to 6 characters long)</p>

                            </asp:Panel>
                        </td>
                    </tr>
                </table>

    </table>

    <br />
    <br />
    <table>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="btnSubmitWorkshop" CssClass="submitbutton" runat="server" Text="Submit Workshop and Kiosk" ValidationGroup="submit" Width="400px" OnClick="btnSubmitWorkshop_Click" Visible="false" />
                <asp:Button ID="btnSubmitEvent" class="submitbutton" runat="server" Text="Submit & Open Kiosk" OnClick="btnSubmitEvent_Click" ValidationGroup="submit" Width="400px" /><br />
                <br />
                <asp:Button ID="btnSubmitEventEnterAnother" class="submitbutton" runat="server" Text="Enter Another Kiosk" ValidationGroup="submit" OnClick="btnSubmitEventEnterAnother_Click" Width="400px" />
            </td>

        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
</asp:Content>

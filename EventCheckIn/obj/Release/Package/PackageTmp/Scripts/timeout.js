/* This script is for session time out
The page will pop up warning message after 95% of total time
and will redirect to TimeOut.aspx after 240 minutes(change the time
in SetPageTimes() function if needed) with no user's response*/

var diag = createNewDialog();
var msecPageLoad;
var msecTimeOut;
var msecWarning;
SessionTimerInit();

function SessionTimerInit()
{
    SetPageTimes();
    setTimeout("ShowPendingTimeoutDialog()", msecWarning);
}
function createNewDialog()
{
    return $('<div></div>');
}
function SetPageTimes()
{
    msecPageLoad = new Date().getTime();
    msecTimeOut = (240 * 60 * 1000); // The page will time out after 240 minutes
    msecWarning = (240  * 60 * 1000 * .95); // The warning message will pop up after passing 95% of the total time
}
function ShowPendingTimeoutDialog()
{
    diag.dialog(
    {
        autoOpen: false,
        title: "Session Expiring",
        position: { my: "center center", at: "center center", of: "#pagecontainer" },
        resizable: false,
        draggable: false,
        closeText: "Hide",
        modal: true,
        buttons:
        {
            Continue: function() { ResetTimeout(); }
        }
    });
    UpdateTimeoutMessage();
    diag.dialog("open");
}
// Call KeepAlive.ashx to keep the session alive when a user clicks "Continue"
function ResetTimeout()
{
    diag.dialog("close");
    $.ajax({
        url: 'KeepAlive.ashx'
    });
    SessionTimerInit();
}
function UpdateTimeoutMessage()
{
    var msecElapsed = (new Date().getTime()) - msecPageLoad;
    var timeLeft = msecTimeOut - msecElapsed; //time left in miliseconds
    if(timeLeft <= 0)
    {
        RedirectToTimeoutPage();
    }
    else
    {
        var minutesLeft = Math.floor(timeLeft / 60000);
        var secondsLeft = Math.floor((timeLeft % 60000) / 1000);
        var sMinutesLeft = ("00" + (minutesLeft).toString()).slice(-2) + ":";
        var sSecondsLeft = ("00" + (secondsLeft).toString()).slice(-2);
        diag.html("<p>Your session is about to expire. Please click 'Continue' to continue working.<br /><br />Time Remaining: " + sMinutesLeft + sSecondsLeft + "</p>");
        setTimeout("UpdateTimeoutMessage()", 50);
    }
}
// Redirect to logout.aspx with a query string to differentiate from a user's log out
function RedirectToTimeoutPage() {
    diag.dialog("close");
    window.location = "Logout.aspx?TimeOut=True";
}
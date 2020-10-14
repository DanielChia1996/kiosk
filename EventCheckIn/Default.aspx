﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventCheckIn.Default" %>

<!DOCTYPE html>
<!--[if lt IE 7]> <html class="no-svg lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]><html class="no-svg lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]><html class="no-svg lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!-->
<html lang="en">
<!--<![endif]-->

<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="keywords" content="WSU, Washington State, Washington State University " />
    <meta charset="utf-8">
    <title>Kiosk | ASCC</title>

    <!-- FAVICON -->
    <link rel="shortcut icon" href="https://repo.wsu.edu/spine/1/favicon.ico" />

    <!-- STYLESHEETS -->
    <link href="https://repo.wsu.edu/spine/1/spine.min.css" rel="stylesheet" type="text/css" />
    <!-- Your custom stylesheets here -->
    <link rel="stylesheet" type="text/css" href="style.css" />

    <!-- RESPOND -->
    <meta name="viewport" content="width=device-width, user-scalable=yes">

    <!-- SCRIPTS -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js"></script>
    <script src="https://repo.wsu.edu/spine/1/spine.min.js"></script>

    <!-- COMPATIBILITY -->
    <!--[if lt IE 9]><script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <noscript>
        <style>
            #spine .spine-sitenav ul ul li {
                display: block !important;
            }
        </style>
    </noscript>

    <!-- DOCS -->
    <link type="text/plain" rel="author" href="https://repo.wsu.edu/spine/1/authors.txt">
    <link type="text/html" rel="help" href="https://brand.wsu.edu">

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

</head>

<body class="">

    <!-- WRAPPING -->
    <div id="jacket">
        <div id="binder" class="hybrid">
            <!-- GRID BEHAVIOR: Default behavior can be changed by changing "hybrid" to "fixed" or "fluid". -->
            <!-- CONTENT -->

            <form id="form1" runat="server">

                <main>
                    <div id="header">
                        <h6>Academic Success and Career Center</h6>
                        <h1>Kiosk</h1>
                    </div>

                    <div id="body">
                        <!-- Responsive Sections -->
                        <section class="row single guttered marginalize-ends">
                            <div class="column one">
                                <h1>Login</h1>
                                <table>
                                    <tr>
                                        <td class="login">WSU Network ID:<br />
                                            (myWSU username)</td>
                                        <td>
                                            <asp:TextBox ID="tbUserName" runat="server" Width="125px"></asp:TextBox>
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="tbUserName" ErrorMessage="Blank usernames are not allowed."
                                                SetFocusOnError="true"
                                                Style="color: #FF3300; font-size: small; font-weight: 700; font-style: italic"
                                                Text="**"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Password:</td>
                                        <td>
                                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="tbPassword"
                                                ErrorMessage="Blank passwords are not allowed." SetFocusOnError="true"
                                                Style="color: #FF3300; font-size: small; font-weight: 700; font-style: italic" Text="**"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblError" runat="server" Style="color: #FF0000; font-family: 'Lucida Sans Unicode'"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click"
                                                Text="Login" Height="37px" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="3">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                DisplayMode="List" HeaderText="The following errors occured:"
                                                ShowMessageBox="false" Style="color: #FF3300; font-weight: 700" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style82">&nbsp;</td>
                                        <td class="style80">
                                            <a href="https://admission.wsu.edu/apply/mywsu/"
                                                target="_top">What is my User ID?</a></td>
                                    </tr>
                                    <tr>
                                        <td class="style82">&nbsp;</td>
                                        <td class="style80">
                                            <a href="https://login.wsu.edu/signin/forgot-password" target="_top">Forgot Password?</a></td>
                                        <td class="style75">&nbsp;</td>
                                        <td class="style75">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <br />
                                If you experience technical difficulties with this application, please contact
      <a href="mailto:ascc.tech.support@wsu.edu">ascc.tech.support@wsu.edu.</a>.
      <br />
                                <br />
                            </div>
                            <!--/column-->

                            <%--<div class="column two">

		            <aside>
				            <h2>Sidebar</h2>
				            <ul>
					            <li>Fight, fight, fight for Washington State! Win the victory!</li>
					            <li>Win the day for Crimson and Gray! Best in the West, we know you'll all do your best, so</li>
					            <li>On, on, on, on! Fight to the end! Honor and Glory you must win! So</li>
					            <li>Fight, fight, fight for Washington State and victory!</li>
					            <li>W-A-S-H-I-N-G-T-O-N-S-T-A-T-E-C-O-U-G-S! <strong>GO COUGS!!</strong></li>
				            </ul>
				            <small>The song appears in the 1985 film <i>Volunteers</i>, sung by John Candy's character Tommy Tuttle from Tacoma, Washington, and later used as a war cry by the Communists.</small>
		            </aside>

	            </div><!--/column-->--%>
                        </section>
                    </div>
                </main>

            </form>
            <!-- /CONTENT -->

            <!-- SPINE -->
            <div id="spine" class="spine-column shelved bleed dark">
                <!-- SPINE COLOR: Available spine colors are crimson, gray, gray-lightest, gray-lightly, gray-lighter, gray-light, gray-dark, gray-darker, gray-darkest. Replace the color class above with your preferred color if you want to override the default. -->
                <!-- SPINE BLEED: Remove "bleed" so that  -->
                <div id="glue" class="spine-glue">

                    <header class="spine-header">
                        <a href="http://www.wsu.edu/" id="wsu-signature" class="spine-signature">Washington State University</a>
                    </header>

                    <!-- ACTIONS -->
                    <section id="wsu-actions" class="spine-actions">

                        <!-- Tabs -->
                        <ul id="wsu-actions-tabs" class="spine-actions-tabs clearfix">
                            <%--<li id="wsu-search-tab" class="spine-search-tab closed"><button>Search</button></li>--%>
                            <li id="wsu-contact-tab" class="spine-contact-tab closed">
                                <button>Contact</button></li>
                            <%--<li id="wsu-share-tab" class="spine-share-tab closed"><button>Share</button></li>--%>
                            <%--<li id="wsu-print-tab" class="spine-print-tab closed"><button>Print</button></li>--%>
                        </ul>

                        <!-- Actions generated by spine.js end up here -->
                    </section>
                    <!--/#wsu-actions-->

                    <section id="spine-navigation" class="spine-navigation">
                        <nav class="spine-sitenav">
                            <ul>
                                <!-- 
			NAVIGATION: Your navigation list here
			• Active link should be denoted with an "active" or "current" link on the link's parent li element.
			• First link should be a home link (otherwise, in exceptional cases, add "homeless" class to #site nav)-->
                                <li><a href="http://ascc.wsu.edu">ASCC</a></li>
                            </ul>
                        </nav>

                        <%--<nav class="spine-offsitenav">
	<ul>
		<li><a href="http://www.example.com">Offsite Link</a></li>
		<li><a href="http://fonts.wsu.edu">Another</a></li>
	</ul>
</nav>--%>
                    </section>
                    <!-- #navigation -->

                    <!-- Social and Global Links -->
                    <footer class="spine-footer">
                        <nav id="wsu-social-channels" class="spine-social-channels">
                            <!--
		SOCIAL CHANNELS: You can replace default social channels with your own. Available channels are:
		• Facebook, Twitter, Youtube, Google+ (.googleplus-channel), Tumblr, Pinterest, Flickr, 
		• Example: <li class="pinterest-channel"><a href="http://pinterest.com/user">Pinterest</a></li>
		• Maximum number of channels is five -->
                            <ul>
                                <li class="facebook-channel"><a href="https://www.facebook.com/ascc.wsu">Facebook</a></li>
                                <li class="twitter-channel"><a href="https://twitter.com/ASCC_WSU">Twitter</a></li>
                                <%--<li class="youtube-channel"><a href="http://www.youtube.com/washingtonstateuniv">YouTube</a></li>--%>
                                <li class="pinterest-channel"><a href="http://www.pinterest.com/wsuascc/">Pinterest</a></li>
                                <%--<li class="directory-channel"><a href="http://social.wsu.edu">Directory</a></li>--%>
                            </ul>
                        </nav>
                        <nav id="wsu-global-links" class="spine-global-links">
                            <ul>
                                <li class="mywsu-link"><a href="http://my.wsu.edu/">myWSU</a></li>
                                <li class="access-link"><a href="http://access.wsu.edu/">Access</a></li>
                                <li class="policies-link"><a href="http://policies.wsu.edu/">Policies</a></li>
                                <li class="copyright-link"><a href="http://copyright.wsu.edu">&copy;</a></li>
                            </ul>
                        </nav>
                    </footer>

                </div>
                <!--/glue-->
            </div>
            <!--/spine-->
            <!-- /SPINE -->

        </div>
        <!--/cover-->
    </div>
    <!--/jacket-->
    <!-- /WRAPPING -->

    <div id="contact-details" itemscope itemtype="http://schema.org/Organization">
        <span itemprop="department" content="Academic Success and Career Center"></span>
        <span itemprop="name" content="Washington State University"></span>
        <div itemprop="address" itemscope itemtype="http://schema.org/PostalAddress">
            <!--<span itemprop="location" content="Lighty Student Services Building Room 180"></span>-->
            <span itemprop="streetAddress" content="Lighty Student Services Building Room 180"></span>
            <span itemprop="addressLocality" content="Pullman"></span>
            <span itemprop="addressRegion" content="WA"></span>
            <span itemprop="postalCode" content="99164"></span>
        </div>
        <span itemprop="telephone" content="509-335-6000"></span>
        <span itemprop="email" content="ascc@wsu.edu"></span>
        <!--<span itemprop="contactPoint" content="http://ascc.wsu.edu/"></span>-->
        <span itemprop="url" content="http://ascc.wsu.edu/"></span>
    </div>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2_downline.aspx.cs" Inherits="PlexusDataHelper.WebForm2_downline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Plexus Reporter</title>
    <meta name="description" content="website description" />
    <meta name="keywords" content="website keywords, website keywords" />
    <meta http-equiv="content-type" content="text/html; charset=windows-1252" />
    <link type='text/css' href='css/demo.css' rel='stylesheet' media='screen' />
    <link type='text/css' href='css/basic.css' rel='stylesheet' media='screen' />
    <link rel="stylesheet" type="text/css" href="style/style.css" />
    
    <style id="cssStyle" type="text/css" media="all">
        .CS {
            background-color: #ba1e4a;
            color: #ffffff;
            border: 1px solid #ba1e4a;
            font: Verdana 10px;
            padding: 1px 4px;
            font-family: Palatino Linotype, Arial, Helvetica, sans-serif;
        }
    </style>

</head>

<body>
    <div id="main">
        <div id="header">
            <div id="logo">
                <div id="logo_text">
                    <h1><a href="index.aspx">plexus<span class="logo_colour">Reporter</span></a></h1>
                    <h2>For Plexus Ambassadors on the Darby Team</h2>
                </div>
            </div>
            <div id="menubar">
                <ul id="menu">
                    <li><a href="index.aspx">Home</a></li>
                    <li class="selected"><a href="WebForm2_downline.aspx">Downline</a></li>
                </ul>
            </div>
        </div>
        <div id="site_content">
            <div id="content" class="gridview">
                <form id="form1" method="post" enctype="multipart/form-data" runat="server">

                    <asp:TreeView ID="TreeView_plexTree" runat="server" ImageSet="Simple">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>

                </form>
            </div>
        </div>
        <div id="footer">
            <p><a href="index.aspx">Home</a> | <a href="mailto:N8Darby@gmail.com">Email Requests</a></p>
        </div>
    </div>
<script type='text/javascript' src='js/jquery.js'></script>
<script type='text/javascript' src='js/jquery.simplemodal.js'></script>
<script type='text/javascript' src='js/basic.js'></script>

</body>
</html>
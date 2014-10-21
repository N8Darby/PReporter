<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PlexusDataHelper.WebForm1" %>
<%@ Import Namespace="System.IO" %>

<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
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
                    <li class="selected"><a href="index.aspx">Home</a></li>
                    <li><a href="WebForm2_downline.aspx">Downline</a></li>
                </ul>
            </div>
        </div>
        <div id="site_content">
            <div id="content">
                <form id="form1" method="post" enctype="multipart/form-data" runat="server">
                    <asp:Table ID="Table1" runat="server" CssClass="table" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" Width="40%" CssClass="leftAlign">
                                Reporting month:
                                <asp:DropDownList CssClass="dropdown"
                                            ID="monthPicker"
                                            AutoPostBack="false"  
                                            runat="server">
                                            <asp:ListItem Value="1" Text="January" />
                                            <asp:ListItem Value="2" Text="February" />
                                            <asp:ListItem Value="3" Text="March" />
                                            <asp:ListItem Value="4" Text="April" />
                                            <asp:ListItem Value="5" Text="May" />
                                            <asp:ListItem Value="6" Text="June" />
                                            <asp:ListItem Value="7" Text="July" />
                                            <asp:ListItem Value="8" Text="August" />
                                            <asp:ListItem Value="9" Text="September" />
                                            <asp:ListItem Value="10" Text="October" />
                                            <asp:ListItem Value="11" Text="November" />
                                            <asp:ListItem Value="12" Text="December" />
                                        </asp:DropDownList>
                                        <asp:DropDownList CssClass="dropdown"
                                            ID="yearPicker"
                                            AutoPostBack="false" 
                                            runat="server">
                                            <asp:ListItem Value="2010" Text="2010" />
                                            <asp:ListItem Value="2011" Text="2011" />
                                            <asp:ListItem Value="2012" Text="2012" />
                                            <asp:ListItem Value="2013" Text="2013" />
                                            <asp:ListItem Value="2014" Text="2014" />
                                            <asp:ListItem Value="2015" Text="2015" />
                                            <asp:ListItem Value="2016" Text="2016" />
                                            <asp:ListItem Value="2017" Text="2017" />
                                            <asp:ListItem Value="2018" Text="2018" />
                                            <asp:ListItem Value="2019" Text="2019" />
                                            <asp:ListItem Value="2020" Text="2020" />
                                        </asp:DropDownList> &nbsp;
                                <input type="submit" id="Submit_SetReporingMonth" value="Set Month" runat="server" />
                                &nbsp;
                                <input type="submit" id="Submit_SetReporingMonth_NOW" value="Set Month to Now" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell Width="15%" CssClass="rightAlign">
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="leftAlign" ColumnSpan="2">
                                <asp:FileUpload ID="FileUpload1" CssClass="CS" runat="server" />
                                <input type="submit" id="NewUpload" value="Upload New File" runat="server" visible="false"/>
                                &nbsp;
                                <input type="submit" id="Submit1" value="Upload" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="rightAlign">
                                <div id='Div2'>
		                            <div id='basic-modal'>
                                        <asp:DropDownList CssClass="dropdown"
                                            ID="DropDownList_topReports"
                                            AutoPostBack="true" 
                                            OnTextChanged="DropDownList_topReports_SelectedIndexChanged"
                                            AppendDataBoundItems="true" 
                                            runat="server">
                                            <asp:ListItem Value="sa" Text="-- Show all --" />
                                            <asp:ListItem Value="t25pv" Text="Top 25 Highest PV" />
                                            <asp:ListItem Value="sc" Text="So Close" />
                                        </asp:DropDownList>
			                            <a href='#' class='basic'><asp:Image ID="fbImage" runat="server" ImageUrl="style/fb.gif" /></a>
		                            </div>
                                </div>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="leftAlign">
                                Number of rows to show:
								<asp:DropDownList ID="dropDownRecordsPerPage" runat="server" OnInit="dropDownRecordsPerPage_Init"
                                    AutoPostBack="true" OnSelectedIndexChanged="dropDownRecordsPerPage_SelectedIndexChanged" AppendDataBoundItems="true"
                                    Style="text-align: right;">
                                    <asp:ListItem Value="10" Text="10" />
                                    <asp:ListItem Value="25" Text="25" />
                                    <asp:ListItem Value="50" Text="50" />
                                    <asp:ListItem Value="100" Text="100" />
                                    <asp:ListItem Value="All" Text="All" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell CssClass="centerAlign">
                                <asp:Label ID="Label_FileName" runat="server" CssClass="N8Label" Text="<%= FileName %>"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="rightAlign">
                                <asp:Label ID="Label2" runat="server" CssClass="right" Text="Total Points: ">Total Points:
                                    <asp:Label ID="Label_TotPoints" CssClass="leftAlign" runat="server" Text="<%= TotalPoints %>"></asp:Label>
                                </asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:GridView ID="GridView_plexRecords"
                        runat="server"
                        EmptyDataText="No data available."
                        EnableSortingAndPagingCallbacks="False"
                        OnSorting="GridView_plexRecords_Sorting"
                        OnRowCreated="GridView_plexRecords_RowCreated"
                        PageSize="20"
                        OnRowDataBound = "OnRowDataBound"
                        AutoGenerateColumns="False"
                        OnPageIndexChanging="GridView_plexRecords_PageIndexChanging"
                        AllowSorting="True"
                        AllowPaging="True"
                        CssClass="gridview">
                        <Columns>
                            <asp:BoundField DataField="Level" ItemStyle-CssClass="TACenter" HeaderText="Level" SortExpression="Level"></asp:BoundField>
                            <asp:BoundField DataField="AmbNum" ItemStyle-CssClass="TACenter" HeaderText="Ambassador Number" SortExpression="AmbNum"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="PayLvl" ItemStyle-CssClass="TACenter" HeaderText="Pay Level" SortExpression="PayLvl"></asp:BoundField>
                            <asp:BoundField DataField="JoinDate" HeaderText="Join Date" SortExpression="JoinDate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="Points" ItemStyle-CssClass="TACenter" HeaderText="Points" SortExpression="Points"></asp:BoundField>
                            <asp:BoundField DataField="Cq" ItemStyle-CssClass="TACenter" HeaderText="CQ" SortExpression="Cq"></asp:BoundField>
                            <asp:BoundField DataField="Active" ItemStyle-CssClass="TACenter" HeaderText="Active" SortExpression="Active"></asp:BoundField>
                            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank"></asp:BoundField>
                            <asp:BoundField DataField="Customers" ItemStyle-CssClass="TACenter" HeaderText="Customers" SortExpression="Customers"></asp:BoundField>
                            <asp:BoundField DataField="Pv" ItemStyle-CssClass="TARight" HeaderText="PV" SortExpression="Pv" DataFormatString="{0:c}"></asp:BoundField>
                            <asp:BoundField DataField="Phone" ItemStyle-CssClass="TACenter" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
                            <asp:BoundField DataField="TotalPoints" HeaderText="Total Points" SortExpression="TotalPoints"></asp:BoundField>
                        </Columns>
                    </asp:GridView>

                    <div id='Div1'>
		                <!-- modal content -->
		                <div id="basic-modal-content">
			                <h3> <%= DropDownList_topReports.SelectedItem %></h3>
			                <p>Copy & Paste the data below to post to Facebook</p>
                            
                                <table>
                                    <tr>
                                        <th>Level 1</th>
                                        <th>Levels 1-4</th>
                                        <th>Levels 1-7</th>
                                        <th>Levels 1-12</th>
                                    </tr>
                                    <tr>
                                        <td class="leftAlignTop">
                                            <%= Session["top25_1"] %>
                                        </td>
                                        <td class="leftAlignTop">
                                            <%= Session["top25_1_4"] %>
                                        </td>
                                        <td class="leftAlignTop">
                                            <%= Session["top25_1_7"] %>
                                        </td>
                                        <td class="leftAlignTop">
                                            <%= Session["top25_1_12"] %>
                                        </td>
                                    </tr>

                                </table>
                                
		                </div>

		                <!-- preload the images -->
		                <div style='display:none'>
			                <img src='css/x.png' alt='' />
		                </div>
	                </div>
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
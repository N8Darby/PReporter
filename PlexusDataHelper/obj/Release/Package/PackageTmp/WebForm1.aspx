<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PlexusDataHelper.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
    <div>
        <input type="file" id="File1" name="File1" runat="server" />
        <input type="submit" id="Submit1" value="Upload" runat="server" />&nbsp;
         <br/>
         <br/>
        <asp:GridView ID="GridView_plexRecords" 
            runat="server" 
            emptydatatext="No data available." 
            EnableSortingAndPagingCallbacks="False" 
            OnSorting="GridView_plexRecords_Sorting" 
            OnRowCreated="GridView_plexRecords_RowCreated"
            PageSize="20" 
            AutoGenerateColumns="False" 
            OnPageIndexChanging="GridView_plexRecords_PageIndexChanging" 
            AllowSorting="True" 
            AllowPaging="True">

            <Columns>
                <asp:BoundField DataField="Level" ItemStyle-HorizontalAlign="Center" HeaderText="Level" SortExpression="Level">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="AmbNum" ItemStyle-HorizontalAlign="Center" HeaderText="Ambassador Number" SortExpression="AmbNum">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="PayLvl" ItemStyle-HorizontalAlign="Center" HeaderText="Pay Level" SortExpression="PayLvl">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="JoinDate" HeaderText="Join Date" SortExpression="JoinDate" DataFormatString="{0:MM/dd/yyyy}" >
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Points" ItemStyle-HorizontalAlign="Center" HeaderText="Points" SortExpression="Points">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cq" ItemStyle-HorizontalAlign="Center" HeaderText="CQ" SortExpression="Cq">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Active" ItemStyle-HorizontalAlign="Center" HeaderText="Active" SortExpression="Active">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Customers" ItemStyle-HorizontalAlign="Center" HeaderText="Customers" SortExpression="Customers">
                    <HeaderStyle HorizontalAlign="Left" Width="125px" />
                </asp:BoundField>
                <asp:BoundField DataField="Pv" ItemStyle-HorizontalAlign="Right" HeaderText="PV" SortExpression="Pv" DataFormatString="{0:c}">
                    <HeaderStyle HorizontalAlign="Left" Width="125px" />
                </asp:BoundField>
                <asp:BoundField DataField="Phone" ItemStyle-HorizontalAlign="Center" HeaderText="Phone" SortExpression="Phone">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                </asp:BoundField>
            </Columns>

        </asp:GridView>
        </div>
       

        <asp:DropDownList ID="dropDownRecordsPerPage" runat="server" CssClass="text-input" OnInit="dropDownRecordsPerPage_Init" 
            AutoPostBack="true" OnSelectedIndexChanged="dropDownRecordsPerPage_SelectedIndexChanged" AppendDataBoundItems="true"
            Style="text-align: right;">
            <asp:ListItem Value="10" Text="10" />
            <asp:ListItem Value="25" Text="25" />
            <asp:ListItem Value="50" Text="50" />
            <asp:ListItem Value="100" Text="100" />
            <asp:ListItem Value="All" Text="All" />
        </asp:DropDownList>

    </form>
</body>
</html>

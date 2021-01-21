<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="EvaluationReport.aspx.cs" Inherits="Medinet.Reports.EvaluationReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Import Namespace="ViewRes.Scripts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>   
        <br />
        <asp:Label ID="Label1" runat="server" Text="Test: "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server"  
            AppendDataBoundItems="True" AutoPostBack="True"
            DataSourceID="ObjectDataSource2" DataTextField="TestName" 
            DataValueField="TestID" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
    </div>
    <div align="center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="730px" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1070px" 
            DocumentMapWidth="100%">
            <LocalReport ReportPath="Reports\EvaluationReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="EvaluationDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
    <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%$RouteUrl:routename=default,controller=Home,action=Index%>"></asp:HyperLink>
    </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetEvaluations" 
        TypeName="MedinetClassLibrary.Services.Reports.EvaluationsReportsServices">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="test_id" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetTests" 
        TypeName="MedinetClassLibrary.Services.Reports.EvaluationsReportsServices">
    </asp:ObjectDataSource>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestByCompanyReport.aspx.cs" Inherits="Medinet.Reports.TestByCompanyReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server"  AppendDataBoundItems="True" AutoPostBack="True"
            DataSourceID="ObjectDataSource2" DataTextField="CompanyName" DataValueField="CompanyID" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
            DataSourceID="ObjectDataSource3" DataTextField="QuestionnaireName" DataValueField="QuestionnaireID" 
            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1056px" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="780px" 
            DocumentMapWidth="100%">
            <LocalReport ReportPath="Reports\TestByCompanyReport.rdlc" 
                EnableExternalImages="True">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="TestDataSet" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="HeaderDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <div align="center">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%$RouteUrl:routename=default,controller=Home,action=Index%>"></asp:HyperLink></li>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetTests" 
            TypeName="MedinetClassLibrary.Services.Reports.TestsByCompaniesReportsServices">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="company_id" 
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="DropDownList2" Name="questionnaire_id" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetCompanies" 
        TypeName="MedinetClassLibrary.Services.Reports.TestsByCompaniesReportsServices">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
        SelectMethod="GetQuestionnaires" 
        TypeName="MedinetClassLibrary.Services.Reports.TestsByCompaniesReportsServices">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
        SelectMethod="GetHeaderData" 
        TypeName="MedinetClassLibrary.Services.Reports.TestsByCompaniesReportsServices">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="company_id" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownList2" Name="questionnaire_id" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    </form>
</body>
</html>

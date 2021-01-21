<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HRCompanyQuestionnaireReport.ascx.cs" Inherits="Medinet.Reports.HRCompanyQuestionnaireReport1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="1056px" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
    Width="730px" DocumentMapWidth="100%">
            <LocalReport ReportPath="Reports\HRCompanyQuestionnaireReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                        Name="QuestionnaireDataSet" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <br />
        <div align="center">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%$RouteUrl:routename=default,controller=Home,action=Index%>"></asp:HyperLink></li>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetQuestionnaires" 
            TypeName="MedinetClassLibrary.Services.Reports.HRCompanyQuestionnairesReportsServices">
        </asp:ObjectDataSource>
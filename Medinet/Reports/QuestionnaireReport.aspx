<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionnaireReport.aspx.cs" Inherits="Medinet.Reports.QuestionnaireReport1" %>
<%@ Register src="HRAdministratorQuestionnaireReport.ascx" tagname="HRAdministratorQuestionnaireReport" tagprefix="uc1" %>
<%@ Register src="HRCompanyQuestionnaireReport.ascx" tagname="HRCompanyQuestionnaireReport" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

        <head id="Head1" runat="server">
            <title></title>
        </head>
<body><div id="report">
    <form id="form1" runat="server" style="vertical-align: middle; text-align: center; background-position: center center">
    <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:HRAdministratorQuestionnaireReport ID="HRAdministratorQuestionnaireReport1" 
            runat="server" />
        <uc2:HRCompanyQuestionnaireReport ID="HRCompanyQuestionnaireReport1" 
            runat="server" />
    </div>
    </form></div>
</body>
</html>



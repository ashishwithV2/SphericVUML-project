<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceCall.aspx.cs" Inherits="IndependentPlannerML.ServiceCall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="example" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"  ShowHeaderWhenEmpty="true" >
            <Columns >
                <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center"  >
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
           
        </asp:TemplateField>
                <asp:BoundField DataField="platform" HeaderText="Platform"  />
                <asp:BoundField DataField="scoredlabels" HeaderText="Yes/No" />
                <asp:BoundField DataField="scoredprobabilities" HeaderText="Confidence" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

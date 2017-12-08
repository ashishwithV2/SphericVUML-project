<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndependentPlannerAnalysis.aspx.cs" Inherits="IndependentPlannerML.IndependentPlannerAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=lsthdsd]').multiselect({
            includeSelectAllOption: true
        });
        $('[id*=LstTerritories]').multiselect({
            includeSelectAllOption: true
        });
        $('[id*=LstHotel]').multiselect({
            includeSelectAllOption: true
        });
        $('[id*=Lstselect]').multiselect({
            includeSelectAllOption: true
        });
        

        
      
    });
   
</script>
</head>
<body>
    <form id="form1" runat="server">
    <br /><br />
    
        
        <br />
         <div>
            <asp:TextBox ID="TxtMode" runat="server" TextMode="MultiLine" Width="649px"></asp:TextBox>
        </div>
        <br />
         <div>
            <asp:TextBox ID="TxtAccuraccy" runat="server"></asp:TextBox>
        </div>

        
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Licensor"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
           
            <tr>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Release"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server"  AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="HD/SD"></asp:Label></td>
                <td>
                    <asp:ListBox ID="lsthdsd" runat="server" SelectionMode="Multiple">
                    <asp:ListItem Text="HD" Value="HD" />
                    <asp:ListItem Text="SD" Value="SD" />
                     </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Territories"></asp:Label></td>
                <td>
                    <asp:ListBox ID="LstTerritories" runat="server" SelectionMode="Multiple">
                    <asp:ListItem Text="US" Value="US" />
                    <asp:ListItem Text="CAN" Value="CAN" />
                    <asp:ListItem Text="US,CAN" Value="US,CAN" />
                    <asp:ListItem Text="US,CAN,CARIB" Value="US,CAN,CARIB" />
                     </asp:ListBox>
                </td>
            </tr>
              <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Hotel"></asp:Label></td>
                <td>
                     <asp:ListBox ID="LstHotel" runat="server" SelectionMode="Multiple">
                    <asp:ListItem Text="Yes" Value="Yes" />
                    <asp:ListItem Text="No" Value="No" />
                    <asp:ListItem Text="Blanks" Value="Blanks" />
                    </asp:ListBox>
                </td>

            </tr>
              <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Select"></asp:Label></td>
                <td>
                     <asp:ListBox ID="Lstselect" runat="server" SelectionMode="Multiple">
                    <asp:ListItem Text="Y" Value="Y" />
                    <asp:ListItem Text="N" Value="N" />
                   
                    </asp:ListBox>
                </td>

            </tr>
           
                 <asp:TextBox ID="txtplatform" runat="server" ></asp:TextBox>
            </table>
         <div>
         <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" /><br /><br />
         <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    </div>
        <div>
            <asp:Button ID="BtnPlatform" runat="server" Text="Movies Details" OnClick="BtnPlatform_Click"  /><br /><br />
        </div>

        <div>

            <asp:Button ID="Button1" runat="server" Text="FinalUpdatedPlatform" OnClick="Button1_Click" />
        </div>

        <div>
           
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Platform" DataField="Platform" />
                        <asp:BoundField HeaderText="Scored Labels" DataField="ScoredLabels" />
                        <asp:BoundField HeaderText="Scored Probabilities" DataField="ScoredProbabilities" />
                    </Columns>
                </asp:GridView>
        </div>
        
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneratePCTsWithResult.aspx.cs" Inherits="IndependentPlannerML.GeneratePCTsWithResult" %>

<!DOCTYPE html>

<!--html xmlns="http://www.w3.org/1999/xhtml"-->

<html lang="en">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Planner Details</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.min.js"></script>


<style type="text/css">
    .bs-example{
    	margin: 100px;
        text-align:center;

    }
   th.dt-center, td.dt-center { text-align: center; }
   #example td:nth-child(3),
    #example td:nth-child(1),
    #example td:nth-child(4)
{
    text-align : center;
 
}
 #example th:nth-child(3),
    #example th:nth-child(1),
    #example th:nth-child(4)
{
    text-align : center;
 
}
 </style>

 





    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.responsive.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>

<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        //$(function () {
        //    $('[id*=example]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
        //        "responsive": true,
        //        "displayLength": 25,
               
        //        "sPaginationType": "full_numbers",
        //        "columnDefs": [
        //            {'orderable':false, "targets": [0,4]},
        //            ]
               
        //    });
        //});
        $(function () {
            $('[id*=Drphdsd]').multiselect({
                includeSelectAllOption: true
               //, nonSelectedText: '=======Select HD/SD Types===========',
            });
            $('[id*=DrpTerritories]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=Drprelease]').multiselect({
                includeSelectAllOption: true
            });

            
            


        });
    </script>

</head>
<body>
<form id="form1" runat="server" >
  <div class="container">
	<div class="row">
        <div class="col-sm-12">
            <br />
        </div>
        <br />
        <!-- panel preview -->
        <div class="col-sm-5">
            <br />
            <div class="panel panel-default">
                <div class="panel-body form-horizontal payment-form">
                    <div class="form-group">
                        <label for="concept" class="col-sm-3 control-label">Studio</label>
                        <div class="col-sm-9">
                          
                            
                            <asp:DropDownList ID="drpstudio" runat="server" CssClass="form-control"  required>
                             
                            </asp:DropDownList>


                        </div>
                    </div>
             
                    <div class="form-group">
                        <label for="description" class="col-sm-3 control-label">Release</label>
                        <div class="col-sm-9">
                          
                            <asp:ListBox ID="Drprelease" runat="server" CssClass="form-control" SelectionMode="Multiple" ></asp:ListBox>
                        </div>
                    </div>
                      <div class="form-group">
                        <label for="concept" class="col-sm-3 control-label">HD/SD</label>
                        <div class="col-sm-9">
                           
                     <asp:ListBox ID="Drphdsd" runat="server" SelectionMode="Multiple" CssClass="selectize-input items not-full has-options" >
               
                     </asp:ListBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label for="description" class="col-sm-3 control-label">Territories</label>
                        <div class="col-sm-9">
                          
                            <asp:ListBox ID="DrpTerritories" runat="server" CssClass="form-control" SelectionMode="Multiple"   ></asp:ListBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                             <asp:Button ID="Button1" runat="server" class="btn btn-primary preview-add-button" Text="Go" OnClick="Button1_Click" />
                               <asp:Button ID="Button2" runat="server" class="btn btn-primary preview-add-button" Text="Submit for All" OnClick="Button2_Click" />
                              <asp:Button ID="Button3" runat="server" class="btn btn-primary preview-add-button" Text="Matched all Records" OnClick="Button3_Click" />
                        </div>
                    </div>
                </div>
            </div>            
        </div> <!-- / panel preview -->
	</div>
</div>
    <hr />
    
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>  

<asp:GridView ID="example" runat="server" AutoGenerateColumns="false">
<Columns>
    <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="PCT" HeaderText="PCTs" ItemStyle-Width="150" />
</Columns>
</asp:GridView>
 

      </form>
    


</body>
</html>     
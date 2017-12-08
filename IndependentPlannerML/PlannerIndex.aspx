<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlannerIndex.aspx.cs" Inherits="IndependentPlannerML.PlannerIndex" %>
<!DOCTYPE html>
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
    <script type="text/javascript">
        $(function () {
            $('[id*=example]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "displayLength": 25,
               
                "sPaginationType": "full_numbers",
                "columnDefs": [
                    {'orderable':false, "targets": [0,4]},
                    ]
               
            });
        });
    </script>

</head>
<body>
<form id="form1" runat="server" >
  <div class="container">
	<div class="row">
        <div class="col-sm-12">
            <legend>Planner Result:</legend>
        </div>
        <br />
        <!-- panel preview -->
        <div class="col-sm-5">
            <h4>Package Details:</h4>
            <div class="panel panel-default">
                <div class="panel-body form-horizontal payment-form">
                    <div class="form-group">
                        <label for="concept" class="col-sm-3 control-label">Studio</label>
                        <div class="col-sm-9">
                          
                            
                            <asp:DropDownList ID="drpstudio" runat="server" CssClass="form-control"  required></asp:DropDownList>


                        </div>
                    </div>
             
                    <div class="form-group">
                        <label for="description" class="col-sm-3 control-label">Release</label>
                        <div class="col-sm-9">
                          
                            <asp:DropDownList ID="Drprelease" runat="server" CssClass="form-control"  ></asp:DropDownList>
                        </div>
                    </div>
                      <div class="form-group">
                        <label for="concept" class="col-sm-3 control-label">HD/SD</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="Drphdsd" runat="server" CssClass="form-control"    ></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label for="description" class="col-sm-3 control-label">Territories</label>
                        <div class="col-sm-9">
                          
                            <asp:DropDownList ID="DrpTerritories" runat="server" CssClass="form-control"   required></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                             <asp:Button ID="Button1" runat="server" class="btn btn-primary preview-add-button" Text="Go" OnClick="Button1_Click" />
                        </div>
                    </div>
                </div>
            </div>            
        </div> <!-- / panel preview -->
	</div>
</div>
    <hr />
    <div class="panel-body">
        <asp:GridView ID="example" runat="server" AutoGenerateColumns="false" class="table table-striped table-responsive" EmptyDataText="No Records Found"  ShowHeaderWhenEmpty="true" >
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

<%--<div class="bs-example">
   
        <div class="form-group">
            <label class="sr-only" for="inputEmail">Title</label>
            
           
        </div>
        
      
      


      <<div class="form-group">
            <label class="sr-only" for="inputPassword">Password</label>
            
        </div>
        
        <div class="form-group">
             <%--<button type="submit" class="btn btn-primary">Login</button>->
           
        </div>
       
   
    <br>

    <hr />
    <div>-%
        

        <%--<table id="example" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th>Name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Age</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th>Name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Age</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </tfoot>
        <tbody>
            <tr>
                <td>Tiger Nixon</td>
                <td>System Architect</td>
                <td>Edinburgh</td>
                <td>61</td>
                <td>2011/04/25</td>
                <td>$320,800</td>
            </tr>
            <tr>
                <td>Garrett Winters</td>
                <td>Accountant</td>
                <td>Tokyo</td>
                <td>63</td>
                <td>2011/07/25</td>
                <td>$170,750</td>
            </tr>
            <tr>
                <td>Ashton Cox</td>
                <td>Junior Technical Author</td>
                <td>San Francisco</td>
                <td>66</td>
                <td>2009/01/12</td>
                <td>$86,000</td>
            </tr>
             </tbody>
    </table>>
    </div>
   
<%--    <div class="alert alert-info">
        <a href="#" class="close" data-dismiss="alert">&times;</a>
        <strong>Note:</strong> The inline form layout is rendered as default vertical form layout if the viewport width is less than 768px. Open the output in a new window and resize the screen to see how it works.
    </div>
</div>>
   


   

    <script--% type="text/javascript">
        $(document).ready(function () {
            var table = $('#example').DataTable();

            //$('#example tbody').on('click', 'tr', function () {
            //    var data = table.row(this).data();
            //    alert('You clicked on ' + data[0] + '\'s row');
            //});
        });
    </script--%>

</body>
</html>                            
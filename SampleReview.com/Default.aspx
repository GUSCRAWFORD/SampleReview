<%@ Page Language="C#" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Sample Review SPA</title>
    
    <link href="css/review.style.css" rel="stylesheet" type="text/css">
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">
</head>
<body>
    <div class="container">
        <div class="header clearfix">
            <nav>
                <ul class="nav nav-pills pull-right">
                    <li role="presentation" class="active">
                        <a href="#"><i class="fa fa-home" aria-hidden="true"></i> Home</a>
                    </li>
                    <li role="presentation" class="active">
                        <a href="#!/gus-plus-tfs"><i class="fa fa-home" aria-hidden="true"></i> Gus + TFS</a>
                    </li>                    
                </ul>
            </nav>
            <h3 class="text-muted"><i class="fa fa-cubes fa-2x" aria-hidden="true"></i> Sample Review Application</h3>
        </div>
        <hr>
        <div ng-view></div>
        <footer class="footer">
            <p>&copy; 2017 Gus Crawford</p>
        </footer>
    </div>
    <script src="js/ui.review.js"></script>
    <script>
        angular.module('app.review').constant('restEndpoint',"<asp:Literal runat='server' Text='<%$ appSettings:ReviewsApi%>' />");
    </script>
</body>
</html>

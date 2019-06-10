<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>報表系統</title>

	<link rel="stylesheet" href="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/css/bootstrap.min.css">  
	<script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
	<script src="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/js/bootstrap.min.js"></script>

</head>
<!--Bootstrap 101 Template-->


  <body>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
<div class="row">
  <div class="col-xs-12 col-sm-3">
    <div class="panel panel-primary">
      <div class="panel-heading">
        <h3 class="panel-title">主選單</h3>
      </div>
      <div class="panel-body">
        <div class="list-group">
          <li class="list-group-item list-group-item-success">報表選單</li>
          <a href="?page=getMain" class="list-group-item">專題成果報告文件</a>
		  <a href="?page=getOpenSerialNo2" class="list-group-item">巡檢歷程報表</a>
          <a href="?page=getOpenSerialNo" class="list-group-item">異常巡檢報表</a>
          <a href="?page=analysisDocStatusRate" class="list-group-item">巡檢報表結案狀態</a>
          <a href="?page=analysisDocByInspector" class="list-group-item">巡檢員巡檢件數</a>
          <a href="?page=newCreateSerialNo" class="list-group-item">手動產生巡檢單</a>
          </div>
      </div>
      <div class="panel-footer">
        <small>報表系統</small>
      </div>
    </div>
  </div>
  <div class="col-xs-12 col-sm-9">
    <div class="panel panel-primary">
      <div class="panel-heading">
        <h3 class="panel-title">內容區</h3>
      </div>
	  
	  <!--
      <div class="panel-body">
        <div class="alert alert-warning" role="alert"><strong>注意：</strong>這只是個demo</div>
		-->
<?php
error_reporting(0);

if(isset($_GET['page']) AND !empty($_GET['page'])){
    $page = $_GET['page'];
}else{
    $page = "";
}
switch($page){  // 依照 GET 參數載入共用的內容
    case "getMain";
    include('./getMain.php');
    break;	
    case "getOpenSerialNo2";
    include('./getOpenSerialNo2.php');
    break;
    case "getOpenSerialNo";
    include('./getOpenSerialNo.php');
    break;
    case "analysisDocStatusRate";
    include('./analysisDocStatusRate.php');
    break;
    case "analysisDocByInspector";
    include('./analysisDocByInspector.php');
    break;
    case "newCreateSerialNo";
    include('./newCreateSerialNo.php');
    break;
}

?>
  



      </div>
    </div>
  </div>
</div>


  </body>
</html>








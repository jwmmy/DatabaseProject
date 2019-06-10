<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<body>

<!--有空再來改
<div class="btn-group" >
	<form method="post"> 
		<input type="hidden" name="searchValue" value="<? = $searchValue ?>" />
			<button type="button" class="btn btn-default" value="03" onClick="this.form.submit();"> 3月</button>
			<button type="button" class="btn btn-default" value="04" onClick="this.form.submit();"> 4月</button>
			<button type="button" class="btn btn-default" value="05" onClick="this.form.submit();"> 5月</button>
	</form>
</div>
-->
      <div class="panel-body">
        <div class="alert alert-warning" role="alert">
		<strong> 
		
			<form method="post">
				<input type="hidden" name="searchValue" value="<? = $searchValue ?>" />
					<select class="form-control" name="searchValue" onchange="this.form.submit()">
						<option value="0" >請選擇月份</option>
						<option value="03" >03</option>
						<option value="04" >04</option>
						<option value="05" >05</option>
			</select>
			</form>	
		
		</strong>
		</div>


<?php
header("Content-Type:text/html; charset=utf-8");
$servername = "35.201.142.203";
$username = "n97071044";
$password = "n97071044";
$dbname = "ncku_hw";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
$conn->query("set names 'utf8'");
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 
?>




<?php
$searchValue = $_POST["searchValue"];
//$filterValue = $_POST["filterValue"] ? ($_POST["filterValue"]) : 0;
$sql = "SELECT 	sum((CASE WHEN t1.check_status='N' THEN 1 ELSE 0 END)) n_qty,
				sum((CASE WHEN t1.check_status='P' THEN 1 ELSE 0 END)) p_qty,
				sum((CASE WHEN t1.check_status='Y' THEN 1 ELSE 0 END)) y_qty " .
       "FROM ichk_doc_header t1 " .
       "WHERE t1.check_date is not null and DATE_FORMAT(t1.check_date,'%Y%m')= '2019" . "$searchValue'";
//if($filterValue > 0) $sql .= "'$filterValue'";

//查詢幾筆
$sqltotal="SELECT sum((CASE WHEN t1.check_status='N' THEN 1 ELSE 0 END))+sum((CASE WHEN t1.check_status='P' THEN 1 ELSE 0 END))+sum((CASE WHEN t1.check_status='Y' THEN 1 ELSE 0 END)) as tt
FROM ichk_doc_header t1 
WHERE t1.check_date is not null and DATE_FORMAT(t1.check_date,'%Y%m')= '2019" . "$searchValue'";
$rt = $conn->query($sqltotal);
$rr= $rt->fetch_assoc();
if(empty($rr["tt"])){
	echo "計有0筆巡檢表" ;
}else{
	echo "計有".$rr["tt"]."筆巡檢表";
}


//查詢$sql
$result = $conn->query($sql);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        //echo "Open: " . $row["n_qty"]. " - InProgress: " . $row["p_qty"]. " - Closed: " . $row["y_qty"]. "<br>";
        //for CanvasJS
    $dataPoints = array(
	array("label"=> "已開立", "y"=> $row["n_qty"]),
	array("label"=> "作業中", "y"=> $row["p_qty"]),
	array("label"=> "已完成", "y"=> $row["y_qty"]));
	}
} else {
    echo "本月無資料";
}
$conn->close();
?> 





<div id="chartContainer" style="height: 370px; width: 100%;"></div>
<script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
</body>
</html>





<script>
window.onload = function () {
 
var chart = new CanvasJS.Chart("chartContainer", {
	animationEnabled: true,
	exportEnabled: true,
	title:{
		text: "巡檢報表結案狀態 -<?php echo $searchValue;?>月 "
	},
	subtitles: [{
		text: ""
	}],
	data: [{
		type: "pie",
		showInLegend: "true",
		legendText: "{label}",
		indexLabelFontSize: 16,
		indexLabel: "{label} - #percent%",
		yValueFormatString: "##,##0",
		dataPoints: <?php echo json_encode($dataPoints, JSON_NUMERIC_CHECK); ?>
	}]
});
chart.render();
 
}
</script>
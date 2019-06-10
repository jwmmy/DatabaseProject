<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
</head>
<body>
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

$sql = 	"SELECT t1.applicant_id,count(t1.serial_no) doc_qty " . 
		"FROM ichk_doc_header t1 " .
		"WHERE DATE_FORMAT(t1.check_date,'%Y%m')>='201904' " .
		"AND t1.applicant_id in ('N97071028','N97071044','N97071086','N97071133')" . 
		"group by t1.applicant_id ";
$result = $conn->query($sql);

$dataPoints = array();

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
    $dataOutput = array( array( "y" => $row["doc_qty"], "label" => $row["applicant_id"] ));
    if(!empty($dataOutput)) {
    $dataPoints = array_merge($dataPoints, $dataOutput);
    }
  }
 } else {
    echo "0 results";
}
//$stringRepresentation= json_encode($dataPoints);
//echo $stringRepresentation . "<br>";

$conn->close();
?> 

<div id="chartContainer" style="height: 370px; width: 100%;"></div>
<script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
</body>
</html>

<script>
window.onload = function() {
 
var chart = new CanvasJS.Chart("chartContainer", {
	animationEnabled: true,
	theme: "light2",
	title:{
		text: "巡檢員巡檢件數"
	},
	axisY: {
		title: "巡檢件數"
	},
	data: [{
		type: "column",
		yValueFormatString: "#,##0.## 筆",
		dataPoints: <?php echo json_encode($dataPoints, JSON_NUMERIC_CHECK); ?>
	}]
});
chart.render();
 
}
</script>
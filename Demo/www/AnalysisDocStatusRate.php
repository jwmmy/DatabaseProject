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

$sql = "SELECT sum((CASE WHEN t1.check_status='N' THEN 1 ELSE 0 END)) n_qty,sum((CASE WHEN t1.check_status='P' THEN 1 ELSE 0 END)) p_qty,sum((CASE WHEN t1.check_status='Y' THEN 1 ELSE 0 END)) y_qty FROM ichk_doc_header t1 WHERE t1.check_date is not null and DATE_FORMAT(t1.check_date,'%Y%m')=DATE_FORMAT(CURRENT_DATE,'%Y%m')";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        //echo "Open: " . $row["n_qty"]. " - InProgress: " . $row["p_qty"]. " - Closed: " . $row["y_qty"]. "<br>";
        //for CanvasJS
    $dataPoints = array(
	array("label"=> "Open", "y"=> $row["n_qty"]),
	array("label"=> "InPoress", "y"=> $row["p_qty"]),
	array("label"=> "Closed", "y"=> $row["y_qty"]));
    }
} else {
    echo "0 results";
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
		text: "Doc Status rate in current-month"
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
		yValueFormatString: "?#,##0",
		dataPoints: <?php echo json_encode($dataPoints, JSON_NUMERIC_CHECK); ?>
	}]
});
chart.render();
 
}
</script>
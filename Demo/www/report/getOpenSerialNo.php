<html>
 <head>
  <title>PHP Test</title>
	<link rel="stylesheet" href="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/css/bootstrap.min.css">  
	<script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
	<script src="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/js/bootstrap.min.js"></script>

 </head>
 <body>


<?php
$servername = "35.201.142.203";
$username = "n97071044";
$password = "n97071044";
$dbname = "ncku_hw";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT serial_no,ichk_name,ichk_item_desc,check_result_desc,check_comment,created_by,replace(url,'C:/wamp64/www/report/','http://35.201.142.203/report/') as url
		FROM v_ichk_doc_all
		WHERE action_status is null and check_result='2'";
//$sql = "SELECT * FROM v_ichk_doc_all WHERE actions_status='N'";
$conn->query("set names 'utf8'");
$result = $conn->query($sql);

$conn->close();
//	  echo $total_fields=mysqli_num_fields($result). "<br>"; // 取得欄位數
   	  //echo $total_records=mysqli_num_rows($result). "<br>";  // 取得記錄數
?>


<table class="table">
	<tr>
		<td>表單號碼</td>
		<td>表單名稱</td>
		<td>檢查項目</td>		
		<td>狀態</td>
		<td>備註</td>
		<td>巡檢人員</td>
		<td>現場相片</td>
	</tr>
	<?php
	error_reporting(0);
	//if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()){
		echo "<tr class=\"danger\">";
		echo "<td>" . $row["serial_no"]. "</td>";
		echo "<td>" . $row["ichk_name"]. "</td>";
		echo "<td>" . $row["ichk_item_desc"]. "</td>";
		echo "<td>" . $row["check_result_desc"]. "</td>";
		echo "<td>" . $row["check_comment"]. "</td>";
		echo "<td>" . $row["created_by"]. "</td>";
		echo "<td> <a href=" . $row["url"]. ">Show.File</a></td>";
		echo "</tr>";
		}
	//}
	?>
	<tr>

	</tr>

</table>

<!--
<?php
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo "SerialNo: " . $row["serial_no"]. " - ChkDoc: " . $row["ichk_code"]. " " . $row["ichk_name"]. $row["action_status_desc"]. "<br>";

    }
} else {
    echo "0 results";
}

$conn->close();
?> 	
-->


</body>
</html>


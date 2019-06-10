<html>
 <head>
  <title>PHP Test</title>
	<link rel="stylesheet" href="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/css/bootstrap.min.css">  
	<script src="https://cdn.staticfile.org/jquery/2.1.1/jquery.min.js"></script>
	<script src="https://cdn.staticfile.org/twitter-bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
 </head>
 <body>
<!-- 
		新增一支“巡檢歷程報表” 
		輸入單號查訊，
		或是簡單一點直接show 出巡檢清單 
		order by 建立時間 desc  ， 
		這樣也利於報告及測試查尋單子
		SELECT * FROM v_ichk_doc_all where 查詢欄位??
-->

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

$filterValue = $_POST["filterValue"]; 
        
$sql = "SELECT 	serial_no,check_status_desc,ichk_group_desc,ichk_item_desc,check_result_desc,action_desc,action_status_desc 
		FROM v_ichk_doc_all 
		WHERE DATE_FORMAT(v_ichk_doc_all.creation_date,'%Y%m')>='201904'";
if($filterValue != null){
	$sql .= " AND v_ichk_doc_all.creation_date = ( SELECT max(creation_date) FROM v_ichk_doc_all) ";
}
else
{
	$sql .= " AND 1=0 ";
}

// execute the stored procedure
$sqlexec = "CALL p_ichk_doc_autogen('D','VWDD','NCKU','$filterValue')";
// call the stored procedure
if($filterValue != null){$q = $conn->query($sqlexec); }

$conn->query("set names 'utf8'");
$result = $conn->query($sql);

$conn->close();
?>


<form method="post">
  <div class="form-row align-items-center">
	<div class="col-auto">
		<div class="input-group mb-2">
			<div class="input-group-prepend">
				<div class="input-group-text">指派巡檢人員號碼</div>
			</div>
			<input name="filterValue" type="text" class="form-control" id="filterValue" placeholder="輸入巡檢人員號碼" >
		</div>
    </div>
    <div class="col-auto">
		<button type="submit" class="btn btn-primary mb-2" onchange="this.form.submit()">產生新巡檢單</button>
    </div>
  </div>
</form>



<table class="table">
	<tr>
		<td>表單號碼</td>
		<td>單據狀態</td>
		<td>檢查類別</td>
		<td>檢查項目</td>
		<td>檢查結果</td>
		<td>處理方式</td>
		<td>案件狀態</td>
	</tr>
	<?php
	error_reporting(0);
	//if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()){
		if(empty($row["action_status_desc"])){
				echo "<tr class=\"danger\">";
			}else{
				echo "<tr class=\"success\">";
			}
		echo "<td>" . $row["serial_no"]. "</td>";
		echo "<td>" . $row["check_status_desc"]. "</td>";
		echo "<td>" . $row["ichk_group_desc"]. "</td>";
		echo "<td>" . $row["ichk_item_desc"]. "</td>";
		echo "<td>" . $row["check_result_desc"]. "</td>";
		echo "<td>" . $row["action_desc"]. "</td>";
		echo "<td>" . $row["action_status_desc"]. "</td>";
		echo "</tr>";
		}
	//}
	?>
	<tr>

	</tr>

</table>

</body>
</html>


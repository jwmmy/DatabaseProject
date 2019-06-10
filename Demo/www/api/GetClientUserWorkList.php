<?php

// error_reporting(0);

// $host_db = "35.201.142.203:3306";
// $user_db = "n97071133";
// $pass_db = "n97071133";
// $db_name = "ncku_hw";
// $connection = mysqli_connect($host_db, $user_db, $pass_db, $db_name);

$outputData = array();
class ToDoList
{
public $serial_no;
public $ichk_name;
public $ichk_rev;
}

$data = json_decode($_POST['json'], true);
$connection = mysqli_connect($data['dbip'], $data['username'], $data['password'], $data['dbname']);
$connection->query("set names 'utf8'");


if(!$connection){
	echo "Connect DB Error";
}else{
	//$sql = "SELECT serial_no,ichk_name,ichk_rev FROM ichk_doc_header WHERE 'applicant_id'='n97071133' AND 'check_status'='Y'";
	$sql = "SELECT serial_no,ichk_name,ichk_rev FROM ichk_doc_header WHERE created_by = '".$data['applicant_id']."' AND check_status != 'Y'";
	$result = $connection->query($sql); 
	
	if($result){
		//echo "查詢成功";
		while ($row = mysqli_fetch_array($result,MYSQLI_ASSOC))
		{
		$list = new ToDoList();
		$list->serial_no = $row["serial_no"];
		$list->ichk_name = $row["ichk_name"];
		$list->ichk_rev = $row["ichk_rev"];
	
		$outputData[]=$list;
		}
		
		$json = json_encode($outputData);//把資料轉換為JSON資料.
		echo $json;
		mysqli_free_result($result);
	}else{
		echo "Select Fail";
	}
	
}

mysqli_close($connection);
exit();


?>
<?php

// error_reporting(0);

$host_db = "35.201.142.203:3306";
$user_db = "n97071133";
$pass_db = "n97071133";
$db_name = "ncku_hw";
$connection = mysqli_connect($host_db, $user_db, $pass_db, $db_name);
$connection->query("set names 'utf8'");

$outputData = array();
class CheckItems
{
public $doc_item;
public $check_status_desc;
public $ichk_item_desc;
public $check_point;
public $check_result_desc;
public $check_comment;
public $photo_path;
public $ichk_group_desc;
}


 $data = json_decode($_POST['json'], true);
// $connection = mysqli_connect($data['dbip'], $data['username'], $data['password'], $data['dbname']);


if(!$connection){
	echo "Connect DB Error";
}else{
	//$sql = "SELECT serial_no,ichk_name,ichk_rev FROM ichk_doc_header WHERE 'applicant_id'='n97071133' AND 'check_status'='Y'";
	//SELECT doc_item,check_status_desc,ichk_item_desc,check_point,check_result_desc,check_comment,photo_path FROM v_ichk_doc_all WHERE serial_no='VWDDTT041708291' order by doc_item
	$sql = "SELECT doc_item,check_status_desc,ichk_item_desc,check_point,check_result_desc,check_comment,photo_path,ichk_group_desc FROM v_ichk_doc_all WHERE serial_no = '".$data['serial_no']."' order by doc_item";
	$result = $connection->query($sql); 
	
	if($result){
		//echo "查詢成功";
		while ($row = mysqli_fetch_array($result,MYSQLI_ASSOC))
		{
		$items = new CheckItems();
		$items->doc_item = $row["doc_item"];
		$items->check_status_desc = $row["check_status_desc"];
		$items->ichk_item_desc = $row["ichk_item_desc"];
		$items->check_point = $row["check_point"];
		$items->check_result_desc = $row["check_result_desc"];
		$items->check_comment = $row["check_comment"];
		$items->photo_path = $row["photo_path"];
		$items->ichk_group_desc = $row["ichk_group_desc"];
		$outputData[]=$items;
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
<?php

// error_reporting(0);

$data = json_decode($_POST['json'], true);



$host_db = "35.201.142.203:3306";
$user_db = "n97071133";
$pass_db = "n97071133";
$db_name = "ncku_hw";
$connection = mysqli_connect($host_db, $user_db, $pass_db, $db_name);


//$connection = mysql_pconnect($host_db, $user_db, $pass_db) or trigger_error(mysql_error(),E_USER_ERROR); 
//mysql_select_db($db_name,$connection); 
$connection->query("set names 'utf8'");

$outputData = array();



// $connection = mysqli_connect($data['dbip'], $data['username'], $data['password'], $data['dbname']);


if(!$connection){
	echo "Connect DB Error";
}else{

		$sql = "UPDATE ichk_doc_header SET check_status = 'Y' WHERE serial_no = '".$data['serial_no']."'";
		$result = $connection->query($sql); 
		
		if($result){
		
		//echo "查詢成功";
		echo "Success";
		
		}else{
			echo mysql_error();
		}
	
	

	
}

mysqli_close($connection);
exit();


?>
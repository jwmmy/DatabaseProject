<?php

// error_reporting(0);

$data = json_decode($_POST['json'], true);
$ImageFileDir = 'C:/wamp64/www/report/image/'.$data['serial_no'];
$dest ='';
if($data['check_result'] == '2'){
	if ( $_FILES['ErrorImage']['error'] === UPLOAD_ERR_OK){
	//  echo '檔案名稱: ' . $_FILES['ErrorImage']['name'] . '<br/>';
	 // echo '檔案類型: ' . $_FILES['ErrorImage']['type'] . '<br/>';
	 // echo '檔案大小: ' . ($_FILES['ErrorImage']['size'] / 1024) . ' KB<br/>';
	 // echo '暫存名稱: ' . $_FILES['ErrorImage']['tmp_name'] . '<br/>';

	  # 檢查檔案是否已經存在
	 // if (file_exists('upload/' . $_FILES['ErrorImage']['name'])){
		//echo '檔案已存在。<br/>';
	 // } else {
		$file = $_FILES['ErrorImage']['tmp_name'];
		$dest = $ImageFileDir .'/'. $_FILES['ErrorImage']['name'];
		if ( ! is_dir($ImageFileDir)) {
			mkdir($ImageFileDir);
		}

		//echo $dest;
		# 將檔案移至指定位置
		move_uploaded_file($file, $dest);
	 // }
	}else{
		echo "Upload Image Error";
		exit();
	}
}



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
	//$sql = "SELECT serial_no,ichk_name,ichk_rev FROM ichk_doc_header WHERE 'applicant_id'='n97071133' AND 'check_status'='Y'";
	//UPDATE ncku_hw.ichk_doc_item SET check_result=1,photo_path='OK' WHERE serial_no = 'CDTT101707002' AND doc_item = '1'
	//$sql = "SELECT doc_item,check_status_desc,ichk_item_desc,check_point,check_result_desc,check_comment,photo_path,ichk_group_desc FROM v_ichk_doc_all WHERE serial_no = '".$data['serial_no']."' order by doc_item";
	$sql = "UPDATE ichk_doc_item SET check_result = '".$data['check_result']."',photo_path = '".$dest."' WHERE serial_no = '".$data['serial_no']."' AND doc_item = '".$data['doc_item']."'";
	$result = $connection->query($sql); 
	if($result){
		
			$sql = "UPDATE ichk_doc_header SET check_status = 'P' WHERE serial_no = '".$data['serial_no']."'";
			$result = $connection->query($sql); 
		
		if($result){
		
		//echo "查詢成功";
		echo "Success";
		
		}else{
			echo mysql_error();
		}
	
	}else{
		echo mysql_error();
	}

	
}

mysqli_close($connection);
exit();


?>
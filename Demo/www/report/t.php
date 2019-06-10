<!--
 
$filterValue = $_POST["filterValue"] ? ($_POST["filterValue"]) : 0;

-->

<form  method="post">
		
	<input type="text" name="filterValue" />
	<button type="submit" onchange="this.form.submit()" >Submit</button>

</form>

<?php
$filterValue = $_POST["filterValue"]; 
$sql = "SELECT 	serial_no,check_status_desc,ichk_group_desc,ichk_item_desc,check_result_desc,action_desc,action_status_desc
		FROM `v_ichk_doc_all` 
		WHERE DATE_FORMAT(v_ichk_doc_all.creation_date,'%Y%m')>='201904'";
if($filterValue != null){
	$sql .= " AND v_ichk_doc_all.serial_no like '%$filterValue%'";
	echo $sql;
}else{
	echo "空的";
}
echo "<br/>";
echo "<br/>";
echo "<br/>";
//echo $filterValue; 
echo "<br/>";
//echo $sql; 
?>
<?php
error_reporting(0);




$data = json_decode($_POST['json'], true);

// $host_db = "35.201.142.203";
// $user_db = "n97071133";
// $pass_db = "n97071133";
// $db_name = "ncku_hw";
$connection = mysqli_connect($data['dbip'], $data['username'], $data['password'], $data['dbname']);

if(!$connection){
	echo "false";
}else{
	echo "true";
}
mysqli_close($connection);
exit();



// if($data != null){
	// echo "\r\n";
	// echo $data['username']."\r\n";
	// echo $data['password']."\r\n";
	// echo $data['dbip']."\r\n";	
	// echo $data['dbname']."\r\n";
// }

// $username = $_POST['username'];
// $password = $_POST['password'];

// $sql = "SELECT * FROM $tbl_name WHERE usuario = '$username'";
// $result = $conexion->query($sql);

// if ($result->num_rows > 0) {     

     // $row = $result->fetch_array(MYSQLI_ASSOC);
     // if ($password == $row["contra"]) { 
        // echo "Success";
     // } else { 
       // echo "upX";
     // }
 // }
 // else
 // {
    // echo "upX";
 // }
 mysqli_close($conexion);
?>
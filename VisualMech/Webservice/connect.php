<?php
header("Access-Control-Allow-Origin: https://almers5.github.io");

$servername = "mysql8010.site4now.net";
$username = "aa9c62_visualg";
$password = "Hello12345!"; 
$dbname = "db_aa9c62_visualg"; 

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>

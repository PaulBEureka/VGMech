<?php
include_once 'connect.php';

// Allow requests from specific origin
header("Access-Control-Allow-Origin: https://almers5.github.io");

$user_id = $_POST['user_id'];
$game_title = $_POST['game_title'];
$game_score = $_POST['game_score'];
$time_finished = $_POST['time_finished'];

// Check if there is an existing record
$sql = "SELECT * FROM game_record WHERE user_id = '$user_id' AND game_title = '$game_title'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    $row = $result->fetch_assoc();
    if ($game_score > $row['game_score'] || ($game_score == $row['game_score'] && $time_finished < $row['time_finished'])) {
        // Update the row with higher score or lower time finished
        $sql_update = "UPDATE game_record SET game_score = '$game_score', time_finished = '$time_finished' WHERE user_id = '$user_id' AND game_title = '$game_title'";
        if ($conn->query($sql_update) === TRUE) {
            echo "Record updated successfully";
        } else {
            echo "Error updating record: " . $conn->error;
        }
    } else {
        echo "Existing record found, no update needed";
    }
} else {
    // Insert a new record
    $sql_insert = "INSERT INTO game_record (user_id, game_title, game_score, time_finished) VALUES ('$user_id', '$game_title', '$game_score', '$time_finished')";
    if ($conn->query($sql_insert) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql_insert . "<br>" . $conn->error;
    }
}

$conn->close();
?>

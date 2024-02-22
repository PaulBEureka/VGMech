<?php
// Start or resume the session
session_start();

// Check if the username is set in the session
if(isset($_SESSION['CurrentUser'])) {
    // Retrieve the username from the session
    $username = $_SESSION['CurrentUser'];

    // Return the username as the response
    echo $username;
} else {
    // If the username is not set in the session, return an error message
    echo "Error: Username not found in session.";
}
?>

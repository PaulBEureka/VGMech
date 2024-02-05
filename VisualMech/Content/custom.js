function searchAndHighlight() {
    var searchText = document.getElementById("searchBox").value.trim(); // Get the search query

    // Perform the find operation
    var result = window.find(searchText);

    // If the search text is not found, alert the user
    if (!result) {
        alert("Text not found!");
    }
    return false;
}

//Try lang to, to be fixed pa

function post_Click() {
    var message = document.getElementById('commentbox').value; // Get textarea value

    PageMethods.post_Click(message,onSuccess, onError);
}

function onSuccess(response) {
    // Handle success response here
    alert(response);
    window.location.href = window.location.pathname; // Redirect to the same page
}

function onError(response) {
    // Handle error response here
    alert("An error occurred while posting comment.");
}



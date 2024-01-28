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
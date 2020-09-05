function init() {
    const query = queryStringFormatter(window.location.search);
    const results = getResult(query);
}

function queryStringFormatter(queryString) {
    let query = queryString.substring(queryString.indexOf('=') + 1);
    query = query.replaceAll("+", " ");
    query = query.replaceAll("%2B", "+");
    return query;
}

function getResult(query) {
    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            showResult(JSON.parse(this.response));
        }
    };
    xhttp.open("GET", `https://localhost:5001/search/${query}`, true);
    xhttp.setRequestHeader("content-type", "application/json");
    xhttp.send();
}


function showResult(results) {
    console.log(results);
}
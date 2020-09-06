var includeWordsList = [];

function init() {
    const query = queryStringFormatter(window.location.search);
    extractWordsList(query);
    document.getElementById('search-word').innerHTML = query;
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
    const resultElement = document.getElementById('result');
    for (const item of results) {
        const content = highlightWords(item.content);
        resultElement.innerHTML += `<details class="card single-result">
        <summary>${item.name}</summary>
        ${content}
        </details>`;
    }
}

function extractWordsList(query = 'a') {
    let words = query.split(' ');
    for (const word of words) {
        if (word.startsWith('+')) {
            includeWordsList.push(word.substring(1));
        } else if (!word.startsWith('-')){
            includeWordsList.push(word);
        }
    }
}

function highlightWords(content) {
    let newContent = content;
    for (const word of includeWordsList) {
        let regex = new RegExp(word, 'ig');
        newContent = newContent.replaceAll(regex, `<span class="highlighted">${word}</span>`);
    }
    return newContent;
}
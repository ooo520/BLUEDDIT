// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitOpinion(kind, answerId) {
	const xmlHttp = new XMLHttpRequest();
	let likeCount = document.getElementById(`answer-${answerId}-like-count`);
	xmlHttp.onreadystatechange = function () {
			if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
				console.info("Opinion submitted successfully");
				likeCount.innerText = Number(likeCount.innerText) + Number(xmlHttp.response);
			}
		}
	xmlHttp.open("POST", `/api/opinion/${kind}/${answerId}`, true); // true for asynchronous
	xmlHttp.send(null);
}
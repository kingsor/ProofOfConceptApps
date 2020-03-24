/*
This script create a json document with all the urls and saved date
*/

var links = document.links;

console.log('links.length: ' + links.length);

var urls = [];

for (var i = 0, linksLength = links.length; i < linksLength; i++) {
    if (links[i].hostname != window.location.hostname) {
        links[i].target = '_blank';
    }
	
	var timeAdded = links[i].getAttribute('time_added');
	//var jan1st1970 = new Date()
	var d = new Date();
	d.setTime(timeAdded*1000); 
	
	var newItem = document.createElement("span");
	//newItem.textContent = d.toString() + ' - ';
	newItem.textContent = d.toISOString() + ' - ';
	
	links[i].parentElement.insertBefore(newItem, links[i]);
	
	//console.log('time_added: ' + d.toDateString() + ' ' + d.toTimeString());
	
	links[i].search = links[i].search.replace(/utm_[^&]+&?/g, '').replace(/&$/, '').replace(/^\?$/, '');
	
	urlItem = { url: links[i].href, time_added: timeAdded, utc_date_added: d.toUTCString(), date_added: d.toString() };
	
	urls.push(urlItem);
}

/*
var preItem = document.createElement("textarea");
preItem.textContent = "URLs count: " + urls.length + "\n\n" + JSON.stringify(urls, null, 2);

document.body.appendChild(preItem);
*/

var textArea = document.createElement("textarea");
textArea.name = "content";
textArea.cols = "140";
textArea.rows = "40";

textArea.textContent = "URLs count: " + urls.length + "\n\n" + JSON.stringify(urls, null, 2);

document.body.appendChild(textArea);
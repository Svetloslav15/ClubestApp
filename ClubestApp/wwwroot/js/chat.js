"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (pictureUrl, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("div");
    div.classList.add('d-flex');
    div.classList.add('justify-content-start');
    div.classList.add('mb-4');       
    let imageDiv = document.createElement('div');
    imageDiv.classList.add('col-md-3');
    let imageEl = document.createElement('img');
    imageEl.src = pictureUrl;
    imageEl.classList.add('rounded-circle');
    imageEl.classList.add('w-100');
    imageDiv.appendChild(imageEl);
    let messageDiv = document.createElement('div');
    messageDiv.textContent = msg;
    let span = document.createElement('span');
    span.textContent = "\n Add datetime";
    messageDiv.appendChild(span);
    div.appendChild(imageDiv);
    div.appendChild(messageDiv);
    document.getElementById("messagesList").appendChild(div);
    document.getElementById("messageInput").value = "";
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    $.ajax({
        type: "POST",
        url: "/Message/Add",
        data: {
            "ClubId": "plamen"
        },
        //contentType: "application/json charset-UTF8",
        //dataType: "json",
        error: function (err) {
            console.log(err);
        }
    });

    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
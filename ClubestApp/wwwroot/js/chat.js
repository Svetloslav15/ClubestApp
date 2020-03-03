"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (pictureUrl, message, clubId) {
    if (document.getElementById("clubId").value == clubId) {
        let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        let div = document.createElement("div");
        div.classList.add('d-flex');
        div.classList.add('height-fit');
        div.classList.add('justify-content-start');
        div.classList.add('mb-2');
        let imageDiv = document.createElement('div');
        imageDiv.classList.add('img_cont_msg');
        let imageEl = document.createElement('img');
        imageEl.src = pictureUrl;
        imageEl.classList.add('w-50px');
        imageEl.classList.add('rounded-circle');
        imageEl.classList.add('user_img_msg');
        imageDiv.appendChild(imageEl);
        let messageDiv = document.createElement('div');
        messageDiv.textContent = msg;
        div.appendChild(imageDiv);
        div.appendChild(messageDiv);
        document.getElementById("messagesList").appendChild(div);
        document.getElementById("messageInput").value = "";
        updateScroll();
    }
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    let message = document.getElementById("messageInput").value;
    let clubId = document.getElementById("clubId").value;
    $.ajax({
        type: "GET",
        url: "/Message/Add",
        data: {
            "ClubId": clubId,
            "Content": message,
        },
        error: function (err) {
            console.log(err.responseText);
        },
        success: function (pictureUrl) {
            if (document.getElementById("clubId").value == clubId && pictureUrl != "Invalid message") {
                let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
                let div = document.createElement("div");
                div.classList.add('d-flex');
                div.classList.add('height-fit');
                div.classList.add('justify-content-start');
                div.classList.add('mb-2');
                div.classList.add('p-1');
                div.classList.add('text-white');
                div.classList.add('blue-gradient');
                let imageDiv = document.createElement('div');
                imageDiv.classList.add('img_cont_msg');
                let imageEl = document.createElement('img');
                imageEl.src = pictureUrl;
                imageEl.classList.add('w-50px');
                imageEl.classList.add('rounded-circle');
                imageEl.classList.add('user_img_msg');
                imageDiv.appendChild(imageEl);
                let messageDiv = document.createElement('div');
                messageDiv.textContent = msg;
                div.appendChild(imageDiv);
                div.appendChild(messageDiv);
                document.getElementById("messagesList").appendChild(div);
                document.getElementById("messageInput").value = "";
                updateScroll();
            }
        }
    });

    connection.invoke("SendMessage", message, clubId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
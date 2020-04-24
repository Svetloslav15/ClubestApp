function updateScroll() {
    var element = document.getElementById("messagesList");
    element.scrollTop = element.scrollHeight;
}

document.getElementById('sendButton').addEventListener('click', updateScroll);
window.onload = function () {
    this.updateScroll();
};
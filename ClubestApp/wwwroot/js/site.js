// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let field = document.getElementById('optionField');
let addBtn = document.getElementById('addButton');
let containter = document.getElementById('options');
let allOptions = document.getElementById('allOptions');
let textOptions = document.getElementById('textOptions');

addBtn.addEventListener('click', () => {
    let newElement = document.createElement('p');
    newElement.textContent = field.value;
    textOptions.textContent += field.value + " ";
    allOptions.setAttribute('value', textOptions.textContent);
    containter.appendChild(newElement);
    field.value = "";
});
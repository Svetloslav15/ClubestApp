// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let field = document.getElementById('optionField');
let addBtn = document.getElementById('addButton');
let containter = document.getElementById('options');
let allOptions = document.getElementById('allOptions');
let textOptions = document.getElementById('textOptions');

addBtn.addEventListener('click', () => {
    let newDiv = document.createElement('div');
    newDiv.classList.add("row");
    let optionText = document.createElement('h5');
    optionText.classList.add("col-lg-5");
    let deleteButton = document.createElement('button');
    deleteButton.classList.add("col-lg-5");
    deleteButton.innerHTML = "Изтрий";
    deleteButton.type = 'button';
    newDiv.appendChild(optionText);
    newDiv.appendChild(deleteButton);
    deleteButton.addEventListener('click', () => {
        allOptions.setAttribute('value', allOptions.value.replace(deleteButton.parentElement.firstChild.textContent, ''));
        textOptions.textContent = textOptions.textContent.replace(deleteButton.parentElement.firstChild.textContent, '');
        deleteButton.parentElement.remove();
    });

    optionText.textContent = field.value;
    textOptions.textContent += field.value + "~";
    allOptions.setAttribute('value', textOptions.textContent);

    containter.appendChild(newDiv);
    field.value = "";
});
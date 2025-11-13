// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    const popup = document.getElementById('confirmationPopup');
    const showBtn = document.getElementById('showPopupBtn');
    const confirmBtn = document.getElementById('confirmBtn');
    const cancelBtn = document.getElementById('cancelBtn');

    popup.style.display = 'none';

    showBtn.onclick = function (e) {
        e.preventDefault();
        popup.style.display = 'flex';
    };

    confirmBtn.onclick = function () {
        popup.style.display = 'none';
        document.getElementById('archiveForm').submit();
    };

    cancelBtn.onclick = function () {
        popup.style.display = 'none';
    };
};
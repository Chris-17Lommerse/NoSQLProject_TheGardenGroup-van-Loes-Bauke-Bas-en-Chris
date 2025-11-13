// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    const popup = document.getElementById('confirmationPopup');
    const showBtn = document.getElementById('showPopupBtn');
    const confirmBtn = document.getElementById('confirmBtn');
    const cancelBtn = document.getElementById('cancelBtn');

    // Zorg dat de popup standaard verborgen is
    popup.style.display = 'none';

    showBtn.onclick = function (e) {
        e.preventDefault(); // voorkom dat het formulier meteen wordt verzonden
        popup.style.display = 'flex'; // toon de popup
    };

    confirmBtn.onclick = function () {
        // Sluit de popup
        popup.style.display = 'none';
        // Verstuur het formulier om te archiveren
        document.getElementById('archiveForm').submit();
    };

    cancelBtn.onclick = function () {
        // Sluit de popup zonder actie
        popup.style.display = 'none';
    };
};
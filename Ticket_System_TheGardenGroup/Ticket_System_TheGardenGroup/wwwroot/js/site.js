// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    const popup = document.getElementById('confirmationPopup');
    const showBtn = document.getElementById('showPopupBtn');
    const confirmBtn = document.getElementById('confirmBtn');
    const cancelBtn = document.getElementById('cancelBtn');

    const undoMessage = document.getElementById('undoMessage');
    const undoBtn = document.getElementById('undoBtn');

    let archiveTimeout; // to hold the timeout ID

    showBtn.onclick = function () {
        popup.style.display = 'flex';
    };

    confirmBtn.onclick = function () {
        popup.style.display = 'none';

        // Show undo message
        undoMessage.style.display = 'block';

        // Set a timer for 10 seconds to perform the archiving
        archiveTimeout = setTimeout(function () {
            undoMessage.style.display = 'none';
            // Proceed with archiving, e.g., submit form or call API
            document.getElementById('archiveForm').submit();
            // Example: document.getElementById('archiveForm').submit();
        }, 10000); // 10 seconds
    };

    cancelBtn.onclick = function () {
        popup.style.display = 'none';
    };

    // Handle "Undo" click
    undoBtn.onclick = function () {
        clearTimeout(archiveTimeout); // cancel the archiving
        undoMessage.style.display = 'none';
        alert('Archiving canceled.');
    };
};
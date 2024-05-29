// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*document.addEventListener("DOMContentLoaded", function () {
    const addToEventDiv = document.querySelector(".addToEventTask");
    const createEventForm = document.getElementById("createEventForm");
    const header = addToEventDiv.querySelector("h3");

    header.addEventListener("click", function () {
        if (createEventForm.style.display === "none" || createEventForm.style.display === "") {
            createEventForm.style.display = "block";
        } else {
            createEventForm.style.display = "none";
        }
    });
});*/


document.addEventListener("DOMContentLoaded", function () {
    function setupToggle(divClass, formId) {
        const addToEventDiv = document.querySelector(divClass);
        if (!addToEventDiv) return;

        const createEventForm = document.getElementById(formId);
        const header = addToEventDiv.querySelector("h3");

        header.addEventListener("click", function () {
            if (createEventForm.style.display === "none" || createEventForm.style.display === "") {
                createEventForm.style.display = "block";
            } else {
                createEventForm.style.display = "none";
            }
        });
    }

    setupToggle(".addToEventTask", "createEventFormTask");
    setupToggle(".addToEventEmployee", "createEventFormEmployee");
    setupToggle(".addToEventParticipant", "createEventFormParticipant");
    setupToggle(".createEvent", "createEventForm"); 
    setupToggle(".addEmployee", "addFormEmployee");
    setupToggle(".addToParticipant", "addFormParticipant");
});

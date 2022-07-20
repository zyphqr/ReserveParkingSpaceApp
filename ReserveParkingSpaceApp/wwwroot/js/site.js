function updateDateShiftForm() {
    document.getElementById("submitForm").dispatchEvent(new Event("submit", {
        bubbles: true,
        cancelable: true
    }));
}

updateDateShiftForm();
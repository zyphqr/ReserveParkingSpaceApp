var modal = document.getElementById('popUp');

function openModal() {
    modal.style.display = "block";
}

document.querySelector('#closeModal').addEventListener('click', function () {
    document.querySelector('#popUp').style.display = 'none';
    console.log("It works");
});

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}




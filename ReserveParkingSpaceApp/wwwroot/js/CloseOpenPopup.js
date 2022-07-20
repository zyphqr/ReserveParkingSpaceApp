
function openModal(spot) {
    var dateValue = document.getElementById("Date").value;
    var shift = document.getElementById("SpotShift").value;
    $.ajax({
        type: "GET",
        url: "/Home/ReserveForm?spotId=" + spot + "&date=" + dateValue + "&shift=" + shift,
        data: {},
        success: function (response) {
            $("#modalContainer").html(response);

            var modal = document.getElementById('popUp');
            modal.style.display = "block";
            document.querySelector('#closeModal').addEventListener('click', function () {
                document.querySelector('#popUp').style.display = 'none';
                console.log("It works");
            });

            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        }
    })
}

function cancelReservations(spot) {
    var dateValue = document.getElementById("Date").value;

    $.ajax({
        type: "GET",
        url: "/Home/CancelSpotReservation?spotId=" + spot + "&date=" + dateValue,
        data: {},
        success: function (response) {
            $("body").append(response);
        }
    })
}




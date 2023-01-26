// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Dropdown menu
/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

// Posting timer
function updateTime() {
    var elements = document.getElementsByClassName("time-elapsed");
    for (var i = 0; i < elements.length; i++) {
        var postedAt = elements[i].getAttribute("data-posted-at");
        var timeElapsed = new Date() - new Date(postedAt);
        var seconds = Math.floor(timeElapsed / 1000);
        var minutes = Math.floor(seconds / 60);
        var hours = Math.floor(minutes / 60);
        var days = Math.floor(hours / 24);
        seconds = seconds % 60;
        minutes = minutes % 60;
        hours = hours % 24;
        elements[i].innerHTML = days + " days " + hours + " hours " + minutes + " minutes " + seconds + " minutes ago";
    }
}
setInterval(updateTime, 1000);


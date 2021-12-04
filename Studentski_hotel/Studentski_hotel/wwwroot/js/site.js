// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
function PrikaziStudenta() {
    document.getElementById("ajaxTabela").style.display = "block";
    //var url = "/Home/LoginForm";
    var url = "/Identity/Account/Login";
    var zahtjev = new XMLHttpRequest();
    zahtjev.onload = function () {
        if (zahtjev.status === 200) {
            document.getElementById("ajaxTabela").innerHTML = zahtjev.responseText;
        } else {

        }
    };
    zahtjev.open("GET", url, true);
    zahtjev.send();

}
function ZatvoriDiv() {
    if (document.getElementById("ajaxTabela").style.display == "block") {

        document.getElementById("ajaxTabela").style.display = "none";
    }
    function Zatvori() {

        document.getElementById("AjaxDiva").style.display = "none";

    }
}

var FilterSoba = () => {

    $.get("/Recepcija/FilterSoba"/*, { tip: indeks }*/, function (data) {
        $("#AjaxDiv").html(data);
    })
}
var FilterStudenata = () => {

    $.get("/Recepcija/FilterStudenata"/*, { tip: indeks }*/, function (data) {
        $("#AjaxDiv").html(data);
    })
}
var PrikazZahtjeva = () => {

    $.get("/Recepcija/PrikazZahtjeva"/*, { tip: indeks }*/, function (data) {
        $("#AjaxDiv").html(data);
    })
}

function password_generator(len) {
    var length = (len) ? (len) : (10);
    var string = "abcdefghijklmnopqrstuvwxyz"; //to upper
    var numeric = '0123456789';
    var punctuation = '!#$%^&*()_+~`|}{[]\:;?><,./-=';
    var password = "";
    var character = "";
    var crunch = true;
    while (password.length < length) {
        entity1 = Math.ceil(string.length * Math.random() * Math.random());
        entity2 = Math.ceil(numeric.length * Math.random() * Math.random());
        entity3 = Math.ceil(punctuation.length * Math.random() * Math.random());
        hold = string.charAt(entity1);
        hold = (password.length % 2 == 0) ? (hold.toUpperCase()) : (hold);
        character += hold;
        character += numeric.charAt(entity2);
        character += punctuation.charAt(entity3);
        password = character;
    }
    password = password.split('').sort(function () { return 0.5 - Math.random() }).join('');
    return password.substr(0, len);
}




//var connection = new signalR.HubConnectionBuilder().withUrl("/notHub").build();
//connection.on("SlanjeZahtjeva", function (Ime,Zahtjev,Datum,Soba) {


  

//    //var glavni = document.getElementById("glavni");
//    //var noviCvijet = document.createElement("div");
//    //var novaSlika = document.createElement("div");
//    //var slikaa = document.createElement("img");
//    //slikaa.src = "../Slike/" + slika;
//    //slikaa.style.height = "200px";
//    //slikaa.style.width = "251px";
//    //glavni.appendChild(noviCvijet).appendChild(novaSlika).appendChild(slikaa);


//    //console.info("radi");

//    var tabela = document.getElementById("SignalTabela");
//    var tr = document.createElement("tr");
//    var td1 = document.createElement("td");
//    var td2 = document.createElement("td");
//    var td3 = document.createElement("td");
//    var td4 = document.createElement("td");
//    var td5 = document.createElement("td");
//    td1.innerHTML = Ime;
//    td2.innerHTML = Zahtjev;
//    td3.innerHTML = Datum;
//    td4.innerHTML = Soba;
//    tabela.appendChild(tr).appendChild(td1);
//    tabela.appendChild(tr).appendChild(td2);
//    tabela.appendChild(tr).appendChild(td3);
//    tabela.appendChild(tr).appendChild(td4);
//    console.log("pasdsad");

//});

//connection.start().then(function () {
//    console.info("started signalR hub");

//}).catch(function (err) {
//    return console.error(err.toString());
//});



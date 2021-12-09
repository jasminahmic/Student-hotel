

var connection = new signalR.HubConnectionBuilder().withUrl("/notHub").build();
connection.on("SlanjeZahtjeva", function (Ime, Zahtjev, Datum, Soba, broj) {



    var tabela = document.getElementById("SignalTabela");
    var tr = document.createElement("tr");
    var td1 = document.createElement("td");
    var td2 = document.createElement("td");
    var td3 = document.createElement("td");
    var td4 = document.createElement("td");
    var td5 = document.createElement("td");


    var button = document.createElement("button");
    button.className = "btn";
    button.className+=" "
    button.textContent = "Pregled";
    button.style.backgroundColor = "#567d5d";
    button.style.color = "white";
    button.addEventListener("mouseover", function () {
        button.style.backgroundColor = "#263829";
    })
    button.addEventListener("mouseout", function () {
        button.style.backgroundColor = "#567d5d";
    })


    td1.innerHTML = Ime;
    td2.innerHTML = Zahtjev;
    td3.innerHTML = Datum;
    td4.innerHTML = Soba;
    console.log(broj);
    if (broj % 2 == 0) {
        tr.style.backgroundColor = "#a5f0b1";
    }
    //tabela.appendChild(tr).appendChild(td1);
    //tabela.appendChild(tr).appendChild(td2);
    //tabela.appendChild(tr).appendChild(td3);
    //tabela.appendChild(tr).appendChild(td4);
    //tabela.appendChild(tr).appendChild(td5).appendChild(button);
    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    tr.appendChild(td5).appendChild(button);
    console.log(tr);
    console.log(td2);

    tabela.insertBefore(tr, tabela.childNodes[0]);

});


connection.start().then(function () {
    console.info("started signalR hub");

}).catch(function (err) {
    return console.error(err.toString());
});




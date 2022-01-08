var connection = new signalR.HubConnectionBuilder().withUrl("/notHub").build();
    connection.on("SlanjeObavijesti", function (Naslov, TxtObavijesti, KorisnikID, DatumDodavanja, ImeRecepcionera, ObavijestID) {
    var novosti = document.querySelector(".news-section");

    var h3 = document.createElement("h3");
    var btnEdit = document.createElement("button");
    var span = document.createElement("span");
    var p = document.createElement("p");
    var a = document.createElement("a");

    btnEdit.classList.add("btn", "btn-success");
    btnEdit.textContent = "Edit Obavijest";
    h3.innerHTML = Naslov;
    p.innerHTML = TxtObavijesti;
    span.innerHTML = DatumDodavanja + '' + ImeRecepcionera;
    a.href = "/Recepcija/Obrisi ? obavijestID =" + ObavijestID;
    a.text = "Obrisi obavijest";
    
    novosti.appendChild(h3);
    novosti.appendChild(span);
    novosti.appendChild(p);
    if (KorisnikID !== 0) {
        novosti.appendChild(btnEdit);
        novosti.appendChild(a);
    }

})

connection.start().then(function () {
    console.info('connection established');

}).catch(function (err) {
    return console.error(err.toString());
});

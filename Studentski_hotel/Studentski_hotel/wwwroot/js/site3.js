var connection = new signalR.HubConnectionBuilder().withUrl("/notHub").build();
connection.on("SlanjeObavijesti", function (Naslov, TxtObavijesti, Korisnik, DatumDodavanja, ObavijestID) {
    console.info(Naslov, TxtObavijesti, Korisnik, DatumDodavanja, ObavijestID);
})

connection.start().then(function () {
    console.info('connection established');

}).catch(function (err) {
    return console.error(err.toString());
});

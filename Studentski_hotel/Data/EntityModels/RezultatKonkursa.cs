namespace DBdata.EntityModels
{
    public class RezultatKonkursa
    {
        public int ID { get; set; }
        public int BrojBodova { get; set; }
        public VrstaStanjaKonkursa VrstaStanjaKonkursa { get; set; }
        public int? VrstaStanjaKonkursaID { get; set; }
        public VrstaRazlogaOdbijanja VrstaRazlogaOdbijanja { get; set; }
        public int? VrstaRazlogaOdbijanjaID { get; set; }
    }
}
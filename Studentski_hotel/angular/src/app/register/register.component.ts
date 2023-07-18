import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { webApi } from '../server/server.service'
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  porukaSucces: string = " ste poslali zahtjev da budete stanar doma ";

  getOpstine: any;
  getOpstinaInKanton: any;
  getPolovi: any;
  getKanton: any;
  getCiklusStudija: any;
  getTipKandidata: any;
  getFakultet: any;
  getGodinaStudija: any;

  kandidat = {
    Ime: "",
    Prezime: "",
    ImeOca: "",
    MjestoRodjenjaID: 0,
    ZanimanjeRoditelja: "",
    Email: "",
    JMBG: "",
    LicnaKarta: "",
    DatumRodjenja: "",
    Mobitel: "",
    PolID: 0,
    Adresa: "",
    MjestoStanovanjaID: 0,
    KantonID: 0,
    FakultetID: 0,
    TipKandidataID: 0,
    BrojIndeksa: "",
    CiklusStudijaID: 0,
    GodinaStudijaID: 0
  };

  constructor(private httpKlijent: HttpClient, private router: Router, private toast: NgToastService) { }

  ngOnInit(): void {
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetOpstine").subscribe( (x: any) => {
      this.getOpstine = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetPol").subscribe( (x: any) => {
      this.getPolovi = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetKanton").subscribe( (x: any) => {
      this.getKanton = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetCiklusStudija").subscribe( (x: any) => {
      this.getCiklusStudija = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetTipKandidata").subscribe( (x: any) => {
      this.getTipKandidata = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetFakultet").subscribe( (x: any) => {
      this.getFakultet = x;
    });
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetGodinaStudija").subscribe( (x: any) => {
      this.getGodinaStudija = x;
    });
  }

  opstineInKanton(kand: any) {
    this.httpKlijent.get(webApi.baseUrl + "/KonkursApi/GetOpstineByKanton/" + kand).subscribe( (x: any) => {
      this.getOpstinaInKanton = x;
    });
  }

  fromStringToInt(kand: any) {
    kand.MjestoRodjenjaID = +kand.MjestoRodjenjaID;
    kand.PolID = +kand.PolID;
    kand.CiklusStudijaID = +kand.CiklusStudijaID;
    kand.MjestoStanovanjaID = +kand.MjestoStanovanjaID;
    kand.KantonID = +kand.KantonID;
    kand.FakultetID = +kand.FakultetID;
    kand.TipKandidataID = +kand.TipKandidataID;
    kand.CiklusStudijaID = +kand.CiklusStudijaID;
    kand.GodinaStudijaID = +kand.GodinaStudijaID;
  }

  snimiKandidata(kand: any) {
    this.fromStringToInt(kand);

    this.httpKlijent.post(webApi.baseUrl + "/KonkursApi/SnimiZahtjev", kand).subscribe( (povratnaVrijednost: any) => {
      this.porukaSucces += " | " + povratnaVrijednost.Ime + " " + povratnaVrijednost.Prezime;
      this.toast.success({detail: 'Uspješno', summary: this.porukaSucces, duration: 2000});
      setTimeout(() => {
        this.router.navigate(['/home']);
      }, 2500);  //1.5s
    });
  }
}

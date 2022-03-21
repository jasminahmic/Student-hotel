<template>
  <div class="sidenav">
    <span class="nav-link" style="font-size:x-large;" href="/Recepcija/PrikazObavijesti">StudentHotel</span>
    <a class="nav-link" href="/Recepcija/PrikazObavijesti">Obavijesti</a>
    <a class="nav-link" href="/Recepcija/FilterSoba">Pregled</a>
    <a class="nav-link" href="/Recepcija/FilterUplata">Uplate</a>
    <a class="nav-link" href="/Kuhinja/FilterKartica">Kartice</a>
    <a class="nav-link" asp-area="Identity" asp-page="/Account/Manage/Index" title="Manage">Postavke</a>
    <a class="nav-link" href="/Kuhinja/PrikazPrisutniStudenata">Prisutni studenti</a>
    <a class="nav-link" href="/Recepcija/PrikazStudenataKojiNisuTU">Studenti nisu prisutni</a>
    <a class="nav-link" href="/Student/NajavaOdlaskaBox">Najavi odlazak</a>
</div>
</template>

<style scoped>
 .sidenav {
  height: 100%;
  width: 210px;
  position: fixed;
  z-index: 1;
  bottom: 0;
  left: 0;
  /*        background-color: #41B3A3;
*/ background-color: #263829;
  overflow-x: hidden;
  padding-top: 20px;
  text-align: center;
  color: white;
  display: flex;
  flex-direction: column;
}

span {
  padding: 0.5rem 1rem;
}

.sidenav a {
    text-decoration: none;
    font-size: 20px;
    color: white;
    display: block;
    padding: 0.5rem 1rem;
  }

.active, .sidenav a:hover {
    color: #263829;
    background-color: #aefdbb;
    transition: 0.5s;
  }
</style>
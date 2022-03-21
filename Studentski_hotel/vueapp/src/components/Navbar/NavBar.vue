<template>
  <nav>
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link zuti" href="/Student/OznaciPrisutnim">Potvrdi da si tu</a>
        <a class="nav-link zuti" href="/Recepcija/ListaZaNapomenuti">Obavijesti Studente</a>
        <a class="nav-link text-white" href="/Identity/Account/Manage">Moji podaci</a>
      </li>
    </ul>
  </nav>

</template>

<style>
  nav {
    background-color: #263829;
    margin-bottom: 1rem;
    justify-content: flex-start;
    display: flex;
    align-items: center;
    position: relative;
    padding: 0.5rem 1rem;
    margin-left: 201px;
    position: fixed;
    top: 0;
    width: 100%;
  }

  ul {
    display: flex;
    flex-basis: auto;
    flex-direction: row-reverse;
  }
    .nav-item:first-of-type {
        display: flex;
    }

    .zuti:hover {
        outline: 2px solid yellow;
        border-radius: 24px;
        font-size: 16px;
        color: #17a2b8 !important;
    }

    ul {
        align-items: center;
    }
</style>
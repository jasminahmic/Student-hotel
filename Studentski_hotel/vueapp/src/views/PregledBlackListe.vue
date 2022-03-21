<template>
  <h1>Blacklista studentskog doma: </h1>

<div class="naslov">
    <button class="btn btn-success" @click="addNewStudent"> Dodaj studenta +</button>
</div>

<table class="table table-borderless table-striped">
    <thead>
        <tr>
            <th>Student</th>
            <th>Razlog</th>
            <th>Ukloni</th>
            <th></th>
        </tr>
    </thead>
      <tr v-for="(student, index) in studenti" :key="index">
          <td> {{ student.imeStudenta }}</td>
          <td> {{ student.razlog }}</td>

          <td><a class="delete" @click.prevent="removeFromList(student.studentID)">Ukloni</a></td>
          <td><button class="btn btn-success" @click="openEditMode(student.studentID)">Uredi</button></td>
      </tr>
</table>
</template>

<script>
import { defineComponent, onBeforeMount, ref } from 'vue'
import axios from 'axios'
import router from '@/router'

export default defineComponent({
  setup() {
    const studenti = ref()

    onBeforeMount(async () => {
      studenti.value = (await axios.get("https://localhost:44328/RecepcijaApi/PregledBlackListe")).data.studenti;
      console.log(studenti.value)
    })

    function openEditMode(id) {
      router.push({ path: 'edit', query: { studentId: id }})
    }

    function addNewStudent() {
      router.push({ path: 'edit'})
    }

    async function removeFromList(studentID) {
      console.log(studentID)
      await axios.delete(`https://localhost:44328/RecepcijaApi/SkloniStudenta?studentID=${studentID}`).then(res=>console.log(res)).catch(err=>console.log(err))
      studenti.value = (await axios.get("https://localhost:44328/RecepcijaApi/PregledBlackListe")).data.studenti;
    }

    return {
      studenti,
      openEditMode,
      addNewStudent,
      removeFromList
    }
  },
})
</script>


<style>
  .btn-pregled {
      background-color: #567d5d;
      color: white;
  }

      .btn-pregled:hover {
          color: white;
          background-color: #263829;
      }

  .btn-success {
      float: right;
    color: #fff;
    background-color: #28a745;
    border-color: #28a745;
    display: inline-block;
    font-weight: 400;
    text-align: center;
    vertical-align: middle;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
    border: 1px solid transparent;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    border-radius: 0.25rem;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
    cursor: pointer;
  }

  a.delete {
      color: red;
      cursor: pointer;
  }

  h1 {
    margin-top: 100px;
  }

 table{
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
  margin-top: 100px;
}

table td,table th {
  border: 1px solid #ddd;
  padding: 8px;
}

table th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #04AA6D;
  color: white;
}
</style>
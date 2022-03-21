<template>
  <form @submit.prevent="updateRecords">
    <div class="form-group">
        <label>Student : </label><br />
        <select v-model="selected" class="form-control">
          <option v-for="item in studenti" :value="item" :key="item.id">
            {{ item.imeStudenta }}
          </option>
        </select>
    </div>
    <div class="form-group">
        <label>Crna lista kategorije : </label><br />
        <select v-model="kategorija" class="form-control">
          <option v-for="item in kategorije" :value="item" :key="item.id">
            {{ item.naziv }}
          </option>
        </select>
    </div>
    <div class="form-group">
        <label for="exampleFormControlTextarea1"> Razlog dodavanja</label>
        <textarea v-model="razlog" maxlength="1000" class="form-control" id="exampleFormControlTextarea1" rows="3" style="height:200px"></textarea>
    </div>

    <input type="submit" value="Snimi" class="btn btn-info" style="float:right" />
  </form>
</template>

<script>
import { defineComponent, onBeforeMount, ref } from 'vue'
import axios from 'axios'
import router from '@/router'
import { useRoute } from "vue-router"

export default defineComponent({
  setup() {
    const studenti = ref()
    const kategorije = ref()
    const selected = ref()
    const kategorija = ref()
    const razlog = ref('')
    const route = useRoute()

    onBeforeMount(async () => {
      kategorije.value = (await axios.get("https://localhost:44328/RecepcijaApi/GetList")).data.kategorije;
      kategorija.value = kategorije.value[0]
      if (route.query.studentId) {
        studenti.value = (await axios.get("https://localhost:44328/RecepcijaApi/PregledBlackListe")).data.studenti;
        selected.value = studenti.value.filter(x=> x.studentID == route.query.studentId)[0]
        razlog.value = selected.value.razlog
      } else {
        studenti.value = (await axios.get("https://localhost:44328/RecepcijaApi/GetAllStudents")).data.studenti;
        selected.value = studenti.value[0]
      }
    })

    async function updateRecords() {
      const student = {
        StudentID : selected.value.studentID,
        Razlog : razlog.value,
        BlackListID : kategorija.value.id
      }

      await axios.put("https://localhost:44328/RecepcijaApi/EditBlackList",student).then(res=>console.log(res)).catch(err=>console.log(err))
      await router.push({ path: '/'})
    }

    return {
      studenti,
      razlog,
      kategorije,
      kategorija,
      selected,
      updateRecords
    }
  },
})
</script>

<style>
  form {
      margin-top: 10%;
  }

  .form-group {
   margin-bottom: 1rem;
  }

  label {
    display: inline-block;
    margin-bottom: 0.5rem;
  }

  .form-control {
    display: block;
    width: 100%;
    height: calc(1.5em + 0.75rem + 2px);
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #495057;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}

.btn {
    color: #fff;
    background-color: #17a2b8;
    border-color: #17a2b8;
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
</style>
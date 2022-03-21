<template>
  <NavBar />
  <div class="main">
    <router-view />
  </div>
 <SideBar />
</template>

<script>
import { ref } from 'vue'

import SideBar from "../src/components/Navbar/SideBar.vue"
import NavBar from "./components/Navbar/NavBar.vue"
import router from '@/router'

export default {
  name: 'App',
  components: {
    SideBar,
    NavBar,
  },
  setup() {
    const path = ref(router.currentRoute.value.path)

    return {
      path
    }
  }
}
</script>

<style>
.main {
  margin-left: 210px;
  height: 100vh;
}

body {
  background-color: #aefdbb;
}
</style>

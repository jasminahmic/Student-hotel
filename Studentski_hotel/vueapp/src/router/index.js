import { createRouter, createWebHashHistory } from 'vue-router'
import PregledBlackListe from "../views/PregledBlackListe.vue"

const routes = [
  {
    path: '/',
    name: 'Home',
    component: PregledBlackListe
  },
  {
    path: '/edit',
    name: 'EditBlackList',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/EditBlackList.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
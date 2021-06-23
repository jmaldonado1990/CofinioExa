import Vue from "vue";
import VueRouter from "vue-router";
import Colaborador from "../components/Colaborador.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/colaborador",
    name: "colaborador",
    component: Colaborador,
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;

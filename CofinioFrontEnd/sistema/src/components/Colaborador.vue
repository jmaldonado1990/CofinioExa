<template>
  <v-layout align-start>
    <v-flex>
      <v-data-table
        :headers="headers"
        :items="colaboradores"
        :search="search"
        sort-by="nombre"
        class="elevation-1"
        :footer-props="{
          itemsPerPageText: 'Mostrando',
          pageText: '{0}-{1} de {2}',
          itemsPerPageOptions: [5, 10, 20, 30, -1],
        }"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>COLABORADORES</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>

            <v-spacer></v-spacer>

            <v-text-field
              class="text-xs-center"
              v-model="search"
              append-icon="search"
              label="Búsqueda"
              single-line
              hide-details
              clearable
            ></v-text-field>

            <v-spacer></v-spacer>
            <template>
              <v-btn
                small
                class="mx-2"
                fab
                dark
                color="primary"
                @click="listar()"
              >
                <v-icon dark>autorenew</v-icon>
              </v-btn>
            </template>

            <v-dialog v-model="dialogNivelRiesgo" max-width="500">
              <v-card>
                <v-card-title class="headline"
                  >Nivel Riesgo de - {{ nombreColaborador }} -</v-card-title
                >
                <v-divider />
                <v-card-text>
                  {{ adNivelRiesgo }}
                </v-card-text>
                <v-divider />
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close"
                    >Aceptar</v-btn
                  >
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>

        <template v-slot:[`item.opciones`]="{ item }">
          <v-icon medium class="mr-2" @click="nivelRiesgo(item)">
            visibility
          </v-icon>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  data: () => ({
    colaboradores: [],
    nombreColaborador: "",
    dialogNivelRiesgo: false,
    headers: [
      { text: "Nombres", value: "nombre", align: "start" },
      { text: "Apellidos", value: "apellido", sortable: true },
      { text: "Dirección", value: "direccion", sortable: false },
      { text: "Edad", value: "edad", sortable: false },
      { text: "Profesión", value: "profesion", sortable: false },
      { text: "Estado Civil", value: "estadocivil", sortable: false },
      { text: "Nivel Riesgo", value: "opciones" },
    ],

    search: "",
    adNivelRiesgo: "",
  }),

  watch: {
    dialog(val) {
      val || this.reset();
    },
  },

  //   created() {
  //     this.listar();
  //   },

  methods: {
    listar() {
      let me = this;
      axios
        .get("ListarColaboradores")
        .then(function(response) {
          me.colaboradores = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    nivelRiesgo(item) {
      this.adNivelRiesgo = item.mensaje;
      this.nombreColaborador = item.nombre;
      this.dialogNivelRiesgo = true;
    },

    close() {
      this.dialogNivelRiesgo = false;
    },
  },
};
</script>

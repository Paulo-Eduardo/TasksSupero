const app = new Vue({
    el: "#app",
    data: {
      editandoTarefa: null,
      tarefas: [],
      novaTarefa: {
          Id: "",
          Title: "",
          Description: "",
          Complete: false
      }
    },
    methods: {
      deletarTarefa(tarefa) {
        fetch("http://localhost:8080/api/Tarefa", {
            body: JSON.stringify(tarefa),
            method: "DELETE",
            headers: {
              "Content-Type": "application/json",
            },
        })
        .then(() => {
            var index = this.tarefas.indexOf(tarefa);
            this.tarefas.splice(index, 1);
        })
      },
      atualizarTarefa(tarefa) {
        fetch("http://localhost:8080/api/Tarefa",{
          body: JSON.stringify(tarefa),
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        })
        .then(() => {
          this.editandoTarefa = null;
        })
      },
      cadastrarTarefa(tarefa) {
        fetch("http://localhost:8080/api/Tarefa",{
          body: JSON.stringify(tarefa),
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        })
        .then(response => response.json())
        .then((data) => {
            this.tarefas.push(data);
            this.novaTarefa.Title = "";
            this.novaTarefa.Description = "";
          })
      },
      switchStatusTarefa(tarefa){
        fetch("http://localhost:8080/api/Tarefa/" + tarefa.Id,{
          method: "PUT",
        })
        .then(() => {          
          var index = this.tarefas.indexOf(tarefa);
          this.tarefas[index].Complete = tarefa.Complete === false
        })
      }
    },
    mounted() {
      fetch("http://localhost:8080/api/Tarefa")
        .then(response => response.json())
        .then((data) => {
          this.tarefas = data;
        })
    },
    template: `
    <div>
      <h4>Criar nova tarefa:</h4>
      <div class="row">
        <div class="input-field col s3">
          <input placeholder="Titulo" id="title" type="text" class="validate" v-model="novaTarefa.Title" />
        </div>
        <div class="input-field col s3">
          <input placeholder="Descrição" id="description" type="text" class="validate" v-model="novaTarefa.Description" />
        </div>        
        <a class="waves-effect waves-light btn" v-on:click="cadastrarTarefa(novaTarefa)">save</a>
      </div>
      <h4>Minhas Tarefas</h4>     
      <table>
        <thead>
          <th> Titulo</th>
          <th> Descrição </th>
          <th> Completa </th>
          <th> Status </th>
        </thead>
        <tbody>
          <tr  v-for="tarefa in tarefas">
            <td><input v-on:keyup.13="atualizarTarefa(tarefa)" v-model="tarefa.Title" /></td> 
            <td><input v-on:keyup.13="atualizarTarefa(tarefa)" v-model="tarefa.Description" /></td>
            <td>
            <input v-if="tarefa.Complete" type="checkbox" id="test6" checked="checked" disabled="disabled" />
            <input v-else="tarefa.Complete" type="checkbox" id="test6" disabled="disabled"/>
            <label for="test6"></label>
          <td>
            <a class="waves-effect waves-light btn" v-on:click="switchStatusTarefa(tarefa)">Trocar Status</a>
            <a class="waves-effect waves-light btn" v-on:click="atualizarTarefa(tarefa)">Editar</a>
            <a class="waves-effect waves-light btn" v-on:click="deletarTarefa(tarefa)">Excluir</a>
          </td>
          </tr>
        </tbody>  
      </table>
    </div>
    `,
});
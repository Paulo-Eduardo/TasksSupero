using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksSupero.Models;

namespace TasksSupero.Test.Models
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void Get()
        {
            var tasks = Tarefa.Get();

            Assert.IsNotNull(tasks);
        }

        [TestMethod]
        public void CriarTarefa()
        {
            var tarefa = new Tarefa();
            tarefa.Title = "Nova Tarefa";
            tarefa.Description = "Tarefa para testar varias tarefas";

            tarefa.Create();
        }

        [TestMethod]
        public void EditarTarefa()
        {
            var getTask = Tarefa.Get();
            var task = getTask[0];
            task.Title = "Novo Nome";
            task.Description = "Nova descrição";

            task.Update();

            var taskEditada = Tarefa.Get(task.Id);
            Assert.AreEqual(taskEditada.Title, "Novo Nome");
            Assert.AreEqual(taskEditada.Description, "Nova descrição");
        }

        [TestMethod]
        public void AlterarStatusTarefa()
        {
            var getTask = Tarefa.Get();
            var task = getTask[0];
            var statusTask = task.Complete;

            task.SwitchStatus();

            var taskEditada = Tarefa.Get(task.Id);
            Assert.AreNotEqual(statusTask, taskEditada.Complete);
        }

        [TestMethod]
        public void DeleteTarefa()
        {
            var getTask = Tarefa.Get();
            var task = getTask[0];

            task.Delete();

            var taskExcluida = Tarefa.Get(task.Id);
            Assert.IsNull(taskExcluida);
        }
    }
}

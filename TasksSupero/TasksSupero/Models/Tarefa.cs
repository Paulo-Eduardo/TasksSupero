using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace TasksSupero.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }

        public static List<Tarefa> Get()
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                return db.Query<Tarefa>("Select * From Tarefa").ToList();
            }
        }

        public static Tarefa Get(Guid id)
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                return db.QueryFirstOrDefault<Tarefa>("Select * From Tarefa where Id = @idTask", new { idTask = id});
            }
        }


        public void Create()
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                this.Id = Guid.NewGuid();
                db.Execute("Insert into Tarefa (id, title, description, complete) values(@id, @title, @description, @complete)", new { id = Id.ToString(), title = Title, description = Description, complete = Complete});
            }
        }

        internal static void SwitchStatus(Guid id)
        {
            var task = Get(id);
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                task.Complete = task.Complete == false;
                db.Execute("update Tarefa set Complete = @complete where Id = @id", new { id = task.Id.ToString(), complete = task.Complete });
            }
        }

        public void SwitchStatus()
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                this.Complete = this.Complete == false;
                db.Execute("update Tarefa set Complete = @complete where Id = @id", new { id = Id.ToString(), complete = Complete });
            }
        }

        public void Delete()
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                db.Execute("delete from Tarefa where Id = @id", new { id = Id.ToString() });
            }
        }

        public void Update()
        {
            using (IDbConnection db = new MySqlConnection("Database=Tasks;Data Source=localhost;User Id=root;"))
            {
                db.Execute("update Tarefa set Title = @title, Description =  @description, Complete = @complete where Id = @id", new { id = Id.ToString(), title = Title, description = Description, complete = Complete});
            }
        }
    }
}

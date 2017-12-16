using System;
using System.Collections.Generic;
using System.Web.Http;
using TasksSupero.Models;

namespace TasksSupero.Controllers
{
    public class TarefaController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Tarefa> Get()
        {
            return Tarefa.Get().ToArray();

        }

        public Tarefa Post(Tarefa value)
        {
            value.Create();
            return value;
        }

       public IHttpActionResult Put(Tarefa value)
        {
            try
            {
                value.Update();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Delete(Tarefa value)
        {
            try
            {
                value.Delete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        public IHttpActionResult Put(Guid id)
        {
            Tarefa.SwitchStatus(id);
            return Ok();
        }
    }
}
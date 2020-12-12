using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using examenfinal.Models;

namespace examenfinal.Controllers
{
    [RoutePrefix("api")]
    public class holasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/holas
        public IQueryable<hola> Getholas()
        {
            return db.holas;
        }

        
        [HttpGet]
        [Route("{numero:int}")]
        public string Operacion(int numero)
        {
            if (numero < 0)
                return "Error";
            if (numero == 0)
                return "Realizado por Daniel";
            return "usted ingreso el numero" + numero.ToString();



        }








        // GET: api/holas/5
        [ResponseType(typeof(hola))]
        public IHttpActionResult Gethola(int id)
        {
            hola hola = db.holas.Find(id);
            if (hola == null)
            {
                return NotFound();
            }

            return Ok(hola);
        }

        // PUT: api/holas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthola(int id, hola hola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hola.numero)
            {
                return BadRequest();
            }

            db.Entry(hola).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!holaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/holas
        [ResponseType(typeof(hola))]
        public IHttpActionResult Posthola(hola hola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.holas.Add(hola);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hola.numero }, hola);
        }

        // DELETE: api/holas/5
        [ResponseType(typeof(hola))]
        public IHttpActionResult Deletehola(int id)
        {
            hola hola = db.holas.Find(id);
            if (hola == null)
            {
                return NotFound();
            }

            db.holas.Remove(hola);
            db.SaveChanges();

            return Ok(hola);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool holaExists(int id)
        {
            return db.holas.Count(e => e.numero == id) > 0;
        }
    }
}
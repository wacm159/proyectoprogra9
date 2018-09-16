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
using ProyectoP9.Models;

namespace ProyectoP9.Controllers
{
    public class VehiculosApiController : ApiController
    {
        private VikingsProyecEntities db = new VikingsProyecEntities();

        // GET: api/VehiculosApi
        public IQueryable<Vehiculos> GetVehiculos()
        {
            return db.Vehiculos;
        }

        // GET: api/VehiculosApi/5
        [ResponseType(typeof(Vehiculos))]
        public IHttpActionResult GetVehiculos(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            return Ok(vehiculos);
        }

        // PUT: api/VehiculosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehiculos(int id, Vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculos.id_vehiculo)
            {
                return BadRequest();
            }

            db.Entry(vehiculos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculosExists(id))
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

        // POST: api/VehiculosApi
        [ResponseType(typeof(Vehiculos))]
        public IHttpActionResult PostVehiculos(Vehiculos vehiculos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehiculos.Add(vehiculos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehiculos.id_vehiculo }, vehiculos);
        }

        // DELETE: api/VehiculosApi/5
        [ResponseType(typeof(Vehiculos))]
        public IHttpActionResult DeleteVehiculos(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return NotFound();
            }

            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();

            return Ok(vehiculos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiculosExists(int id)
        {
            return db.Vehiculos.Count(e => e.id_vehiculo == id) > 0;
        }
    }
}
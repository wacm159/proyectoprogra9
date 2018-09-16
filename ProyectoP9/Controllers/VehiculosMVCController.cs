using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProyectoP9.Models;

namespace ProyectoP9.Controllers
{
    public class VehiculosMVCController : Controller
    {
        private VikingsProyecEntities db = new VikingsProyecEntities();
        //Vistas del Index

        // GET: VehiculosMVC
        public ActionResult Index()
        {
        
            IEnumerable<Vehiculos> alumnos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54520/api/");
                //GET GETAlumnos
                //el siguente codigo obtiene la informacion de manera asincrona y espera hata obtener la data
                var reponseTask = client.GetAsync("VehiculosApi");
                reponseTask.Wait();
                var result = reponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // leer todo el cotenido y lo parseamos a una lista de alumno
                    var leer = result.Content.ReadAsAsync<IList<Vehiculos>>();
                    leer.Wait();
                    alumnos = leer.Result;
                }
                else
                {
                    alumnos = Enumerable.Empty<Vehiculos>();
                    ModelState.AddModelError(string.Empty, "Error...");
                }

            }
            return View(alumnos.ToList());
        }


        //Vista del detalle
        // GET: VehiculosMVC/Details/5
        public ActionResult Details(int? id)
        {
             //Codigo para el detalle  como restfull

            Vehiculos vehiculos = new Vehiculos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54520/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("VehiculosApi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<Vehiculos>();
                    leer.Wait();
                    vehiculos = leer.Result;
                }
                else
                {
                    vehiculos = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: VehiculosMVC/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nit");
            ViewBag.ubicacion = new SelectList(db.Ubicacion, "id_ubicacion", "Direccion");
            return View();
        }

        // POST: VehiculosMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_vehiculo,placa,marca,modelo,año_creacion,cilindros,cantidad_llantas,motor,id_cliente,ubicacion")] Vehiculos vehiculos)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Vehiculos.Add(vehiculos);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_cliente);
            //ViewBag.ubicacion = new SelectList(db.Ubicacion, "id_ubicacion", "Direccion", vehiculos.ubicacion);
            //return View(vehiculos);


            //cambio de create
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54520/api/");
                    //HTTP Post
                    var postTask = client.PostAsJsonAsync<Vehiculos>("vehiculosapi", vehiculos);
                    postTask.Wait();

                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            }
            return RedirectToAction("Index");

        }

        //editar vehiculos con rest

        // GET: VehiculosMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Vehiculos vehiculos = db.Vehiculos.Find(id);
            //    if (vehiculos == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_cliente);
            //    ViewBag.ubicacion = new SelectList(db.Ubicacion, "id_ubicacion", "Direccion", vehiculos.ubicacion);
            //    return View(vehiculos);
            Vehiculos vehiculos = new Vehiculos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54520/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("vehiculosapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<Vehiculos>();
                    leer.Wait();
                    vehiculos = leer.Result;
                }
                else
                {
                    vehiculos = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);

        }

        // POST: VehiculosMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_vehiculo,placa,marca,modelo,año_creacion,cilindros,cantidad_llantas,motor,id_cliente,ubicacion")] Vehiculos vehiculos)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(vehiculos).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.id_cliente = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_cliente);
            //ViewBag.ubicacion = new SelectList(db.Ubicacion, "id_ubicacion", "Direccion", vehiculos.ubicacion);
            //return View(vehiculos);
            // nuevo metodo utilizando el Json y el uri del restfull

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54520/api/");
                    //HTTP Post
                    var postTask = client.PutAsJsonAsync<Vehiculos>("vehiculosapi", vehiculos);
                    postTask.Wait();

                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            }
            return RedirectToAction("Index");
        }

        // GET: VehiculosMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Vehiculos vehiculos = db.Vehiculos.Find(id);
            //if (vehiculos == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(vehiculos);

            // eliminar almnos utilizando el Json y el restfull
            Vehiculos vehiculos = new Vehiculos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54520/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("vehiculosapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<Vehiculos>();
                    leer.Wait();
                    vehiculos = leer.Result;
                }
                else
                {
                    vehiculos = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: VehiculosMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Vehiculos vehiculos = db.Vehiculos.Find(id);
            //db.Vehiculos.Remove(vehiculos);
            //db.SaveChanges();
            //return RedirectToAction("Index");


            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54520/api/");
                    //HTTP Post
                    var postTask = client.DeleteAsync("vehiculosapi/" + id.ToString());
                    postTask.Wait();

                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercción,favor contacte al administrador");
            }
            return RedirectToAction("Index");


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

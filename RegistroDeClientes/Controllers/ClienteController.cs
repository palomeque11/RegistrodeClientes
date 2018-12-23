using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistroDeClientes.Models;

namespace RegistroDeClientes.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult AgregarCliente(int id=0)
        {
            Cliente cliente = new Cliente();
            return View(cliente);
        }
        [HttpPost]
        public ActionResult AgregarCliente(Cliente cliente)
        {
            using(DbModel db = new DbModel())
            {
                if(db.Cliente.Any(x => x.Nombre== cliente.Nombre) && 
                    db.Cliente.Any(x => x.Apellidos == cliente.Apellidos))
                {
                    ViewBag.ErrorMensage = "Cliente registrado anteriormente.";
                    return View("AgregarCliente", cliente);
                }
                db.Cliente.Add(cliente);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMensage = "Registro Existoso.";
            return View("AgregarCliente",new Cliente());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AdministradorCanales.Entities;
using AdministradorCanales.Models;

namespace AdministradorCanales.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioModel usuarioModel = new UsuarioModel();

        // GET: Login
        public ActionResult Index()
        {
            return View("Index", new Usuario());
        }

        [HttpPost]
        public ActionResult Login(string correo, string contrasena)
        {
            if (correo.Length != 0 && contrasena.Length != 0)
            {
                var usuario = usuarioModel.login(correo, contrasena);
                if (usuario != null)
                {
                    Session["usuario_id"] = usuario.Id;
                    Session["usuario_nombre"] = usuario.Nombre;
                    Session["usuario_apellido"] = usuario.Apellido;
                    Session["usuario_rol"] = usuario.Rol;
                   
                    return RedirectToAction("Index", "Management");
                }
            }
            ViewBag.error = "Correo y/o contraseña incorrecta";
            return Index();
        } 
    }
}
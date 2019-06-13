using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdministradorCanales.Entities;
using AdministradorCanales.Models;
namespace AdministradorCanales.Controllers
{
    public class UserController : Controller
    {
        private ChatModel chatModel = new ChatModel();
        private MensajeModel mensajeModel = new MensajeModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Chat()
        {
            ViewBag.CreateChat = true;
            ViewBag.Mensajes = new Mensaje[0];
            return View("Chat");
        }

        [HttpPost]
        public ActionResult Chat(string asunto)
        {
            ViewBag.CreateChat = false;

            Chat chat = new Chat();
            chat.Asunto = asunto;
            chat.Canal = "mensajeria";
            chat.IdUsuario = Session["usuario_id"].ToString();
            chat.Status = true;
            chat.Fecha = DateTime.Now;
            Session["chat_id"] = chatModel.ingresar(chat);
            return View("Chat");
        }

        [HttpPost]
        public ActionResult SendMessage(string texto)
        {
            if(texto.Length > 0)
            {
                ViewBag.CreateChat = false;

                Mensaje mensaje = new Mensaje();
                mensaje.IdChat = Session["chat_id"].ToString();
                mensaje.IdUsuario = Session["usuario_id"].ToString();
                mensaje.Texto = texto;
                mensaje.Fecha = DateTime.Now;
                ViewBag.Mensajes = mensajeModel.ingresar(mensaje);
            }
            
            return View("Chat");
        }
    }
}
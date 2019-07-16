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
            ViewBag.userId = Session["usuario_id"].ToString();
            ViewBag.CreateChat = true;
            ViewBag.Mensajes = new Mensaje[0];
            
            return View("Chat");
        }

        [HttpPost]
        public ActionResult Chat(string asunto)
        {
            ViewBag.CreateChat = false;
            ViewBag.userId = Session["usuario_id"].ToString();
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
            ViewBag.userId = Session["usuario_id"].ToString();
            List<Mensaje> mensajes = new List<Mensaje>();
            if (texto.Length > 0)
            {
                ViewBag.CreateChat = false;

                Mensaje mensaje = new Mensaje();
                mensaje.IdChat = Session["chat_id"].ToString();
                mensaje.IdUsuario = Session["usuario_id"].ToString();
                mensaje.Texto = texto;
                mensaje.Fecha = DateTime.Now;
                
                mensajes= mensajeModel.ingresar(mensaje);
                ViewBag.Mensajes = mensajes;
            }
            
            return View("Chat", mensajes);
        }

        public ActionResult SendMessage()
        {
            
            ViewBag.userId = Session["usuario_id"].ToString();
            List<Mensaje> mensajes = new List<Mensaje>();
            if (Session["chat_id"] != null)
            {
                string id = Session["chat_id"].ToString();
                ViewBag.Mensajes = mensajeModel.mensajesPorChat(id);
                mensajes = mensajeModel.mensajesPorChat(id);
            }

            return View("Chat", mensajes);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using AdministradorCanales.Entities;
using AdministradorCanales.Models;

namespace AdministradorCanales.Controllers
{
    public class ManagementController : Controller
    {
        private ChatModel chatModel = new ChatModel();
        private MensajeModel mensajeModel = new MensajeModel();
        private UsuarioModel usuarioModel = new UsuarioModel();
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View("About", new Usuario());
        }

        [HttpPost]
        public ActionResult About(string asunto, string destinatario, string texto)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "f7611bbfa0a597219bfe5f33a8a7f6dc-16ffd509-783f7c82");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox8287bcebf99047a7acedf54618d1c36d.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Usuario Tesis <mailgun@sandbox8287bcebf99047a7acedf54618d1c36d.mailgun.org>");
            request.AddParameter("to", destinatario);
            request.AddParameter("subject", asunto);
            request.AddParameter("text", texto);
            request.Method = Method.POST;
            client.Execute(request);
            return About();
        }

        public ActionResult Chat()
        {
            ViewBag.userId = Session["usuario_id"];
            ViewBag.CreateChat = true;
            ViewBag.ListaChat = getListChats();
            return View("Chat");
        }

        [HttpPost]
        public ActionResult Chat(string chatId, string mensaje)
        {
            if (mensaje != null)
            {
                if(mensaje.Length > 0)
                {
                    Mensaje nuevoMensaje = new Mensaje();
                    nuevoMensaje.IdChat = Session["chat_id"].ToString();
                    nuevoMensaje.IdUsuario = Session["usuario_id"].ToString();
                    nuevoMensaje.Texto = mensaje;
                    nuevoMensaje.Fecha = DateTime.Now;
                    mensajeModel.ingresar(nuevoMensaje);
                }
            }
            if (chatId == null)
            {
                chatId = Session["chat_id"].ToString();
                ViewBag.userId = Session["usuario_id"];
                List<Mensaje> mensajes = mensajeModel.mensajesPorChat(chatId);
                ViewBag.ListaMensajes = mensajes;
                ViewBag.ListaChat = getListChats();
            }
            else
            {
                Session["chat_id"] = chatId;
                ViewBag.userId = Session["usuario_id"];
                List<Mensaje> mensajes = mensajeModel.mensajesPorChat(chatId);
                ViewBag.ListaMensajes = mensajes;
                ViewBag.ListaChat = getListChats();
            }
            return View("Chat");
        }

        public List<ItemChat> getListChats()
        {
            List<Chat> chats = chatModel.chatNoCerrados();
            List<ItemChat> arregloChats = new List<ItemChat>();
            foreach (var chat in chats)
            {
                Usuario usuario = usuarioModel.buscar(chat.IdUsuario);
                ItemChat item = new ItemChat();
                List<Mensaje> mensajes = mensajeModel.mensajesPorChat(chat.Id.ToString()); ;
                item.Asunto = chat.Asunto;
                item.ChatId = chat.Id.ToString();
                item.UserId = chat.IdUsuario.ToString();
                item.UserName = usuario.Nombre + " " + usuario.Apellido;
                item.Mensajes = mensajes.Count();
                arregloChats.Add(item);
            }

            return arregloChats;
        }
        

    }
}
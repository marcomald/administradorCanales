using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdministradorCanales.Entities;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;

using System.Configuration;
namespace AdministradorCanales.Models
{
    public class ChatModel
    {
        private MongoClient mongoClient;
        private IMongoCollection<Chat> chatCollection;

        public ChatModel()
        {
            mongoClient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);

            chatCollection = db.GetCollection<Chat>("chat");
        }

        public List<Chat> todos()
        {
            return chatCollection.AsQueryable<Chat>().ToList();
        }

        public List<Chat> chatNoCerrados()
        {
            return chatCollection.AsQueryable().Where(chat => chat.Status == true).ToList();
        }

        public Chat buscar(string id)
        {
            var chatId = new ObjectId(id);
            return chatCollection.AsQueryable<Chat>().SingleOrDefault(chat => chat.Id == chatId);
        }

        public string ingresar(Chat chat)
        {
            chatCollection.InsertOne(chat);
            string chatId = chatCollection.AsQueryable<Chat>().SingleOrDefault(c => c.Asunto == chat.Asunto).Id.ToString();
            return chatId;
        }

        public void actualizar(Chat chat)
        {
            chatCollection.UpdateOne(
                Builders<Chat>.Filter.Eq("_id", chat.Id),
                Builders<Chat>.Update
                .Set("idusuario", chat.IdUsuario)
                .Set("status", chat.Status)
                .Set("canal", chat.Canal)
                .Set("asunto", chat.Asunto)
                .Set("fecha", chat.Fecha)
            );
        }

        public void eliinar(string id)
        {
            chatCollection.DeleteOne(Builders<Chat>.Filter.Eq("_id", ObjectId.Parse(id)));
        }
    }
}
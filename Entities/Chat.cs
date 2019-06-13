using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdministradorCanales.Entities
{
    [BsonIgnoreExtraElements]
    public class Chat
    {
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }

        [BsonElement("idusuario")]
        public string IdUsuario
        {
            get;
            set;
        }

        [BsonElement("canal")]
        public string Canal
        {
            get;
            set;
        }

        [BsonElement("asunto")]
        public string Asunto
        {
            get;
            set;
        }

        [BsonElement("fecha")]
        public DateTime Fecha
        {
            get;
            set;
        }

        [BsonElement("status")]
        public bool Status
        {
            get;
            set;
        }
    }
}
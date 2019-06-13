using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdministradorCanales.Entities
{
    [BsonIgnoreExtraElements]
    public class Mensaje
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

        [BsonElement("idchat")]
        public string IdChat
        {
            get;
            set;
        }

        [BsonElement("texto")]
        public string Texto
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
    }
}
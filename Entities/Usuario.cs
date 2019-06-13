using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdministradorCanales.Entities
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        [BsonId]
        public ObjectId Id
        {
            get;
            set;
        }

        [BsonElement("nombre")]
        public string Nombre
        {
            get;
            set;
        }

        [BsonElement("apellido")]
        public string Apellido
        {
            get;
            set;
        }

        [BsonElement("correo")]
        public string Correo
        {
            get;
            set;
        }

        [BsonElement("contrasena")]
        public string Contrasena
        {
            get;
            set;
        }

        [BsonElement("telefono")]
        public string Telefono
        {
            get;
            set;
        }

        [BsonElement("rol")]
        public bool Rol
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
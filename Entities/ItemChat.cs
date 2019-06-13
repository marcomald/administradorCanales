using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdministradorCanales.Entities
{
    public class ItemChat
    {
        public string UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string ChatId
        {
            get;
            set;
        }

        public string Asunto
        {
            get;
            set;
        }

        public int Mensajes
        {
            get;
            set;
        }

    }
}
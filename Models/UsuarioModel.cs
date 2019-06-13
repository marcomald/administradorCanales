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
    public class UsuarioModel
    {
        private MongoClient mongoClient;
        private IMongoCollection<Usuario> usuarioCollection;

        public UsuarioModel()
        {
            mongoClient = new MongoClient(ConfigurationManager.AppSettings["mongoDBHost"]);
            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["mongoDBName"]);

            usuarioCollection = db.GetCollection<Usuario>("usuario");
        }

        public List<Usuario> todos()
        {
            return usuarioCollection.AsQueryable<Usuario>().ToList();
        }

        public Usuario buscar(string id)
        {
            var idUsuario = new ObjectId(id);
            return usuarioCollection.AsQueryable<Usuario>().SingleOrDefault(u => u.Id == idUsuario);
        }

        public Usuario login(string correo, string contrasena)
        {
            return usuarioCollection.AsQueryable<Usuario>().SingleOrDefault(u => u.Correo == correo && u.Contrasena == contrasena);
        }

        public void ingresar(Usuario usuario)
        {
            usuarioCollection.InsertOne(usuario);
        }

        public void actualizar(Usuario usuario)
        {
            usuarioCollection.UpdateOne(
                Builders<Usuario>.Filter.Eq("_id", usuario.Id),
                Builders<Usuario>.Update
                .Set("nombre", usuario.Nombre)
                .Set("apellido", usuario.Apellido)
                .Set("correo", usuario.Correo)
                .Set("contrasena", usuario.Contrasena)
                .Set("telefono", usuario.Telefono)
                .Set("rol", usuario.Rol)
                .Set("status", usuario.Status)
            );
        }

        public void eliminar(string id)
        {
            usuarioCollection.DeleteOne(Builders<Usuario>.Filter.Eq("_id", ObjectId.Parse(id)));
        }
    }
}
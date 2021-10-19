using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veiculo.Dominio;

namespace Veiculo.Repositorio
{
    public static class UserRepo
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Usuario = "batman", Senha = "batman", Role = "manager" });
            users.Add(new User { Id = 2, Usuario = "robin", Senha = "robin", Role = "employee" });
            users.Add(new User { Id = 3, Usuario = "miranha", Senha = "miranha", Role = "mechanic" });
            users.Add(new User { Id = 4, Usuario = "thor", Senha = "thor", Role = "support" });
            return users.Where(x => x.Usuario.ToLower() == username.ToLower() && x.Senha == x.Senha).FirstOrDefault();
        }
    }
}

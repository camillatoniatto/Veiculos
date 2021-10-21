using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculo.Dominio
{
    public class User
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        //public string Role { get; set; }
        protected List<UserRole> UsersRoles { get; set; }
    }
}

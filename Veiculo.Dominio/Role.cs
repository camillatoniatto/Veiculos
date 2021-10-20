using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculo.Dominio
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        private List<UserRole> UsersRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculo.Dominio
{
    public class UserRole
    {
        public int UserId { get; set; }    
        public int RoleId { get; set; }
        //public string Usuario { get; set; }
        private User User { get; set; }
        private Role Role { get; set; }
        //public bool Selected { get; set; } //determine if the user is selected to be a member of the role.
    }
}

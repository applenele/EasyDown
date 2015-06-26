using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDown.Entity
{
    [Table("t_user")]
    public class User
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public int RoleAsInt { get; set; }
        

        [Column("picture")]
        public byte[] Picture { set; get; }

        [NotMapped]
        public Role Role
        {
            get { return (Role)RoleAsInt; }
            set { RoleAsInt = (int)Role; }
        }

        public virtual ICollection<Resource> Resources { set; get; }

    }

   
}

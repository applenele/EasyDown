using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDown.Entity
{
    [Table("t_reply")]
    public class Reply
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("ptime")]
        public DateTime Ptime { get; set; }

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserID { set; get; }

        public virtual User  User { get; set; }

        [Column("resource_id")]
        [ForeignKey("Resource")]
        public int ResourceID { get; set; }


        public virtual Resource Resource { get; set; }
    }
}

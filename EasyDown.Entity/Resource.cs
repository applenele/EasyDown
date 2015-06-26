using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDown.Entity
{
    [Table("t_resources")]
    public class Resource
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("resource_name")]
        public string  ResourceName { get; set; }
       

        [Column("description")]
        public string Description { get; set; }

        [Column("resource_type")]
        public int  TypeAsInt  { get; set; }
        [Column("upload_time")]
        public DateTime  UploadTime { get; set; }

        [Column("path")]
        public string Path { get; set; }

        [NotMapped]
        public Type Type
        {
            get { return (Type)TypeAsInt; }
            set { TypeAsInt = (int)Type; }
        }

        [Column("userId")]
        [ForeignKey("User")]
        public int UserId { set; get; }

        public virtual User User { set; get; }

        public virtual ICollection<Reply> Replies { get; set; }


        [Column("icon")]
        public byte[] Icon { get; set; }
       
    }


    public enum Type { Word, Xls, PPT, 视屏,音乐,图片,压缩,其他 }
}

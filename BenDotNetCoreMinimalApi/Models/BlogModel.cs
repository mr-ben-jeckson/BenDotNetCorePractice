using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDotNetCoreMinimalApi.Models
{
    [Table("Blogs")]
    public class BlogModel
    {
        [Key]
        [Column("BlogId")] // Not the same column name and obj attribute name
        public int BlogId { get; set; }

        [Column("BlogAuthor")]
        public string? BlogAuthor { get; set; }

        [Column("BlogTitle")]
        public string? BlogTitle { get; set; }

        [Column("BlogContent")]
        public string? BlogContent { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}

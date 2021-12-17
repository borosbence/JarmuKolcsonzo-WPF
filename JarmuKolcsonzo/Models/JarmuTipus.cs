using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JarmuKolcsonzo.Models
{
    [Table("jarmu_tipus")]
    public partial class JarmuTipus
    {
        public JarmuTipus()
        {
            jarmu = new HashSet<Jarmu>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string megnevezes { get; set; }
        [Column(TypeName = "int(2)")]
        public int ferohely { get; set; }

        [InverseProperty("tipus")]
        public virtual ICollection<Jarmu> jarmu { get; set; }
    }
}

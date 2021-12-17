using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JarmuKolcsonzo.Models
{
    [Table("ugyfel")]
    public partial class Ugyfel
    {
        public Ugyfel()
        {
            rendeles = new HashSet<Rendeles>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Required]
        [StringLength(20)]
        public string vezeteknev { get; set; }
        [Required]
        [StringLength(20)]
        public string keresztnev { get; set; }
        [Required]
        [StringLength(50)]
        public string varos { get; set; }
        [Column(TypeName = "int(4)")]
        public int iranyitoszam { get; set; }
        [Required]
        [StringLength(250)]
        public string cim { get; set; }
        [Required]
        [StringLength(12)]
        public string telefonszam { get; set; }
        [Required]
        [StringLength(100)]
        public string email { get; set; }
        public decimal pont { get; set; }

        [InverseProperty("ugyfel")]
        public virtual ICollection<Rendeles> rendeles { get; set; }
    }
}

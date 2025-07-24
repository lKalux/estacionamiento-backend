using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEstacionamientoBackend.Models
{
    public class Auto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Marca { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(10)]
        public string Patente { get; set; }

        public int? Anio { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

    }
}

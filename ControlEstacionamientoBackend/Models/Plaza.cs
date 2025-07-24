using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlEstacionamientoBackend.Models
{
    public class Plaza
    {
        [Key]
        public int Id { get; set; }

        public int? ClienteId { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroPlaza { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado {  get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public DateTime? FechaRevocacion {  get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ControlEstacionamientoBackend.DTOs
{
    public class AutoInputDTO
    {
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
    }
}

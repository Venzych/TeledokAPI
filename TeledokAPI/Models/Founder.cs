using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeledokAPI.Models
{
    public class Founder
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 10)]
        public string INN { get; set; } // ИНН клиента

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } // ФИО учредителя

        public DateTime DateOfCreation { get; set; } // Дата создания
        public DateTime DateOfModification { get; set; } // Дата обновления

        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}

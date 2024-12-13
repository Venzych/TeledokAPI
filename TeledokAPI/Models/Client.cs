using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TeledokAPI.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 10)]
        public string INN { get; set; } // ИНН клиента

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } // Наименование клиента

        [Required]
        public ClientType Type {  get; set; } // Тип клиента (ЮЛ/ИП)

        public DateTime DateOfCreation { get; set; } = DateTime.Now; // Дата добавления
        public DateTime DateOfModification { get; set; } = DateTime.Now; // Дата обновления

        public ICollection<Founder> Founders { get; set; } = new List<Founder>(); 
    }
    public enum ClientType
    {
        LegalEntity, // Юридическое лицо
        SoleTrader // Индивидуальный предприниматель
    }
}

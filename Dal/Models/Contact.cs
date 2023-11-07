using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Dal.Models
{
    [Table("Contacts")]
	public class Contact
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required string Fullname { get; set; }

        public required string Email { get; set; }

        [JsonIgnore]
        [ForeignKey("ContragentId")]
        public int ContragentId { get; set; }

        public virtual Contragent Contragent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dal.Models;

namespace Api.Controllers.DTO.ResponseModels
{
	public class ContactResponseModel
	{

        public int Id { get; set; }
        
        public string Fullname { get; set; }

        public string Email { get; set; }

        public ContragentResponseModel Contragent { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ContactResponseModel(Contact contact)
        {
            Id = contact.Id;
            Fullname = contact.Fullname;
            Email = contact.Email;
            Contragent = new ContragentResponseModel(contact.Contragent);
            CreatedAt = contact.CreatedAt;
            UpdatedAt = contact.UpdatedAt;
        }
    }
}


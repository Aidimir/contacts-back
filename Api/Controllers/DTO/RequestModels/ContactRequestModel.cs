using Dal.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.DTO.RequestModels
{
    public class ContactRequestModel : IPublicContact
    {
        [Required]
        [Fullname]
        public string Fullname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Contragent { get; set; }
    }
}


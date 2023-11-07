using System;
using System.ComponentModel.DataAnnotations;

namespace Dal.Interfaces
{
	public interface IPublicContact
	{
        [Required]
        [MinLength(3)]
        public string Fullname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Contragent { get; set; }
    }
}


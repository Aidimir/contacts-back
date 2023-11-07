using System;
using System.ComponentModel.DataAnnotations;
using Dal.Interfaces;

namespace Api.Controllers.DTO.RequestModels
{
    public class ContragentRequestModel : IPublicContragent
    {
        [Required]
        public string Name { get; set; }
    }
}


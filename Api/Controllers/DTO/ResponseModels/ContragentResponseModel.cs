using System;
using Dal.Interfaces;
using Dal.Models;

namespace Api.Controllers.DTO.ResponseModels
{
    public class ContragentResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ContragentResponseModel(Contragent contragent)
        {
            Id = contragent.Id;
            Name = contragent.Name;
            CreatedAt = contragent.CreatedAt;
            UpdatedAt = contragent.UpdatedAt;
        }
    }
}


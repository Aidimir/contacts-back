using System;
using Dal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Interfaces
{
	public interface IPublicContragent
	{
        public string Name { get; set; }
    }
}
using Api.Controllers.DTO.ResponseModels;
using Api.Controllers.DTO.RequestModels;
using Dal.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _service;

    public ContactsController(IContactsService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContactResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultErrorResponseModel))]
    public async Task<ActionResult> Create(ContactRequestModel request)
    {
        var contact = new Contact { Email = request.Email, Fullname = request.Fullname };
        var createdContact = await _service.CreateContact(contact, request.Contragent);
        var result = new ContactResponseModel(createdContact);

        return StatusCode(201, result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultErrorResponseModel))]
    public async Task<ActionResult> FetchContacts(int? contactId, string? contactEmail,
                                                                        string? contactName, string? contactsContrage)
    {
        var contacts = await _service.FetchContacts(id: contactId, nameFilter: contactName,
                                            emailFilter: contactEmail, contragentFilter: contactsContrage);
        var result = contacts.Select(c => new ContactResponseModel(c));

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> DeleteContact(int id)
    {
        await _service.DeleteContact(id);

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResponseModel))]
    public async Task<ActionResult> UpdateContact(int id, ContactRequestModel contact)
    {
        var updatedContact = await _service.UpdateContact(id, contact);
        var result = new ContactResponseModel(updatedContact);

        return StatusCode(200, result);
    }
}


using System;
using Api.Controllers.DTO.RequestModels;
using Api.Controllers.DTO.ResponseModels;
using Dal.Interfaces;
using Dal.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContragentsController : ControllerBase
{
    private readonly IContragentsService _service;

    public ContragentsController(IContragentsService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContragentResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultErrorResponseModel))]
    public async Task<ActionResult> Create(string name)
    {
        var contragent = new Contragent { Name = name };
        var createdContragent= await _service.CreateContragent(contragent);
        var result = new ContragentResponseModel(createdContragent);

        return StatusCode(201, result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContragentResponseModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DefaultErrorResponseModel))]
    public async Task<ActionResult> FetchContragents(int? contragentId, string? contragentName)
    {
        var contacts = await _service.FetchContragents(id: contragentId, nameFilter: contragentName);
        var result = contacts.Select(c => new ContragentResponseModel(c));

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> DeleteContragent(int id)
    {
        await _service.DeleteContragent(id);

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContragentResponseModel))]
    public async Task<ActionResult> UpdateContact(int id, ContragentRequestModel updatedContragent)
    {
        var updatedContragentModel = await _service.UpdateContragent(id, updatedContragent);
        var result = new ContragentResponseModel(updatedContragentModel);

        return StatusCode(200, result);
    }
}


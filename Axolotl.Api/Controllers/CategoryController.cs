using MediatR;
using Microsoft.AspNetCore.Mvc;
using Axolotl.Application.UseCases.Categories.Commands.CreateCommand;
using Axolotl.Application.UseCases.Categories.Commands.DeleteCommand;
using Axolotl.Application.UseCases.Categories.Commands.UpdateCommand;
using Axolotl.Application.UseCases.Categories.Queries.GetAllQuery;
using Axolotl.Application.UseCases.Categories.Queries.GetByIdQuery;
using Axolotl.Application.UseCases.Categories.Queries.GetSelectQuery;

namespace Axolotl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> CategoriesList([FromQuery] GetAllCategoriesQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> CategoriesSelect()
    {
        var response = await _mediator.Send(new GetSelectCategoriesQuery());
        return Ok(response);
    }

    [HttpGet("{categoriesId:int}")]
    public async Task<IActionResult> CategoriesById(Guid categoriesId)
    {
        var response = await _mediator.Send(new GetCategoriesByIdQuery() { CategoriesId = categoriesId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CategoriesCreate([FromBody] CreateCategoriesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> CategoriesUpdate([FromBody] UpdateCategoriesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Delete/{categoriesId:int}")]
    public async Task<IActionResult> CategoriesDelete(Guid categoriesId)
    {
        var response = await _mediator.Send(new DeleteCategoriesCommand() { CategoriesId = categoriesId });
        return Ok(response);
    }
}
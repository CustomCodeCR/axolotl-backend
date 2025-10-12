using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Axolotl.Domain.Entities;
using Axolotl.Application.Dtos;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Application.Interfaces.Persistence;

namespace Axolotl.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Products> _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Products> repo,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _repo = repo;
        _uow = uow;
        _mapper = mapper;
    }

    // GET: api/v1/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsDto>>> Get()
    {
        var list = await _repo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ProductsDto>>(list));
    }

    // GET: api/v1/Products/{uuid}
    [HttpGet("{uuid:guid}")]
    public async Task<ActionResult<ProductsDto>> GetByUuid(Guid uuid)
    {
        var entity = await _repo.GetByUUIDAsync(uuid);
        return entity is null ? NotFound() : Ok(_mapper.Map<ProductsDto>(entity));
    }

    // POST: api/v1/Products
    [HttpPost]
    public async Task<ActionResult<ProductsDto>> Create(ProductsDto? dto)
    {
        if (dto is null) return BadRequest();

        var entity = _mapper.Map<Products>(dto);
        await _repo.CreateAsync(entity);
        await _uow.SaveChangesAsync();

        var result = _mapper.Map<ProductsDto>(entity);
        return CreatedAtAction(nameof(GetByUuid), new { uuid = result.Uuid }, result);
    }

    // PUT: api/v1/Products/{uuid}
    [HttpPut("{uuid:guid}")]
    public async Task<IActionResult> Update(Guid uuid, ProductsDto? dto)
    {
        if (dto is null) return BadRequest();

        // (Opcional) si tu DTO tiene Uuid, evitar inconsistencias:
        // if (dto.Uuid != Guid.Empty && dto.Uuid != uuid) return BadRequest();

        var entity = await _repo.GetByUUIDAsync(uuid);
        if (entity is null) return NotFound();

        _mapper.Map(dto, entity);
        _repo.UpdateAsync(entity);          // void según tu interfaz
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/v1/Products/{uuid}
    [HttpDelete("{uuid:guid}")]
    public async Task<IActionResult> Delete(Guid uuid)
    {
        await _repo.DeleteAsync(uuid);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}

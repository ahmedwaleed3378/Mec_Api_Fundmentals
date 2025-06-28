

using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Models.Exceptions;
using Mec_Api_Fundmentals.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


// Controllers/BaseController.cs
[Route("api/[controller]")]
[ApiController]
public class BaseController<T> : ControllerBase where T : class
{
    private readonly IGenericRepository<T> _repository;

    public BaseController(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<T>>>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<T>>.CreateSuccess(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<T>>> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new ApiException($"{typeof(T).Name} with ID {id} not found", StatusCodes.Status404NotFound);

        return Ok(ApiResponse<T>.CreateSuccess(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<T>>> Create(T entity)
    {
        if (entity == null)
            throw new ApiException("Entity cannot be null", StatusCodes.Status400BadRequest);

        var createdEntity = await _repository.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), 
            new { id = GetEntityId(createdEntity) }, 
            ApiResponse<T>.CreateSuccess(createdEntity, $"{typeof(T).Name} created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<T>>> Update(int id, T entity)
    {
        if (entity == null)
            throw new ApiException("Entity cannot be null", StatusCodes.Status400BadRequest);

        var updatedEntity = await _repository.UpdateAsync(id, entity);
        return Ok(ApiResponse<T>.CreateSuccess(updatedEntity, $"{typeof(T).Name} updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (!result)
            throw new ApiException($"{typeof(T).Name} with ID {id} not found", StatusCodes.Status404NotFound);

        return Ok(ApiResponse<object>.CreateSuccess(null, $"{typeof(T).Name} deleted successfully"));
    }

    protected virtual object GetEntityId(T entity)
    {
        return entity.GetType().GetProperty("Id")?.GetValue(entity) 
            ?? throw new ApiException("Entity must have an Id property");
    }
}

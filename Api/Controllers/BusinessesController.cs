using Api.Dtos.Businesses;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BusinessesController : ControllerBase
{
    private readonly ILogger<BusinessesController> _logger;
    private readonly IRepository<Business> _businessRepository;

    public BusinessesController(
        ILogger<BusinessesController> logger,
        IRepository<Business> businessRepository)
    {
        _logger = logger;
        _businessRepository = businessRepository;
    }

    [HttpPost]
    public virtual async Task<ActionResult<BusinessDto>> Post(CreateBusinessDto createBusinessDto)
    {
        var business = new Business("", createBusinessDto.Name, createBusinessDto.Description);
        await _businessRepository.InsertAsync(business);

        return CreatedAtAction(
            nameof(Get),
            new { id = business.Id },
            new BusinessDto { Id = business.Id, Name = business.Name, Description = business.Description });
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<Business>> Get(int id)
    {
        var business = await _businessRepository.GetAsync(id);

        if (business is null)
            return NotFound();
        
        return Ok(new BusinessDto { Id = business.Id, Name = business.Name, Description = business.Description });
    }
    
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, UpdateBusinessDto businessDto)
    {
        if (id != businessDto.Id)
            return BadRequest();

        var business = await _businessRepository.GetAsync(id);
        if (business is null)
            return NotFound();

        business.UpdateDetails(businessDto.Name, businessDto.Description);

        await _businessRepository.UpdateAsync(business);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var business = await _businessRepository.GetAsync(id);
        
        if (business is null)
            return NotFound();
        
        await _businessRepository.DeleteAsync(business);
        return NoContent();
    }
}
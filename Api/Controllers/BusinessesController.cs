using Api.Dtos.Businesses;
using Api.Dtos.Pictures;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BusinessesController : ControllerBase
{
    private readonly ILogger<BusinessesController> _logger;
    private readonly IRepository<Business> _businessRepository;
    private readonly IRepository<Picture> _pictureRepository;
    private readonly IFileService _fileService;

    public BusinessesController(
        ILogger<BusinessesController> logger,
        IRepository<Business> businessRepository,
        IRepository<Picture> pictureRepository,
        IFileService fileService)
    {
        _logger = logger;
        _businessRepository = businessRepository;
        _fileService = fileService;
        _pictureRepository = pictureRepository;
    }

    [HttpPost]
    public virtual async Task<ActionResult<BusinessDto>> Post([FromForm] CreateBusinessDto createBusinessDto)
    {
        var business = new Business(
            User.Identity?.Name!,
            createBusinessDto.Name,
            createBusinessDto.Description);
            
        await _businessRepository.InsertAsync(business);
        
        return CreatedAtAction(
            nameof(Get),
            new { id = business.Id },
            new BusinessDto { Id = business.Id, Name = business.Name, Description = business.Description });
    }

    [HttpGet]
    [AllowAnonymous]
    public virtual async Task<ActionResult<List<BusinessDto>>> GetAll()
    {
        var businesses = await _businessRepository.GetAll()
            .Include(b => b.Pictures).ToListAsync();
        
        var businessDto = businesses.Select(b => new BusinessDto
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description,
            Pictures = b.Pictures.Select(p => new PictureDto
            {
                Id = p.Id,
                Url = p.VirtualPath
            }).ToList()
        }).ToList();

        return Ok(businessDto);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public virtual async Task<ActionResult<BusinessDto>> Get(int id)
    {
        var business = await _businessRepository.GetAsync(id);

        if (business is null)
            return NotFound();
        
        var pictures = await _pictureRepository.GetAll()
            .Where(p => p.BusinessId == id)
            .Select(p => new PictureDto
            {
                Id = p.Id,
                Url = p.VirtualPath
            })
            .ToListAsync();
        
        return Ok(new BusinessDto
        {
            Id = business.Id,
            Name = business.Name,
            Description = business.Description,
            Pictures = pictures
        });
    }
    
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, UpdateBusinessDto businessDto)
    {
        if (id != businessDto.Id)
            return BadRequest();

        var business = await _businessRepository.GetAsync(id);
        if (business is null || business.UserId != User.Identity?.Name)
            return NotFound();

        business.UpdateDetails(businessDto.Name, businessDto.Description);

        await _businessRepository.UpdateAsync(business);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var business = await _businessRepository.GetAsync(id);
        
        if (business is null || business.UserId != User.Identity?.Name)
            return NotFound();
        
        await _businessRepository.DeleteAsync(business);
        return NoContent();
    }
}
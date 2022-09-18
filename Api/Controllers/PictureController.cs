using Api.Dtos.Pictures;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/Business/{businessId}/[Controller]")]
public class PicturesController : ControllerBase
{
    private readonly IRepository<Picture> _pictureRepository;
    private readonly IRepository<Business> _businessRepository;
    private readonly IFileService _fileService;

    public PicturesController(IRepository<Picture> pictureRepository, IFileService fileService, IRepository<Business> businessRepository)
    {
        _pictureRepository = pictureRepository;
        _fileService = fileService;
        _businessRepository = businessRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public virtual async Task<ActionResult<List<PictureDto>>> GetAll(int businessId)
    {
        var pictures = _pictureRepository.GetAll()
            .Where(p => p.BusinessId == businessId);

        return await pictures.Select(p => new PictureDto 
        {
            Id = p.Id,
            Url = p.VirtualPath
        }).ToListAsync();
    }

    [HttpPost]
    public virtual async Task<ActionResult<PictureDto>> Post([FromForm] CreatePictureDto createPictureDto)
    {
        var business = await _businessRepository.GetAsync(createPictureDto.BusinessId);

        if (business is null || business.UserId != User.Identity?.Name) return BadRequest();

        var virtualPath = await _fileService.Upload(
            createPictureDto.Picture.OpenReadStream(),
            Path.GetExtension(createPictureDto.Picture.FileName),
            business.Name);

        var picture = new Picture(
            createPictureDto.BusinessId,
            createPictureDto.Picture.ContentType,
            virtualPath,
            createPictureDto.SeoFileName,
            createPictureDto.AltAttribute,
            createPictureDto.TitleAttribute);

        await _pictureRepository.InsertAsync(picture);

        return Ok(new PictureDto
        {
            Id = picture.Id,
            Url = picture.VirtualPath
        });
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult> EditPicture(int id, [FromForm] UpdatePictureDto updatePictureDto)
    {
        if (id != updatePictureDto.Id)
            return BadRequest();
        
        var picture = await _pictureRepository.GetAsync(id);

        if (picture is null)
            return NotFound();

        var business = await _businessRepository.GetAsync(picture.BusinessId);

        if (business is null || business.UserId != User.Identity?.Name) return BadRequest();
        
        picture.UpdateSeo(updatePictureDto.SeoFileName, updatePictureDto.AltAttribute, updatePictureDto.TitleAttribute);
        if (updatePictureDto.Picture != null)
        {
            _fileService.Delete(picture.VirtualPath);
            var virtualPath = await _fileService.Upload(updatePictureDto.Picture.OpenReadStream(), Path.GetExtension(updatePictureDto.Picture.FileName));

            picture.UpdatePicturePath(virtualPath);
        }

        await _pictureRepository.UpdateAsync(picture);

        return Ok(new PictureDto
        {
            Id = picture.Id,
            Url = picture.VirtualPath
        });
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var picture = await _pictureRepository.GetAsync(id);

        if (picture is null)
            return NotFound();
        
        var business = await _businessRepository.GetAsync(picture.BusinessId);

        if (business is null || business.UserId != User.Identity?.Name) return BadRequest();

        _fileService.Delete(picture.VirtualPath);
        await _pictureRepository.DeleteAsync(picture);

        return NoContent();
    }
}
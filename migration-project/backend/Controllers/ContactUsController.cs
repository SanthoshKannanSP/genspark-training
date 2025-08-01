using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs.ContactUs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactUsController : ControllerBase
{
    private readonly IContactUsService _contactUsService;
    public ContactUsController(IContactUsService contactUsService)
    {
        _contactUsService = contactUsService;
    }

    [HttpPost]
    public async Task<ActionResult> ValidateCaptchaAsync(ContactUsRequestDTO contactUsRequestDTO)
    {
        var responseDTO = await _contactUsService.ValidateCaptcha(contactUsRequestDTO);
        if (responseDTO.Success)
            return Ok(responseDTO);

        return BadRequest(responseDTO);
    }
}
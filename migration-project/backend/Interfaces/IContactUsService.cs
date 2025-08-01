using Backend.Models.DTOs.ContactUs;

namespace Backend.Interfaces;

public interface IContactUsService
{
    Task<ContactUsResponseDTO> ValidateCaptcha(ContactUsRequestDTO contactUsRequestDTO);
}
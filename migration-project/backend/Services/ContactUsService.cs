using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs.ContactUs;
using Newtonsoft.Json;

namespace Backend.Services;

public class ContactUsService : IContactUsService
{
    private readonly IRepository<int, ContactUs> _contactRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    public ContactUsService(IRepository<int, ContactUs> contactRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _contactRepository = contactRepository;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<ContactUsResponseDTO> ValidateCaptcha(ContactUsRequestDTO contactUsRequestDTO)
    {
        string secret = _configuration["Captcha:SecretKey"];
        var captcha = contactUsRequestDTO.RecaptchaResponse;

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetStringAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={captcha}");

        var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(response);
        System.Console.WriteLine(captchaResponse.Success);
        foreach (var code in captchaResponse.ErrorCodes)
        {
            System.Console.WriteLine(code);
        }
        if (!captchaResponse.Success)
        {
            if (captchaResponse.ErrorCodes?.Count > 0)
            {
                return new ContactUsResponseDTO()
                {
                    Success = false,
                    Message = GetCaptchaErrorMessage(captchaResponse.ErrorCodes[0])
                };
            }
            return new ContactUsResponseDTO()
            {
                Success = false,
                Message = "CAPTCHA validation failed."
            };
        }

        var contact = new ContactUs
        {
            Name = contactUsRequestDTO.CusName,
            Email = contactUsRequestDTO.CusEmail,
            Phone = contactUsRequestDTO.CusPhone,
            Content = contactUsRequestDTO.CusContent
        };

        await _contactRepository.AddAsync(contact);
        return new ContactUsResponseDTO()
        {
            Success = true,
            Message = "Your query has been submitted successfully. We will get back to you shortly."
        };
    }

    private string GetCaptchaErrorMessage(string errorCode)
    {
        return errorCode.ToLower() switch
        {
            "missing-input-secret" => "Missing secret parameter",
            "invalid-input-secret" => "The secret is invalid or malformed",
            "missing-input-response" => "Missing response parameter",
            "invalid-input-response" => "The response parameter is invalid or malformed",
            _ => "An unknown error occurred during CAPTCHA verification"
        };
    }
}
namespace Backend.Models.DTOs.ContactUs;

public class ContactUsRequestDTO
{
    public string CusName { get; set; }
    public string CusEmail { get; set; }
    public string CusPhone { get; set; }
    public string CusContent { get; set; }
    public string RecaptchaResponse { get; set; }
}
namespace assignment_1.Models
{
    public class EncryptModel
    {
        public string? Data { get; set; }
        public byte[]? EncryptedData { get; set; }
        public byte[]? HashKey { get; set; }
    }
}
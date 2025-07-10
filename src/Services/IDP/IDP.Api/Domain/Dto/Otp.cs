namespace IDP.Api.Domain.Dto
{
    public class Otp
    {
        public required string UserName { get; set; }
        public required int OtpCode { get; set; }
        public bool IsUse { get; set; }
    }
}

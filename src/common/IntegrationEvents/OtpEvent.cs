using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents
{
    public class OtpEvent
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public OtpEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
        public string? MobileNumber { get; set; }
        public string? OtpCode { get; set; }
    }
}

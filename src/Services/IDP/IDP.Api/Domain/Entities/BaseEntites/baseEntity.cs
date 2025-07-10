using System.ComponentModel.DataAnnotations;

namespace IDP.Api.Domain.Entities.BaseEntites
{
    public class baseEntity
    {
        public baseEntity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime datDate { get; set; }
    }
}

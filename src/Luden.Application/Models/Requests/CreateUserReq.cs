using Luden.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Luden.Application.Models.Requests
{
    public class CreateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        public UserStatus Status { get; set; }
    }
}
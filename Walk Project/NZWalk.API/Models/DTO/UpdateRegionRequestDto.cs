using System.ComponentModel.DataAnnotations;

namespace NZWalk.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Cod has to minimum of 3 Characters")]
        [MaxLength(3, ErrorMessage = "Cod has to maximum of 3 Characters")]
        public string code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to maximum of 100 Characters")]
        public string Name { get; set; }
        public string? RegioImageUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestLib.Infrastructure.Models.V1
{
    public class RequestTopicDto : IValidatableObject
    {
        [Required(ErrorMessage = "This field is required and a length between 10 and 150.")]
        [MinLength(10)]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public ICollection<RequestMessageDto> Messages { get; set; } = new List<RequestMessageDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Title == Text)
            {
                yield return new ValidationResult("Make sure that your Text is different from the Title", 
                    new[] { "RequestTopicDto" });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tasks.Models
{
    public class TaskModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nameは必須項目です。")]
        [StringLength(100, ErrorMessage = "Nameは100文字以内で入力してください。")]
        public string Name { get; set; } = string.Empty;

        public string? Schedule { get; set; }

        [Required(ErrorMessage = "Completeは必須項目です。")]
        public bool Complete { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Schedule))
            {
                var regex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
                if (!regex.IsMatch(Schedule))
                {
                    yield return new ValidationResult(
                        "Scheduleは'YYYY-MM-DD'形式で入力してください。",
                        new[] { nameof(Schedule) }
                    );
                }
                else if (!DateTime.TryParse(Schedule, out _))
                {
                    yield return new ValidationResult(
                        "Scheduleは有効な日付である必要があります。",
                        new[] { nameof(Schedule) }
                    );
                }
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.Validators
{
    public class NotAfterThisYearAttribute : ValidationAttribute
    {
        public NotAfterThisYearAttribute()
        {
            ErrorMessage = "L'année ne peut pas être supérieure à l'année en cours";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            int year = (int)value;
            return year <= DateTime.Now.Year && year >= 0;
        }
    }
}

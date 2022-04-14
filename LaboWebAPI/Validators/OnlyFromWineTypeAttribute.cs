using LaboADO.Models;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.Validators
{
    public class OnlyFromWineTypeAttribute : ValidationAttribute
    {
        public OnlyFromWineTypeAttribute()
        {
            ErrorMessage = "Le type de vin que vous souhaitez ajouter n'existe pas";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            string type = ((WineType)value).ToString();
            return type != "";
        }
    }
}

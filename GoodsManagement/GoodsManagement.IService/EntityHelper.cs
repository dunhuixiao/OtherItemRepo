using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    public static class EntityHelper
    {
        public static bool Validate(this object instance, bool throwIfNotValid = true)
        {
            List<ValidationResult> validationResults;
            var r = instance.Validate(out validationResults);
            if (throwIfNotValid && !r)
            {
                throw new Exception("数据无效");
            }
            return r;
        }
        public static bool Validate(this object instance, out List<ValidationResult> validationResults)
        {
            ValidationContext context = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            var r = Validator.TryValidateObject(instance, context, validationResults, true);
            return r;
        }
    }
}

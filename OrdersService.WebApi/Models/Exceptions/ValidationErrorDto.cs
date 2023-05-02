using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class ValidationErrorDto : ErrorDto
    {
        [JsonPropertyName("propertyName")]
        public string PropertyName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class ExceptionDto<TErrorDto>
        where TErrorDto : ErrorDto
    {
        public ICollection<TErrorDto> Errors { get; set; }

        public DateTime Date { get; set; }

        public int Code { get; set; }
    }
}

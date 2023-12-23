using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.DTOs
{
    public class UploadFileDto
    {
        public string Base64 { get; set; }
        public string UserId { get; set; }
        public string SecurityKey { get; set; }
    }
}

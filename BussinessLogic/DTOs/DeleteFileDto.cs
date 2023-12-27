using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.DTOs
{
    public class DeleteFileDto
    {
        public string FileName { get; set; }
        public string SecurityKey { get; set; }
        public string AdminKey { get; set; }
        public string UserId { get; set; }
    }
}

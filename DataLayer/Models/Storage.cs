﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Storage
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

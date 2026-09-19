using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Base
{
    public class Response
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Detail { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceId { get; set; }
    }
}

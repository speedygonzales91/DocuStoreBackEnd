using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class SystemLog
    {
        public long Id { get; set; }
        public int Level { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Time { get; set; }
    }
}

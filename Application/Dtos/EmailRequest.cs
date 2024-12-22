using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class EmailRequest
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public List<EmailContent> Content { get; set; }
    }
}

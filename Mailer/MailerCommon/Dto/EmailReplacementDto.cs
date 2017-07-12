using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailerCommon.Dto
{
    public class EmailReplacementDto
    {
        public long EmailReplacementId { get; set; }
        public string Token { get; set; }
        public string Value { get; set; }
    }
}

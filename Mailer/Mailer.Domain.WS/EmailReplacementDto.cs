﻿namespace Mailer.Domain.WS
{
    public class EmailReplacementDto
    {
        public long EmailReplacementId { get; set; }
        public string Token { get; set; }
        public string Value { get; set; }
    }
}

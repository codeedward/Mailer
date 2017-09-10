//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MailerDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailQueue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailQueue()
        {
            this.EmailReplacements = new HashSet<EmailReplacement>();
        }
    
        public long EmailQueueId { get; set; }
        public long EmailMessageId { get; set; }
        public byte EmailStatus { get; set; }
        public int EmailType { get; set; }
        public byte TriesLeft { get; set; }
        public Nullable<System.DateTime> LastTryDateUtc { get; set; }
        public Nullable<System.DateTime> LockedDateUtc { get; set; }
        public Nullable<System.DateTime> AvailableToSendFromUtc { get; set; }
        public Nullable<System.DateTime> SendDateUtc { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ToEmailAddress { get; set; }
        public string ToPerson { get; set; }
    
        public virtual EmailMessage EmailMessage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailReplacement> EmailReplacements { get; set; }
    }
}

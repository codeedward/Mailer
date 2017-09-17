//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mailer.DAL.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailMessage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailMessage()
        {
            this.EmailQueues = new HashSet<EmailQueue>();
        }
    
        public long EmailMessageId { get; set; }
        public string FromAddress { get; set; }
        public string FromPerson { get; set; }
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
    }
}
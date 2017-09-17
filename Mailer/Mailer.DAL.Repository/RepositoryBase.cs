using Mailer.DAL.Db;

namespace Mailer.DAL.Repository
{
    public class RepositoryBase
    {
        public MailerEntities MailerContext { get; set; }

        public RepositoryBase()
        {
            MailerContext = new MailerEntities();
        }
    }
}

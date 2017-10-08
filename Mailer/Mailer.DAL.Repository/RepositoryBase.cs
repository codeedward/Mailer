using Mailer.DAL.Db;

namespace Mailer.DAL.Repository
{
    public class RepositoryBase
    {
        public MailerEntities MailerContext => new MailerEntities();
    }
}

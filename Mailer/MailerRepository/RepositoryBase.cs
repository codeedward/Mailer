using MailerDb;

namespace MailerRepository
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

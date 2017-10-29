namespace Mailer.DAL.Db
{
    public class RepositoryBase
    {
        public MailerEntities MailerContext => new MailerEntities();
    }
}

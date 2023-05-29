namespace Vertem.News.Infra.Base
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CriadoEm { get; protected set; }
        public DateTime? AlteradoEm { get; protected set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.Now;
        }
    }
}

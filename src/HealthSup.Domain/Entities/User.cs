namespace HealthSup.Domain.Entities
{
    public class User: BaseEntity
    {
        public int Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool IsActive { get; private set; }
    }
}

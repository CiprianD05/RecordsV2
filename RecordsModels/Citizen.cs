namespace RecordsModels
{
    public class Citizen
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? SocialSecurityNumber { get; set; }

        public string? PassportNumber { get; set; }

    }
}
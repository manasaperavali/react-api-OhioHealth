namespace BackendAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public int YearOfJoining { get; set; }
    }
}

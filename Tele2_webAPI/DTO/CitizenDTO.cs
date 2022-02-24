namespace Tele2API.DTO
{
    public class CitizenDTO
    {
        public int Id_num { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; } = 0;
    }
}

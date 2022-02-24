namespace FootballManager.ViewModels
{
    public class AddPlayerFormModel
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Position { get; set; }
        public byte Speed { get; set; }
        public byte Endurance { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}

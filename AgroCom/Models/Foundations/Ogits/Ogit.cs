namespace AgroCom.Models.Foundations.Ogits
{
    public class Ogit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public OgitType OgitType { get; set; }
    }
}

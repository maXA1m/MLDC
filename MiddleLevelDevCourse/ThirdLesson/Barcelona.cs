namespace MiddleLevelDevCourse.ThirdLesson
{
    public class Barcelona
    {
        public Barcelona(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; set; }
        public int ZipCode { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Latitude}, {Longitude}";
        }
    }
}

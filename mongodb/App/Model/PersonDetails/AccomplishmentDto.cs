namespace FamilyTreeMongoApp.Model.PersonDetails
{
    public class AccomplishmentDto
    {
        public string Time { get; set; } = default!;

        public string Description { get; set; } = default!;
    
        public void SetTimeFromDateTime(DateTime time)
        {
            Time = time.ToString();
        }
    }
}

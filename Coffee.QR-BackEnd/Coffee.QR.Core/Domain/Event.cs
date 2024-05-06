using Coffee.QR.BuildingBlocks.Core.Domain;

namespace Coffee.QR.Core.Domain
{
    public class Event : Entity
    {
        public string Name { get; private set; }
        public DateTime DateTime { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public long UserId { get; private set; }
        public User Creator { get; private set; }

        // Parameterless constructor required by EF Core
/*        private Event()
        {
        }*/

        // Main constructor for business logic
        public Event(string name, DateTime dateTime, string description, string image, long userId)
        {
            Name = name;
            DateTime = dateTime;
            Description = description;
            Image = image;
            UserId = userId;
        }

/*        // Optional public method to update the event
        public void UpdateDetails(string name, DateTime dateTime, string description, string image)
        {
            Name = name;
            DateTime = dateTime;
            Description = description;
            Image = image;
        }*/
    }
}

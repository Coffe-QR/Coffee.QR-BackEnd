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
        public long LocalId {  get; private set; }
        public Local Local {  get; private set; }

        public Event(string name, DateTime dateTime, string description, string image, long userId, long localId)
        {
            Name = name;
            DateTime = dateTime;
            Description = description;
            Image = image;
            UserId = userId;
            LocalId = localId;
        }

    }
}

namespace RoomDecorate
{
    internal class Room
    {
        public Room(string name, SizeOfRoom sizeOfRoom)
        {
            Name = name;
            SizeOfRoom = sizeOfRoom;
        }

        public string Name { get; set; }
        public SizeOfRoom SizeOfRoom { get; set; }

        public List<Furniture> Furnitures { get; set; } = new List<Furniture>();
    }
}

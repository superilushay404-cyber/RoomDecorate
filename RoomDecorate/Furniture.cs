namespace RoomDecorate
{
    internal class Furniture
    {
        public Furniture(string name, float price, int xSize, int ySize)
        {
            Name = name;
            Price = price;
            XSize = xSize;
            YSize = ySize;
        }
        public string Name { get; set; }

        public float Price { get; set; }

        public int XSize { get; set; }

        public int YSize { get; set; }

        public int XPosition { get; set; }

        public int YPosition { get; set; }
    }
}

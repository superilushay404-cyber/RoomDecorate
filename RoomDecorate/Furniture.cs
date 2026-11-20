namespace RoomDecorate
{
    internal class Furniture
    {
        public Furniture(string name, float price, int xSize, int ySize, string color)
        {
            Name = name;
            Price = price;
            XSize = xSize;
            YSize = ySize;
            Color = color;
        }
        public string Name { get; set; }

        public float Price { get; set; }

        public int XSize { get; set; }

        public int YSize { get; set; }

        public string Color { get; set; }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public int Degree { get; set; } = 0;
    }
}

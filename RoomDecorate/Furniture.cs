namespace RoomDecorate
{
    internal class Furniture
    {
        public Furniture(string name, int xSize, int ySize, int zSize)
        {
            Name = name;
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;
        }
        public string Name { get; set; }

        public float Price { get; set; }

        public int XSize { get; set; }

        public int YSize { get; set; }

        public int ZSize { get; set; }

        public int X1 { get; set; }

        public int X2 { get; set; }

        public int Y1 { get; set; }

        public int Y2 { get; set; }

        public int Z1 { get; set; }

        public int Z2 { get; set; }
    }
}

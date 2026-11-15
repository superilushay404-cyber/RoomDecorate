namespace RoomDecorate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isProgramRunning = true;
            List<Room> rooms = new List<Room>();


            while (isProgramRunning)
            {
                PrintInfo();
                bool isParseSuccess = int.TryParse(Console.ReadLine(), out int userInput);

                if (userInput == 1)
                {
                    Console.WriteLine("Enter room name");
                    string roomName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(roomName))
                    {
                        Console.WriteLine("Enter room size (x1, x2, y1, y2, z1, z2) separated by commas");
                        string sizeInput = Console.ReadLine();
                        string[] sizeParts = sizeInput.Split(',');

                        if (sizeParts.Length == 6 &&
                            int.TryParse(sizeParts[0], out int x1) &&
                            int.TryParse(sizeParts[1], out int x2) &&
                            int.TryParse(sizeParts[2], out int y1) &&
                            int.TryParse(sizeParts[3], out int y2) &&
                            int.TryParse(sizeParts[4], out int z1) &&
                            int.TryParse(sizeParts[5], out int z2))
                        {
                            SizeOfRoom sizeOfRoom = new SizeOfRoom(x1, x2, y1, y2, z1, z2);
                            Room room = new Room(roomName, sizeOfRoom);
                            Console.WriteLine($"Room '{room.Name}' created with size: ({x1}-{x2}x, {y1}-{y2}y, {z1}-{z2}z)");
                        }
                        else
                        {
                            Console.WriteLine("Invalid size input. Please enter 6 integers separated by commas.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Room name cannot be empty.");
                    }
                }
            }
        }

        static void PrintInfo()
        {
            Console.WriteLine("Enter 1 to create new room");
        }

        static void SeedData (List<Furniture> furnitureList)
        {
            furnitureList.Add(new Furniture("Chair", 1, 3, 2));
            furnitureList.Add(new Furniture("Table", 3, 5, 1, 2, 1, 2));
            furnitureList.Add(new Furniture("Sofa", 6, 8, 1, 3, 1, 2));
            furnitureList.Add(new Furniture("Bed", 9, 12, 1, 4, 1, 2));
            furnitureList.Add(new Furniture("Closet", 4, 3, 3, 3, 1, 2));
        })
    }
}

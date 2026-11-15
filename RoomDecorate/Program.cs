namespace RoomDecorate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isProgramRunning = true;
            List<Room> rooms = new List<Room>();
            List<Furniture> furnitureList = new List<Furniture>();
            SeedData(furnitureList);

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
                        Console.WriteLine("Enter room size (x and y) separated by commas");
                        bool isXSizeParseSuccessSize = int.TryParse(Console.ReadLine(), out int roomSizeX);

                        if (isXSizeParseSuccessSize)
                        {
                            bool isYSizeParseSuccessSize = int.TryParse(Console.ReadLine(), out int roomSizeY);
                            if (isYSizeParseSuccessSize)
                            {
                                SizeOfRoom sizeOfRoom = new SizeOfRoom(roomSizeX, roomSizeY);
                                Room newRoom = new Room(roomName, sizeOfRoom);
                                rooms.Add(newRoom);
                                Console.WriteLine($"Room '{roomName}' created with size {roomSizeX}x{roomSizeY}.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for room size Y.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for room size X.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Room name cannot be empty.");
                    }
                }
                else if (userInput == 2)
                {
                    Console.WriteLine("Catalog of Furniture:");

                    foreach (var furniture in furnitureList)
                    {
                        Console.WriteLine($"Name: {furniture.Name}, Size: {furniture.XSize}x{furniture.YSize}");
                    }
                }
            }
        }

        static void PrintInfo()
        {
            Console.WriteLine("Enter 1 to create new room");
            Console.WriteLine("Enter 2 to see catalog of furniture");
            Console.WriteLine("Enter 3 to place a furniture into the room");
        }

        static void SeedData (List<Furniture> furnitureList)
        {
            furnitureList.Add(new Furniture("Chair", 50, 1, 5));
            furnitureList.Add(new Furniture("Table", 150, 3, 5));
            furnitureList.Add(new Furniture("Sofa", 500, 6, 3));
            furnitureList.Add(new Furniture("Bed", 575, 9, 3));
            furnitureList.Add(new Furniture("Closet", 350, 4, 3));
        }
    }
}

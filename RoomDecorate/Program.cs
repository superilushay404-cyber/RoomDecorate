using System.ComponentModel.Design;

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
                        Console.WriteLine("Enter room size x");
                        bool isXSizeParseSuccessSize = int.TryParse(Console.ReadLine(), out int roomSizeX);

                        if (isXSizeParseSuccessSize)
                        {
                            Console.WriteLine("Enter room size y");
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
                        Console.WriteLine($"Name: {furniture.Name}, Size: {furniture.XSize}x{furniture.YSize} Price: {furniture.Price}");
                    }
                }
                else if (userInput == 3)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(furnitureList, nameOfFurniture);
                        Furniture dublicate = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null && dublicate == null)
                        {
                            Console.WriteLine("Enter x");
                            bool isXParseSuccess = int.TryParse(Console.ReadLine(), out int x);

                            if (isXParseSuccess == true && x <= rooms[0].SizeOfRoom.X && x > 0)
                            {
                                Console.WriteLine("Enter y");
                                bool isYParseSuccess = int.TryParse(Console.ReadLine(), out int y);

                                if (isYParseSuccess == true && y <= rooms[0].SizeOfRoom.Y && y > 0)
                                {
                                    Furniture roomWithSamePosision = GetFurnitureByXOrY(rooms[0].Furnitures, x, y);

                                    if (roomWithSamePosision != null)
                                    {
                                        Console.WriteLine("This place is taken. Try to change it");
                                    }
                                    else
                                    {
                                        Furniture newFurniture = findedFurniture;
                                        rooms[0].Furnitures.Add(newFurniture);
                                        newFurniture.XPosition = x;
                                        newFurniture.YPosition = y;
                                        Console.WriteLine("Success");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input of y (or other furniture is already placed there)");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong input of x (or other furniture is already placed there)");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Furniture not found or already placed in the room.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rooms available. Please create a room first.");
                    }
                }
                else if (userInput == 4)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture to rotate");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null)
                        {
                            if (findedFurniture.Degree != 360)
                            {
                                findedFurniture.Degree = findedFurniture.Degree + 90;
                            }
                            else
                            {
                                findedFurniture.Degree = 0;
                            }

                            Console.WriteLine($"Success! Current degree is {findedFurniture.Degree}");
                        }
                        else
                        {
                            Console.WriteLine("Furniture not found in the room.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rooms available. Please create a room first.");
                    }
                }
                else if (userInput == 5)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null )
                        {
                            Console.WriteLine("Enter new x");
                            bool isXParseSuccess = int.TryParse(Console.ReadLine(), out int x);

                            if (isXParseSuccess == true)
                            {
                                Console.WriteLine("Enter new y");
                                bool isYParseSuccess = int.TryParse(Console.ReadLine(), out int y);

                                if (isYParseSuccess == true)
                                {
                                    Furniture furnitureWithSamePosition = GetFurnitureByXOrY(rooms[0].Furnitures, x, y);

                                    if (furnitureWithSamePosition == null )
                                    {
                                        findedFurniture.XPosition = x;
                                        findedFurniture.YPosition = y;
                                        Console.WriteLine("Success");
                                    }
                                    else
                                    {
                                        Console.WriteLine("This place is not avaivable");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Input is not int");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input is not int");
                            }
                        }
                        else
                        {
                            Console.WriteLine("This furniture is not placed in this room");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rooms available. Please create a room first.");
                    }
                }
            }
        }

        static void PrintInfo()
        {
            Console.WriteLine("Enter 1 to create new room");
            Console.WriteLine("Enter 2 to see catalog of furniture");
            Console.WriteLine("Enter 3 to place a furniture into the room");
            Console.WriteLine("Enter 4 to rotate furniture by 90 degrees");
            Console.WriteLine("Enter 5 to move furniture");
        }

        static void SeedData(List<Furniture> furnitureList)
        {
            furnitureList.Add(new Furniture("Chair", 50, 1, 5, "red"));
            furnitureList.Add(new Furniture("Table", 150, 3, 5, "blue"));
            furnitureList.Add(new Furniture("Sofa", 500, 6, 3, "brown"));
            furnitureList.Add(new Furniture("Bed", 575, 9, 3, "white"));
            furnitureList.Add(new Furniture("Closet", 350, 4, 3, "pink"));
        }

        static Furniture GetFurnitureByName(List<Furniture> furnitureList, string nameOfFurniture)
        {
            foreach (var furniture in furnitureList)
            {
                if (furniture.Name == nameOfFurniture)
                {
                    return furniture;
                }
            }
            return null;
        }

        static Furniture GetFurnitureByXOrY(List<Furniture> furnitures, int x, int y)
        {
            foreach (var furniture in furnitures)
            {
                if (furniture.XPosition == x || furniture.YPosition == y)
                {
                    return furniture;
                }
            }
            return null;
        }
    }
}

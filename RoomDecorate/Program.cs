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
                                    int x1 = x - findedFurniture.XSize / 2;
                                    int x2 = x + findedFurniture.XSize / 2;
                                    int y1 = y - findedFurniture.YSize / 2;
                                    int y2 = y + findedFurniture.YSize / 2;
                                    Furniture roomWithSamePosision = GetFurnitureByXOrY(rooms[0].Furnitures, x1, y1, x2, y2, -1);

                                    if (roomWithSamePosision != null)
                                    {
                                        Console.WriteLine("This place is taken. Try to change it");
                                    }
                                    else
                                    {
                                        XAndYPosiotions xAndYPositions = new XAndYPosiotions(x1, y1, x2, y2);

                                        Furniture newFurniture = new Furniture(
                                        findedFurniture.Name,
                                        findedFurniture.Price,
                                        findedFurniture.XSize,
                                        findedFurniture.YSize,
                                        findedFurniture.Color
                                        );
                                        rooms[0].Furnitures.Add(newFurniture);
                                        newFurniture.XAndYPosiotions = xAndYPositions;
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
                                    int x1 = x - findedFurniture.XSize / 2;
                                    int x2 = x + findedFurniture.XSize / 2;
                                    int y1 = y - findedFurniture.YSize / 2;
                                    int y2 = y + findedFurniture.YSize / 2;
                                    int index = rooms[0].Furnitures.IndexOf(findedFurniture);
                                    Furniture furnitureTakenPlace = GetFurnitureByXOrY(rooms[0].Furnitures, x1, y1, x2, y2, index);

                                    if (furnitureTakenPlace == null )
                                    {
                                        findedFurniture.XAndYPosiotions = new XAndYPosiotions(x1, y1, x2, y2);
                                        findedFurniture.XPosition = x;
                                        findedFurniture.YPosition = y;
                                        Console.WriteLine("Success");
                                    }
                                    else
                                    {
                                        Console.WriteLine("This place is already taken");
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
                else if (userInput == 6)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurnitureToRemove = Console.ReadLine();
                        Furniture furnitureToRemove = GetFurnitureByName(rooms[0].Furnitures, nameOfFurnitureToRemove);

                        if (furnitureToRemove != null)
                        {
                            rooms[0].Furnitures.Remove(furnitureToRemove);
                            Console.WriteLine("Success");
                        }
                        else
                        {
                            Console.WriteLine("Couldnt find furniture");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 7)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null)
                        {
                            Console.WriteLine("Input new color of furniture");
                            string newColorOfFurniture = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newColorOfFurniture))
                            {
                                findedFurniture.Color = newColorOfFurniture;
                                Console.WriteLine("Success");
                            }
                            else
                            {
                                Console.WriteLine("Wrong color input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Furniture is not exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 8)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null)
                        {
                            Console.WriteLine("Input new name of furniture");
                            string newNameOfFurniture = Console.ReadLine();

                            if (!string.IsNullOrEmpty(newNameOfFurniture))
                            {
                                findedFurniture.Name = newNameOfFurniture;
                                Console.WriteLine("Success");
                            }
                            else
                            {
                                Console.WriteLine("Wrong color input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Furniture is not exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 9)
                {
                    if (rooms.Count > 0)
                    {
                        Console.WriteLine("Enter name of furniture");
                        string nameOfFurniture = Console.ReadLine();
                        Furniture findedFurniture = GetFurnitureByName(rooms[0].Furnitures, nameOfFurniture);

                        if (findedFurniture != null)
                        {
                            Console.WriteLine("Enter new x size");
                            bool isXParseSuccess = int.TryParse(Console.ReadLine(), out int x);

                            if (isXParseSuccess == true)
                            {
                                Console.WriteLine("Enter new y size");
                                bool isYParseSuccess = int.TryParse(Console.ReadLine(), out int y);

                                if (isYParseSuccess == true)
                                {
                                    int x1 = findedFurniture.XPosition - x / 2;
                                    int x2 = findedFurniture.XPosition + x / 2;
                                    int y1 = findedFurniture.YPosition - y / 2;
                                    int y2 = findedFurniture.YPosition + y / 2;
                                    int index = rooms[0].Furnitures.IndexOf(findedFurniture);
                                    Furniture furnitureTakenPlace = GetFurnitureByXOrY(rooms[0].Furnitures, x1, y1, x2, y2, index);

                                    if (furnitureTakenPlace != null)
                                    {
                                        Console.WriteLine("Place is taken");
                                    }
                                    else
                                    {
                                        XAndYPosiotions xAndYPositions = new XAndYPosiotions(x1, y1, x2, y2);
                                        findedFurniture.XAndYPosiotions = xAndYPositions;
                                        findedFurniture.XSize = x;
                                        findedFurniture.YSize = y;
                                        Console.WriteLine("Success");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Couldnt find this room");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 10)
                {
                    if (rooms.Count > 0)
                    {
                        float totalPrice = 0;

                        foreach (var furniture in rooms[0].Furnitures)
                        {
                            totalPrice += furniture.Price;
                        }

                        if (totalPrice == 0)
                        {
                            Console.WriteLine("No funiture added yet");
                        }
                        else
                        {
                            Console.WriteLine($"Total price is: {totalPrice}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 11)
                {
                    if (rooms.Count > 0 || rooms[0].Furnitures.Count > 0)
                    {
                        foreach (var furniture in rooms[0].Furnitures)
                        {
                            Console.WriteLine($"{furniture.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms or added furniture yet");
                    }
                }
                else if (userInput == 12)
                {
                    if (rooms.Count > 0)
                    {
                        float totalPlace = rooms[0].SizeOfRoom.X * rooms[0].SizeOfRoom.Y;
                        float totalTakenPlace = 0;

                        foreach (var furniture in rooms[0].Furnitures)
                        {
                            totalTakenPlace += furniture.XSize * furniture.YSize;
                        }

                        if (totalTakenPlace == 0)
                        {
                            Console.WriteLine("There is no furniture yet");
                        }
                        else
                        {
                            Console.WriteLine($"Percent of taken place: {(totalTakenPlace / totalPlace) * (float)100}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no rooms yet");
                    }
                }
                else if (userInput == 13)
                {
                    if (rooms.Count > 1)
                    {
                        Console.WriteLine("Enter name of room to change");
                        string nameOfRoom = Console.ReadLine();
                        Room findedRoom = GetRoomByName(rooms, nameOfRoom);

                        if (findedRoom != null)
                        {
                            Room copyRoom = new Room(findedRoom.Name, findedRoom.SizeOfRoom, findedRoom.Furnitures);
                            findedRoom = new Room(rooms[0].Name, rooms[0].SizeOfRoom, rooms[0].Furnitures);
                            rooms[0] = new Room(copyRoom.Name, copyRoom.SizeOfRoom, copyRoom.Furnitures);

                            Console.WriteLine("Success");
                        }
                        else
                        {
                            Console.WriteLine("This room is not exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Atleast 3 rooms needed");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input");
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
            Console.WriteLine("Enter 6 to remove furniture from room");
            Console.WriteLine("Enter 7 to change color of furniture");
            Console.WriteLine("Enter 8 to change name of furniture");
            Console.WriteLine("Enter 9 to change size of object");
            Console.WriteLine("Enter 10 to see total price of all furniture in the room");
            Console.WriteLine("Enter 11 to see list of all furniture in the room");
            Console.WriteLine("Enter 12 to see info about free and taken place in the room");
            Console.WriteLine("Enter 13 to change this room");
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

        static Furniture GetFurnitureByXOrY(List<Furniture> furnitures, int x1, int y1, int x2, int y2, int index)
        {
            foreach (var furniture in furnitures)
            {
                int indexOfFurniture = furnitures.IndexOf(furniture);

                if (furniture.XAndYPosiotions != null && indexOfFurniture != index)
                {
                    if (!(x2 <= furniture.XAndYPosiotions.X1 || x1 >= furniture.XAndYPosiotions.X2 || y2 <= furniture.XAndYPosiotions.Y1 || y1 >= furniture.XAndYPosiotions.Y2))
                    {
                        return furniture;
                    }
                }
            }

            return null;
        }

        static Room GetRoomByName(List<Room> rooms,  string nameOfRoom)
        {
            foreach (var room in rooms)
            {
                if (room.Name ==  nameOfRoom)
                {
                    return room;
                }
            }

            return null;
        }
    }
}

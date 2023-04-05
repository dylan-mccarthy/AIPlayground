public static class RoomGenerator
{
    public static List<Room> GenerateRooms(int numberOfRooms)
    {
        Random rand = new Random();
        Graph graph = RandomGraphGenerator.GenerateRandomGraph(numberOfRooms);
        List<Room> rooms = new List<Room>();

        int range = (int)Math.Sqrt(numberOfRooms);

        for(int u = 0; u < numberOfRooms; u++)
        {
            Room room = new Room(u, rand.Next(0,range), rand.Next(0, range), new List<KeyValuePair<string, int>>(), "", new List<string>(), false);
            rooms.Add(room);
        }

        for(int u = 0 ; u < numberOfRooms; u++)
        {
            Room room = rooms[u];
            if(graph.HasEdge(u, u + 1) && (u + 1) % range != 0 && u + 1 < numberOfRooms){
                room.Exits.Add(new KeyValuePair<string, int>("east", u+1));
                rooms[u + 1].Exits.Add(new KeyValuePair<string, int>("west", u));
            }

            if(graph.HasEdge(u, u + range) && u + range < numberOfRooms){
                room.Exits.Add(new KeyValuePair<string, int>("south", u+range));
                rooms[u + range].Exits.Add(new KeyValuePair<string, int>("north", u));
            }

            //Create random description for room
            string[] descriptions = new string[]{"You are in a dark room", "You are in a room with a large window", "You are in a room with a large mirror", "You are in a room with a large painting", "You are in a room with a large table", "You are in a room with a large chair", "You are in a room with a large bed", "You are in a room with a large wardrobe", "You are in a room with a large desk", "You are in a room with a large bookshelf", "You are in a room with a large fireplace", "You are in a room with a large sofa", "You are in a room with a large rug", "You are in a room with a large lamp", "You are in a room with a large cupboard", "You are in a room with a large chest", "You are in a room with a large wardrobe", "You are in a room with a large sink", "You are in a room with a large toilet", "You are in a room with a large bath"};
            room.Description = descriptions[rand.Next(0, descriptions.Length)];
        }

        // Add Treasure to a random room
        int treasureRoomId = rand.Next(0, numberOfRooms);
        Room treasureRoom = rooms.FirstOrDefault(r => r.Id == treasureRoomId) ?? throw new Exception("Could not find Treasure Room");
        if(treasureRoom != null)
        {
            treasureRoom.HasTreasure = true;
            treasureRoom.Description += " You see a treasure chest";
        }

        // Add Key to a random room
        int keyRoomId = rand.Next(0, numberOfRooms);
        Room keyRoom = rooms.FirstOrDefault(r => r.Id == keyRoomId) ?? throw new Exception("Could not find Key Room");
        if(keyRoom != null)
        {
            keyRoom.Items.Add("key");
            keyRoom.Description += " You see a key";
        }

        return rooms;
    }
}
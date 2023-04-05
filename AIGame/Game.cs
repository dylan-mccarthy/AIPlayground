using System.Text;

public class Game{

    public bool IsPlaying {get; set;}
    public bool FoundTreasure {get; set;}

    public bool FoundKey {get; set;}
    
    private List<Room> Rooms {get; set;}

    private Room currentRoom;

    private List<Room> visitedRooms;

    private List<string> Inventory;

    private bool newGame = true;

    public Game(){
        IsPlaying = true;
        FoundTreasure = false;
        FoundKey = false;
        Rooms = new List<Room>();
        visitedRooms = new List<Room>();
        Inventory = new List<string>();
        currentRoom = CreateRooms();
    }

    private Room CreateRooms()
    {
        // Create random number of rooms
        var numRooms = new Random().Next(5, 10);
        Rooms = RoomGenerator.GenerateRooms(numRooms);

        // Select first room randomly
        var firstRoom = Rooms[new Random().Next(0, Rooms.Count)];
        return firstRoom;
    }

    public void Start(){
        // Start Game Loop

        while(IsPlaying){
            // Get Input from player
            var input = getPlayerInput();
            // Process Input
            var output = processInput(input);
            // Update Game State
            updateGameState();
            // Send Game State back to player
            updatePlayer(output);
        }
    }

    private void updatePlayer(string output)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(currentRoom.Description + "\n");
        sb.Append("Exits: ");
        foreach(var exit in currentRoom.Exits){
            sb.Append(exit.Key + " ");
        }
        sb.Append("\n");
        sb.Append("Items: ");
        foreach(var item in currentRoom.Items){
            sb.Append(item + " ");
        }
        sb.Append("\n");
        if(output.Length < 0)
            sb.Append("[ " + output + " ]");
        Console.WriteLine(sb.ToString());
    }

    private void updateGameState()
    {
        //Check if treasure is found and set game state
        if(FoundTreasure){
            IsPlaying = false;
            Console.WriteLine("You found the treasure");
        }
    }

    private string processInput(string input)
    {
        // List of commands look, go, take, use
        var commands = input.Split(" ");

        if(commands[0] == "look"){
            return currentRoom.Description;
        }

        if(commands[0] == "go"){
            // Check if the room has an exit in the direction the player wants to go
            var exit = currentRoom.Exits.FirstOrDefault(e => e.Key == commands[1]);
            if(exit.Value != 0){
                // Get the room the player wants to go to
                var room = Rooms.FirstOrDefault(r => r.Id == exit.Value);
                if(room != null)
                {
                    // Set the current room to the room the player wants to go to
                    currentRoom = room;
                    // Add the room to the list of visited rooms
                    visitedRooms.Add(room);
                    // Return the description of the room
                    return room.Description;
                }
                else{
                    return "Error can't find the room";
                }
            }
            else{
                return "You can't go that way";
            }
        }

        if(commands[0] == "take"){
            // Check if the room has the item the player wants to take
            var item = currentRoom.Items.FirstOrDefault(i => i == commands[1]);
            if(item != null){
                // Remove the item from the room
                currentRoom.Items.Remove(item);
                // Add the item to the player's inventory
                Inventory.Add(item);
                // Return a message to the player
                return $"You took the {item}";
            }
            else{
                return "You can't take that";
            }
        }

        if(commands[0] == "use"){
            // Check if the player has the item they want to use
            var item = Inventory.FirstOrDefault(i => i == commands[1]);
            if(item != null){
                // Check if the item is a key
                if(item == "key"){
                    // Check if the player is in the room with the treasure
                    if(currentRoom.HasTreasure){
                        // Set the game state to found treasure
                        FoundTreasure = true;
                        // Return a message to the player
                        return "You found the treasure";
                    }
                    else{
                        return "You can't use that here";
                    }
                }
                else{
                    return "You can't use that";
                }
            }
            else{
                return "You don't have that";
            }
        }

        return "I don't understand";
        
    }

    private string getPlayerInput()
    {
        if(newGame)
        {
            Console.WriteLine("Welcome to the game");
            updatePlayer("");
            newGame = false;
        }
        Console.WriteLine("What would you like to do?");
        Console.Write("> ");
        var playerinput = Console.ReadLine();

        return playerinput ?? "";
    }
}
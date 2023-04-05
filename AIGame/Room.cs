public class Room{

    public int Id {get; set;}
    public int x {get; set;}
    public int y {get; set;}
    // Exit has a direction and a room id
    public List<KeyValuePair<string, int>> Exits {get; set;}
    public string Description {get; set;}
    public List<string> Items {get; set;}

    public bool HasTreasure {get; set;}

    public Room(int id,int x, int y, List<KeyValuePair<string, int>> exits, string description, List<string> items, bool hasTreasure){
        Id = id;
        this.x = x;
        this.y = y;
        Exits = exits;
        Description = description;
        Items = items;
        HasTreasure = hasTreasure;
    }
}
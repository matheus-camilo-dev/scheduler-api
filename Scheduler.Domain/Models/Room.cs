namespace Scheduler.Domain.Models;

public class Room
{
    public int Id { get;  set; }
    public string Name { get;  set; }
    public RoomStatus Status { get;  set; }

    public Room() { } // EF Core

    public Room(int id, string name)
    {
        Id = id;
        Name = name;
        Status = RoomStatus.Free;
    }

    public Room(int id, string name, RoomStatus status) 
    {
        Id = id;
        Name = name;
        Status = status;
    }

    public void Use()
    {
        if (Status != RoomStatus.Free)
        {
            throw new InvalidOperationException("Just is Possible to use Free Rooms!");
        }

        Status = RoomStatus.InUse;
    }

    public void Free()
    {
        if (Status == RoomStatus.Free)
        {
            throw new InvalidOperationException("Already is free!");
        }
        Status = RoomStatus.Free;
    }
}

using System.Collections.Generic;

public class RoomReport
{
    public string RoomNumber { get; set; }
    public int TotalBookings { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<string> CustomerNames { get; set; }
}

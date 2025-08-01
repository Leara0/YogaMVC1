namespace YogaMVC1.Models;

public class Category
{
    public int Category_Id { get; set; }
    public string? Category_Name { get; set; }
    public string? Category_Description { get; set; }
    public List<int> PosesInThisCategory { get; set; } 
}
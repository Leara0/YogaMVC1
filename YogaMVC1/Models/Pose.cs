using Microsoft.AspNetCore.Mvc.Rendering;

namespace YogaMVC1.Models;

public class Pose
{
    public int Pose_Id { get; set; }
    public string English_Name { get; set; }
    public string Sanskrit_Name_Adapted { get; set; }
    public string Sanskrit_Name { get; set; }
    public string Translation_Name { get; set; }
    public string Pose_Description { get; set; }
    public string Pose_Benefits { get; set; }
    public int Difficulty_Id { get; set; }
    public string Url_Svg { get; set; }
    public string Url_Png { get; set; }
    public string Url_Svg_Alt { get; set; }
    
    
    public string Difficulty_Level { get; set; }
    public List<SelectListItem> DifficultyOptions { get; set; }
    
    
    public List<int> CategoryIdPerPose { get; set; }
    public List<SelectListItem> CategoryOptions { get; set; }
    public string Category { get; set; }
    public List<(string CatName, int? CatId)> CategoryList { get; set; }
    
    
    public IEnumerable<Category>? Categories { get; set; }
    public IEnumerable<Difficulty>? Difficulties { get; set; }
    
   
    

}
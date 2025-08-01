using Microsoft.AspNetCore.Mvc.Rendering;

namespace YogaMVC1.Models;

public class InsertPoseModel
{
    public int Pose_Id { get; set; }
    public string English_Name { get; set; }
    public string Sanskrit_Name_Adapted { get; set; }
    public string Sanskrit_Name { get; set; }
    public string Translation_Name { get; set; }
    public string Pose_Description { get; set; }
    public string Pose_Benefits { get; set; }
    public string Url_Svg { get; set; }
    public string Url_Png { get; set; }
    public string Url_Svg_Alt { get; set; }
    
    public List<SelectListItem> DifficultyOptions { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
}
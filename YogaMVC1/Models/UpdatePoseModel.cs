using Microsoft.AspNetCore.Mvc.Rendering;

namespace YogaMVC1.Models;

public class UpdatePoseModel
{
    public int PoseId { get; set; }
    public string Name { get; set; }
    public string PoseDesc { get; set; }
    public string PoseBenefits { get; set; }
    
    
    public int SelectedDifficultyId { get; set; }
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    
    public List<SelectListItem> DifficultyOptions { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
    
}
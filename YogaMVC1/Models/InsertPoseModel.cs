using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YogaMVC1.Models;

public class InsertPoseModel
{
    
    public int PoseId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
   public string SanskritName { get; set; }
    public string TranslationName { get; set; }
    public string PoseDescription { get; set; }
    public string PoseBenefits { get; set; }
    public int DifficultyId { get; set; }
    public string UrlSvg { get; set; }
    public string UrlSvgAlt { get; set; }
    
    public int SelectedDifficultyId { get; set; }
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    
    public List<SelectListItem> DifficultyOptions { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
}
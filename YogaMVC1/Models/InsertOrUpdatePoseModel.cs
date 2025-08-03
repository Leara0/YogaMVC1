using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YogaMVC1.Models;

public class InsertOrUpdatePoseModel
{
    public int PoseId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int? DifficultyId { get; set; }
    
    [RegularExpression(@"^$|.*\.svg$", ErrorMessage = "Please enter a valid SVG URL or leave blank")]
    [Display(Name = "SVG Image URL")]
    public string? UrlSvg { get; set; }
    
    [RegularExpression(@"^(|.*svg.*)$", ErrorMessage = "Please enter a valid SVG URL or leave blank")]
    [Display(Name = "SVG URL")]
    public string? UrlSvgAlt { get; set; }
    
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    
    public List<SelectListItem> DifficultyOptions { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
}
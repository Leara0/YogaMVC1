using Microsoft.AspNetCore.Mvc.Rendering;
using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public class InsertOrUpdateFactory:IInsertOrUpdateFactory
{
    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _catRepo;
    private readonly IDifficultyRepository _diffRepo;

    public InsertOrUpdateFactory(IPoseRepository poseRepo, ICategoryRepository catRepo, IDifficultyRepository diffRepo)
    {
        _poseRepo = poseRepo;
        _catRepo = catRepo;
        _diffRepo = diffRepo;
    }
    
    public InsertOrUpdatePoseModel BuildUpdateModel(int poseId)
    {
        var pose = _poseRepo.GetPoseById(poseId);
        var selectedCategories = _poseRepo.GetCategoryIdByPoseId(poseId);
        var difficulties = _diffRepo.GetAllDifficulties();
        var categories = _catRepo.GetAllCategories();

        return new InsertOrUpdatePoseModel()
        {
            PoseId = pose.Pose_Id,
            Name = pose.English_Name,
            SanskritName = pose.Sanskrit_Name,
            TranslationName = pose.Translation_Name,
            PoseDescription = pose.Pose_Description,
            PoseBenefits = pose.Pose_Benefits,
            UrlSvg = pose.Url_Svg,
            UrlSvgAlt = pose.Url_Svg_Alt,
            DifficultyId = pose.Difficulty_Id,
            SelectedCategoryIds = selectedCategories,

            DifficultyOptions = difficulties.Select(d => new SelectListItem
            {
                Value = d.Difficulty_Id.ToString(),
                Text = d.Difficulty_Level,
                Selected = d.Difficulty_Id == pose.Difficulty_Id
            }).ToList(),

            CategoryOptions = categories.Select(c => new SelectListItem
            {
                Value = c.Category_Id.ToString(),
                Text = c.Category_Name,
                Selected = selectedCategories.Contains(c.Category_Id)
            }).ToList()


        };
    }

    public InsertOrUpdatePoseModel BuildInsertModel()
    {
        var difficulties = _diffRepo.GetAllDifficulties();
        var categories = _catRepo.GetAllCategories();
        return new InsertOrUpdatePoseModel()
        {
            DifficultyOptions = difficulties.Select(d => new SelectListItem
            {
                Value = d.Difficulty_Id.ToString(),
                Text = d.Difficulty_Level,
                Selected = false
            }).ToList(),
            
            CategoryOptions = categories.Select(c=>new SelectListItem
            {
                Value = c.Category_Id.ToString(),
                Text = c.Category_Name,
                Selected = false
            }).ToList()
        };
    }
    
    
}
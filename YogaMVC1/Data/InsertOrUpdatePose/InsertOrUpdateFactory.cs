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
    
    public UpdatePoseModel BuildUpdateModel(int poseId)
    {
        var pose = _poseRepo.GetPoseById(poseId);
        var selectedCategories = _poseRepo.GetCategoryIdByPoseId(poseId);
        var difficulties = _diffRepo.GetAllDifficulties();
        var categories = _catRepo.GetAllCategories();

        return new UpdatePoseModel()
        {
            PoseId = pose.Pose_Id,
            Name = pose.English_Name,
            PoseDesc = pose.Pose_Description,
            PoseBenefits = pose.Pose_Benefits,
            SelectedDifficultyId = pose.Difficulty_Id,
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

    public InsertPoseModel BuildInsertModel()
    {
        var difficulties = _diffRepo.GetAllDifficulties();
        var categories = _catRepo.GetAllCategories();
        return new InsertPoseModel()
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
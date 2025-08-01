using Microsoft.AspNetCore.Mvc.Rendering;
using YogaMVC1.Models;

namespace YogaMVC1.Data.UpdateFactory;

public class PoseUpdateFactory:IPoseUpdateFactory
{
    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _catRepo;
    private readonly IDifficultyRepository _diffRepo;

    public PoseUpdateFactory(IPoseRepository poseRepo, ICategoryRepository catRepo, IDifficultyRepository diffRepo)
    {
        _poseRepo = poseRepo;
        _catRepo = catRepo;
        _diffRepo = diffRepo;
    }
    
    public UpdatePoseModel BuildModel(int poseId)
    {
        var pose = _poseRepo.GetPoseById(poseId);
        var selectedCategories = _poseRepo.GetCategoryIdByPoseId(poseId);
        var currentDiffId = _poseRepo.GetDifficultyIdByPoseId(poseId);
        var difficulties = _diffRepo.GetAllDifficulties();
        var categories = _catRepo.GetAllCategories();

        return new UpdatePoseModel()
        {
            PoseId = pose.Pose_Id,
            Name = pose.English_Name,
            PoseDesc = pose.Pose_Description,
            PoseBenefits = pose.Pose_Benefits,
            SelectedDifficultyId = currentDiffId,
            SelectedCategoryIds = selectedCategories,

            DifficultyOptions = difficulties.Select(d => new SelectListItem
            {
                Value = d.Difficulty_Id.ToString(),
                Text = d.Difficulty_Level,
                Selected = d.Difficulty_Id == currentDiffId
            }).ToList(),

            CategoryOptions = categories.Select(c => new SelectListItem
            {
                Value = c.Category_Id.ToString(),
                Text = c.Category_Name,
                Selected = selectedCategories.Contains(c.Category_Id)
            }).ToList()


        };
    }
}
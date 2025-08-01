using YogaMVC1.Models;

namespace YogaMVC1.Data;

public interface IPoseRepository
{
    IEnumerable<Pose> GetAllPoses();
    Pose GetPoseById(int id);
    string GetDifficultyLevelByPoseId(int id);
    int GetDifficultyIdByPoseId(int id);
    List<int> GetCategoryIdByPoseId(int id);
    List<Pose> GetAllPosesInThisCategory(List<int> poseIds);
    
    //add in return poses by difficulty?
    Pose AssignCatAndDiffToPose(int id);
    void UpdatePose(Pose pose);
    
}
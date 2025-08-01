using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public interface IInsertOrUpdateFactory
{
    UpdatePoseModel BuildUpdateModel(int poseId);
    
    InsertPoseModel BuildInsertModel();
}
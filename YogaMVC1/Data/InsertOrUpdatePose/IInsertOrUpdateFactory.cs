using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public interface IInsertOrUpdateFactory
{
    InsertOrUpdatePoseModel BuildUpdateModel(int poseId);
    
    InsertOrUpdatePoseModel BuildInsertModel();
}
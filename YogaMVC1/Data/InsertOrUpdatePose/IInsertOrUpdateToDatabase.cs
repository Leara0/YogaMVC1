using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public interface IInsertOrUpdateToDatabase
{
    void UpdatePoseToDatabase(InsertOrUpdatePoseModel model);
    
    int InsertPoseToDatabase(InsertOrUpdatePoseModel model);
}
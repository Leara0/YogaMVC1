using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public interface IInsertOrUpdateToDatabase
{
    void UpdatePoseToDatabase(UpdatePoseModel model);
    
    int InsertPoseToDatabase(InsertPoseModel model);
}
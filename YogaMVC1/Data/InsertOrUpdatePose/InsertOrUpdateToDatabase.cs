using System.Data;
using Dapper;
using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public class InsertOrUpdateToDatabase(IDbConnection db) : IInsertOrUpdateToDatabase
{
    private readonly IDbConnection _db = db;

    public void UpdatePoseToDatabase(UpdatePoseModel model)
    {
        //update pose table
        _db.Execute("UPDATE poses SET English_Name = @Name, Pose_Description = @Description, Pose_Benefits= @Benefits WHERE Pose_Id = @PoseId",
            new {Name = @model.Name, Description = @model.PoseDesc, Benefits = model.PoseBenefits, PoseId = model.PoseId});
        
        //delete old mapping
        _db.Execute("DELETE FROM pose_mapping WHERE pose_id = @PoseId", new { PoseId = model.PoseId });
        
        //insert new mapping
        foreach (var categoryId in model.SelectedCategoryIds)
        {
            _db.Execute(
                "INSERT INTO pose_mapping (pose_id, difficulty_id, category_id) VALUES (@PoseId, @DifficultyId, @CategoryId)",
                new
                {
                    PoseId = model.PoseId, DifficultyId = model.SelectedDifficultyId, CategoryId = categoryId
                });
        }
        


    }
}
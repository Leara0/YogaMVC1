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
        _db.Execute("UPDATE poses SET English_Name = @Name, Pose_Description = @Description, "+
                    "Pose_Benefits= @Benefits, Difficulty_Id = @Difficulty "+
                    "WHERE Pose_Id = @PoseId",
            new {Name = @model.Name, Description = @model.PoseDesc, Benefits = model.PoseBenefits, 
                Difficulty = model.SelectedDifficultyId, PoseId = model.PoseId});
        
        //delete old mapping
        _db.Execute("DELETE FROM pose_mapping WHERE pose_id = @PoseId", new { PoseId = model.PoseId });
        
        //insert new mapping
        foreach (var categoryId in model.SelectedCategoryIds)
        {
            _db.Execute(
                "INSERT INTO pose_mapping (pose_id, category_id) VALUES (@PoseId, @CategoryId)",
                new
                {
                    PoseId = model.PoseId, CategoryId = categoryId
                });
        }
    }

    public int InsertPoseToDatabase(InsertPoseModel model)
    {
        //insert into pose table (and grab the id right away to use in the next step
        var newPoseId = _db.ExecuteScalar<int>(
            @"INSERT INTO poses "+
            "(English_Name, Sanskrit_Name, Translation_Name, Pose_Description, Pose_Benefits, "+
             "Difficulty_Id, Url_Svg, Url_Svg_Alt)"+
          "VALUES"+ 
            "(@Name, @SanskritName, @TranslationName, @PoseDescription, "+ 
             "@PoseBenefits, @DifficultyId, @UrlSvg, @UrlSvgAlt);"+
          "SELECT LAST_INSERT_ID();",
            new
            {
                Name = model.Name, SanskritName = model.SanskritName, TranslationName = model.TranslationName,
                PoseDescription = model.PoseDescription, PoseBenefits = model.PoseBenefits,
                DifficultyId = model.SelectedDifficultyId, UrlSvg = model.UrlSvg, UrlSvgAlt = model.UrlSvgAlt
            });

        //insert into mapping table
        foreach (var categoryId in model.SelectedCategoryIds)
        {
            _db.Execute(
                "INSERT INTO pose_mapping (pose_id, category_id) VALUES (@PoseId, @CategoryId)",
                new
                {
                    PoseId = newPoseId, CategoryId = categoryId
                });
        }
        return newPoseId;
    }
}
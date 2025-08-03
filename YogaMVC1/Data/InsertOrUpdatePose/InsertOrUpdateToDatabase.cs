using System.Data;
using Dapper;
using YogaMVC1.Models;

namespace YogaMVC1.Data.InsertOrUpdatePose;

public class InsertOrUpdateToDatabase(IDbConnection db) : IInsertOrUpdateToDatabase
{
    private readonly IDbConnection _db = db;

    public void UpdatePoseToDatabase(InsertOrUpdatePoseModel model)
    {
        //update pose table
        _db.Execute("UPDATE poses SET English_Name = @Name, Sanskrit_Name = @SanskritName, "+
                    "Translation_Name = @TranslationName, Pose_Description = @Description, "+
                    "Pose_Benefits= @Benefits, Url_Svg = @UrlSvg, Url_Svg_Alt = @UrlSvgAlt, Difficulty_Id = @Difficulty "+
                    "WHERE Pose_Id = @PoseId",
            new {Name = model.Name, SanskritName = model.SanskritName, TranslationName = model.TranslationName,
                Description = model.PoseDescription, Benefits = model.PoseBenefits, UrlSvg = model.UrlSvg, UrlSvgAlt = model.UrlSvgAlt,
                Difficulty = model.DifficultyId, PoseId = model.PoseId});
        
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

    public int InsertPoseToDatabase(InsertOrUpdatePoseModel model)
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
                DifficultyId = model.DifficultyId, UrlSvg = model.UrlSvg, UrlSvgAlt = model.UrlSvgAlt
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
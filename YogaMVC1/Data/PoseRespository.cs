using System.Data;
using Dapper;
using YogaMVC1.Models;

namespace YogaMVC1.Data;

public class PoseRepository(IDbConnection db)  : IPoseRepository
{
    private readonly IDbConnection _db = db;
    
    public IEnumerable<Pose> GetAllPoses()
    {
        return _db.Query<Pose>("SELECT * FROM poses");
    }

    public Pose GetPoseById(int id)
    {
        var pose = _db.QuerySingleOrDefault<Pose>("SELECT * FROM poses WHERE Pose_Id = @id", new { id });
        var category = new CategoryRepository(_db);
        
        pose.Difficulty_Level = GetDifficultyLevelByPoseId(id);
        pose.CategoryList = category.ListCategoryNameFromCategoryIdList(GetCategoryIdByPoseId(id));
        return pose;
    }
    public string GetDifficultyLevelByPoseId(int id)
    {
        var difficultyId = _db.QuerySingleOrDefault<int>("SELECT DISTINCT Difficulty_Id FROM pose_mapping WHERE Pose_Id = @id", new { id });
        
        return _db.QuerySingleOrDefault<string>("SELECT Difficulty_Level FROM difficulty WHERE Difficulty_Id = @id",
            new { id = difficultyId });
    }

    public int GetDifficultyIdByPoseId(int id)
    {
        return _db.QuerySingleOrDefault<int>("SELECT DISTINCT Difficulty_Id FROM pose_mapping WHERE Pose_Id = @id", new { id });
    }

    public List<int> GetCategoryIdByPoseId(int id)
    {
        return _db.Query<int>("SELECT Category_Id FROM pose_mapping WHERE Pose_Id = @id", new { id }).ToList();
    }

    public List<Pose> GetAllPosesInThisCategory(List<int> poseIds)
    {
        var poses = _db.Query<Pose>("SELECT * FROM poses WHERE Pose_Id IN @Ids", 
            new { Ids = poseIds }).ToList();
        return poses;
    }

    

   }
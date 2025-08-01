using System.Data;
using Dapper;
using YogaMVC1.Models;

namespace YogaMVC1.Data;

public class DifficultyRepository(IDbConnection db): IDifficultyRepository
{
    private readonly IDbConnection _db = db;


    public IEnumerable<Difficulty> GetAllDifficulties()
    {
        return _db.Query<Difficulty>("select * from difficulty");
    }
}
using System.Data;
using Dapper;
using YogaMVC1.Models;

namespace YogaMVC1.Data;

public class CategoryRepository(IDbConnection db): ICategoryRepository
{
    private readonly IDbConnection _db = db;


    public IEnumerable<Category> GetAllCategories()
    {
        return _db.Query<Category>("SELECT * FROM Categories");
    }

    public Category GetCategoryById(int id)
    {
        return _db.QuerySingleOrDefault<Category>("SELECT * FROM categories WHERE Category_Id = @id", new { id });
    }

    public List<(string CatName, int? CatId)>  ListCategoryNameFromCategoryIdList(List<int> categoryIdList)
    {
        var allCategories = GetAllCategories();
        List<(string, int?)> ListCategory = new List<(string, int?)>();
        
        foreach (var categoryId in categoryIdList)
        {
            var matched = allCategories.FirstOrDefault(x => x.Category_Id == categoryId);
            if (matched != null)
            {
                ListCategory.Add((matched.Category_Name, matched.Category_Id));
            }
        }
        return ListCategory;
    }

    public List<int> GetPoseIdByCategoryId(int id)
    {
        return _db.Query<int>("SELECT Pose_Id FROM pose_mapping WHERE Category_Id = @id", new { id }).ToList();
    }
}
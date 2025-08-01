using YogaMVC1.Models;

namespace YogaMVC1.Data;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAllCategories();
    Category GetCategoryById(int id);

    List<(string CatName, int? CatId)> ListCategoryNameFromCategoryIdList(List<int> categoryIdList);

    List<int> GetPoseIdByCategoryId(int id);

}
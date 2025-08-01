using Microsoft.AspNetCore.Mvc;
using YogaMVC1.Data;
using YogaMVC1.Models;

namespace YogaMVC1.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _catRepo;
    private readonly IPoseRepository _poseRepo;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryRepository catRepo, IPoseRepository poseRepo, ILogger<CategoryController> logger)
    {
        _catRepo = catRepo;
        _poseRepo = poseRepo;
        _logger = logger;
    }
    
    // GET
    public IActionResult Index()
    {
        var categories = _catRepo.GetAllCategories();
        _logger.LogInformation("All categories retrieved");
        return View(categories);
    }

    public IActionResult GetCategoryById(int id)
    {
        var category = _catRepo.GetCategoryById(id);
        category.PosesInThisCategory = _catRepo.GetPoseIdByCategoryId(id);

        var poses = _poseRepo.GetAllPosesInThisCategory(category.PosesInThisCategory);
        var viewModel = new CategoryDetailViewModel()
        {
            Category = category,
            Poses = poses,
        };
        
        
        _logger.LogInformation("GetCategoryById retrieved");
        return View(viewModel);
    }
}
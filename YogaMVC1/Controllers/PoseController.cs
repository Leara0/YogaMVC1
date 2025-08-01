using Microsoft.AspNetCore.Mvc;
using YogaMVC1.Data;
using YogaMVC1.Data.InsertOrUpdatePose;
using YogaMVC1.Models;

namespace YogaMVC1.Controllers;

public class PoseController : Controller
{
    private readonly ILogger<PoseController> _logger;
    private readonly IPoseRepository _poseRepo;
    private readonly IInsertOrUpdateFactory _insertOrUpdateFactory;
    private readonly IInsertOrUpdateToDatabase _insertOrUpdateToDatabase;

    public PoseController(IPoseRepository poseRepo, ILogger<PoseController> logger, IInsertOrUpdateFactory insertOrUpdateFactory, IInsertOrUpdateToDatabase insertOrUpdateToDatabase)
    {
        _poseRepo = poseRepo;
        _logger = logger;
        _insertOrUpdateFactory = insertOrUpdateFactory;
        _insertOrUpdateToDatabase = insertOrUpdateToDatabase;
    }
    // GET
    public IActionResult Index()
    {
        var poses = _poseRepo.GetAllPoses();
        _logger.LogInformation("PoseController Index Action called");
        return View(poses);
    }

    public IActionResult GetPose(int id)
    {
        var pose = _poseRepo.GetPoseById(id);
        return View(pose);
    }

    public IActionResult UpdatePose(int id)
    {
        var pose = _insertOrUpdateFactory.BuildUpdateModel(id);
        //this gets the update pose model which had the category and difficulty options (using SelectListItem)
        
        if (pose == null)
            return RedirectToAction("Index");
        return View(pose);
    }
//Post
    public IActionResult UpdatePoseToDatabase(UpdatePoseModel pose)
    {
        _insertOrUpdateToDatabase.UpdatePoseToDatabase(pose);
        return RedirectToAction("GetPose", new { id = pose.PoseId });
    }
    //GET
    public IActionResult InsertPose()
    {
        var pose = _insertOrUpdateFactory.BuildInsertModel();
        return View(pose);
    }
}
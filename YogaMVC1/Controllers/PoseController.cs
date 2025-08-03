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
        _logger.LogInformation($"Pose Controller GetPoseById called on pose {id}");
        return View(pose);
    }

    public IActionResult UpdatePose(int id)
    {
        var pose = _insertOrUpdateFactory.BuildUpdateModel(id);
        //this gets the update pose model which had the category and difficulty options (using SelectListItem)
        
        _logger.LogInformation("Pose Controller UpdatePose Action called");
        if (pose == null)
            return RedirectToAction("Index");
        return View(pose);
    }
    [HttpPost]
    public IActionResult UpdatePoseToDatabase(InsertOrUpdatePoseModel pose)
    {
        _insertOrUpdateToDatabase.UpdatePoseToDatabase(pose);
        _logger.LogInformation("Pose Controller UpdatePoseToDatabase called");
        return RedirectToAction("GetPose", new { id = pose.PoseId });
    }
    //GET
    public IActionResult InsertPose()
    {
        var pose = _insertOrUpdateFactory.BuildInsertModel();
        _logger.LogInformation("Pose Controller InsertPose Action called");
        return View(pose);
    }

    [HttpPost]
    public IActionResult InsertPoseToDatabase(InsertOrUpdatePoseModel orUpdatePose)
    {
        var newPoseId = _insertOrUpdateToDatabase.InsertPoseToDatabase(orUpdatePose);
        _logger.LogInformation("Pose Controller InsertPostToDatabase called");
        return RedirectToAction("GetPose", new { id = newPoseId });
    }
    [HttpPost]
    public IActionResult DeletePose(int id)
    {
        _logger.LogInformation($"DeletePose called with id: {id}");
        _poseRepo.DeletePoseById(id);
        _logger.LogInformation("Pose Controller DeletePose Action called");
        return RedirectToAction("Index");
    }
}
using Microsoft.AspNetCore.Mvc;
using YogaMVC1.Data;
using YogaMVC1.Data.UpdateFactory;
using YogaMVC1.Models;

namespace YogaMVC1.Controllers;

public class PoseController : Controller
{
    private readonly ILogger<PoseController> _logger;
    private readonly IPoseRepository _poseRepo;
    private readonly IPoseUpdateFactory _updateFactory;

    public PoseController(IPoseRepository poseRepo, ILogger<PoseController> logger, IPoseUpdateFactory updateFactory)
    {
        _poseRepo = poseRepo;
        _logger = logger;
        _updateFactory = updateFactory;
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
        var pose = _updateFactory.BuildModel(id);
        //this gets the update pose model which had the category and difficulty options (using SelectListItem)
        
        if (pose == null)
            return RedirectToAction("Index");
        return View(pose);
    }

    public IActionResult UpdatePoseToDatabase(UpdatePoseModel pose)
    {
        //@TODO call the repo action to update!
        return RedirectToAction("GetPose", new { id = pose.PoseId });
    }
}
using YogaMVC1.Models;

namespace YogaMVC1.Data.UpdateFactory;

public interface IPoseUpdateFactory
{
    UpdatePoseModel BuildModel(int poseId);
}
using YogaMVC1.Models;

namespace YogaMVC1.Data;

public interface IDifficultyRepository
{
    IEnumerable<Difficulty> GetAllDifficulties();
}
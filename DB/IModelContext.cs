namespace DevelopersToday_TestTask.DB;

public interface IModelContext
{
    public void AddModels(List<DefaulModel> models);
    public long GetAmount();
}

namespace Dinazor.Core.Interfaces.Databases
{
    public interface ISoftDelete : IEntity
    {
        bool IsDeleted { get; set; }
    }
}

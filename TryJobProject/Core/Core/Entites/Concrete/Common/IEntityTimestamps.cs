namespace Core.Entites.Concrete.Common
{
    public interface IEntityTimestamps
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
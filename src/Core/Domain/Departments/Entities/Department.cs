using Shared.Core.Domain.Impl.Entity;

namespace Actions.Core.Domain.Departments.Entities
{
    public class Department : Entity<Department, string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RegionId { get; set; }
        public string ParentId { get; set; }
        public int Order { get; set; }
    }
}
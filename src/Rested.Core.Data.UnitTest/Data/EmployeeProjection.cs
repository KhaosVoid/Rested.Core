using Rested.Core.Data;

namespace Rested.Core.Data.UnitTest.Data
{
    public class EmployeeProjection : Projection
    {
        #region Properties

        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public DateTime? DOB { get; set; }
        public DateOnly? StartDate { get; set; }
        public EmploymentTypes? EmploymentType { get; set; }

        #endregion Properties

        #region Ctor

        static EmployeeProjection()
        {
            RegisterMapping((EmployeeProjection p) => p.Id, (IDocument<Employee> d) => d.Id);
            RegisterMapping((EmployeeProjection p) => p.FullName, (IDocument<Employee> d) => d.Data.FullName);
            RegisterMapping((EmployeeProjection p) => p.Age, (IDocument<Employee> d) => d.Data.Age);
            RegisterMapping((EmployeeProjection p) => p.DOB, (IDocument<Employee> d) => d.Data.DOB);
            RegisterMapping((EmployeeProjection p) => p.StartDate, (IDocument<Employee> d) => d.Data.StartDate);
            RegisterMapping((EmployeeProjection p) => p.EmploymentType, (IDocument<Employee> d) => d.Data.EmploymentType);
        }

        #endregion Ctor
    }
}

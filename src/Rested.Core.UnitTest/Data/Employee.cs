﻿using Rested.Core.CQRS.Data;

namespace Rested.Core.UnitTest.Data
{
    public class Employee : IData
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public DateTime? DOB { get; set; }
        public DateOnly? StartDate { get; set; }
        public EmploymentTypes? EmploymentType { get; set; }

        [SearchIgnore]
        public object? Metadata { get; internal set; } = "Test Metadata that cannot be searched";
    }
}

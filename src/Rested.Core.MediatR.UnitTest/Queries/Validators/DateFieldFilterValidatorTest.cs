using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

[TestClass]
public class DateFieldFilterValidatorTest : FieldFilterValidatorTest<DateFieldFilterValidator, DateFieldFilter>
{
    #region Test Methods

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsRequiredValidation()
    {
        var dateFieldFilter = new DateFieldFilter()
        {
            FieldName = null,
            FilterOperation = DateFieldFilterOperations.Equals,
            Value = DateOnly.FromDateTime(DateTime.Now)
        };
        
        TestFieldFilterNameIsRequiredValidation(dateFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsInvalidValidation()
    {
        var dateFieldFilter = new DateFieldFilter()
        {
            FieldName = "invalidField",
            FilterOperation = DateFieldFilterOperations.Equals,
            Value = DateOnly.FromDateTime(DateTime.Now)
        };
        
        TestFieldFilterNameIsInvalidValidation(dateFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterOperationNotSupportedValidation()
    {
        var dateFieldFilter = new DateFieldFilter()
        {
            FieldName = "data.startDate",
            FilterOperation = (DateFieldFilterOperations)9999,
            Value = DateOnly.FromDateTime(DateTime.Now)
        };
        
        TestFieldFilterOperationNotSupportedValidation(dateFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterValueIsRequiredValidation()
    {
        var dateFieldFilter = new DateFieldFilter()
        {
            FieldName = "data.startDate",
            FilterOperation = DateFieldFilterOperations.Equals,
            Value = null
        };
        
        TestFieldFilterValueIsRequiredValidation(dateFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterToValueIsRequiredValidation()
    {
        var dateFieldFilter = new DateFieldFilter()
        {
            FieldName = "data.startDate",
            FilterOperation = DateFieldFilterOperations.InRange,
            Value = DateOnly.FromDateTime(DateTime.Now),
            ToValue = null
        };
        
        TestFieldFilterToValueIsRequiredValidation(dateFieldFilter);
    }
    
    #endregion Test Methods
}
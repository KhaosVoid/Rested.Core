using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

[TestClass]
public class DateTimeFieldFilterValidatorTest : FieldFilterValidatorTest<DateTimeFieldFilterValidator, DateTimeFieldFilter>
{
    #region Test Methods

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsRequiredValidation()
    {
        var dateTimeFieldFilter = new DateTimeFieldFilter()
        {
            FieldName = null,
            FilterOperation = DateTimeFieldFilterOperations.Equals,
            Value = DateTime.Now
        };
        
        TestFieldFilterNameIsRequiredValidation(dateTimeFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsInvalidValidation()
    {
        var dateTimeFieldFilter = new DateTimeFieldFilter()
        {
            FieldName = "invalidField",
            FilterOperation = DateTimeFieldFilterOperations.Equals,
            Value = DateTime.Now
        };
        
        TestFieldFilterNameIsInvalidValidation(dateTimeFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterOperationNotSupportedValidation()
    {
        var dateTimeFieldFilter = new DateTimeFieldFilter()
        {
            FieldName = "data.dob",
            FilterOperation = (DateTimeFieldFilterOperations)9999,
            Value = DateTime.Now
        };
        
        TestFieldFilterOperationNotSupportedValidation(dateTimeFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterValueIsRequiredValidation()
    {
        var dateTimeFieldFilter = new DateTimeFieldFilter()
        {
            FieldName = "data.dob",
            FilterOperation = DateTimeFieldFilterOperations.Equals,
            Value = null
        };
        
        TestFieldFilterValueIsRequiredValidation(dateTimeFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterToValueIsRequiredValidation()
    {
        var dateTimeFieldFilter = new DateTimeFieldFilter()
        {
            FieldName = "data.dob",
            FilterOperation = DateTimeFieldFilterOperations.InRange,
            Value = DateTime.Now,
            ToValue = null
        };
        
        TestFieldFilterToValueIsRequiredValidation(dateTimeFieldFilter);
    }
    
    #endregion Test Methods
}
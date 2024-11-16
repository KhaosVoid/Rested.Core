using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

[TestClass]
public class NumberFieldFilterValidatorTest : FieldFilterValidatorTest<NumberFieldFilterValidator, NumberFieldFilter>
{
    #region Test Methods

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsRequiredValidation()
    {
        var numberFieldFilter = new NumberFieldFilter()
        {
            FieldName = null,
            FilterOperation = NumberFieldFilterOperations.Equals,
            Value = 0
        };
        
        TestFieldFilterNameIsRequiredValidation(numberFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsInvalidValidation()
    {
        var numberFieldFilter = new NumberFieldFilter()
        {
            FieldName = "invalidField",
            FilterOperation = NumberFieldFilterOperations.Equals,
            Value = 0
        };
        
        TestFieldFilterNameIsInvalidValidation(numberFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterOperationNotSupportedValidation()
    {
        var numberFieldFilter = new NumberFieldFilter()
        {
            FieldName = "data.age",
            FilterOperation = (NumberFieldFilterOperations)9999,
            Value = 0
        };
        
        TestFieldFilterOperationNotSupportedValidation(numberFieldFilter);
    }
    
    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterValueIsRequiredValidation()
    {
        var numberFieldFilter = new NumberFieldFilter()
        {
            FieldName = "data.age",
            FilterOperation = NumberFieldFilterOperations.Equals,
            Value = null
        };
        
        TestFieldFilterValueIsRequiredValidation(numberFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterToValueIsRequiredValidation()
    {
        var numberFieldFilter = new NumberFieldFilter()
        {
            FieldName = "data.age",
            FilterOperation = NumberFieldFilterOperations.InRange,
            Value = 30,
            ToValue = null
        };
        
        TestFieldFilterToValueIsRequiredValidation(numberFieldFilter);
    }
    
    #endregion Test Methods
}   
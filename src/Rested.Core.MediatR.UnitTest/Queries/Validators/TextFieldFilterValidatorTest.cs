using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

[TestClass]
public class TextFieldFilterValidatorTest : FieldFilterValidatorTest<TextFieldFilterValidator, TextFieldFilter>
{
    #region Test Methods

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsRequiredValidation()
    {
        var textFieldFilter = new TextFieldFilter()
        {
            FieldName = null,
            FilterOperation = TextFieldFilterOperations.Equals,
            Value = "testValue"
        };
        
        TestFieldFilterNameIsRequiredValidation(textFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterNameIsInvalidValidation()
    {
        var textFieldFilter = new TextFieldFilter()
        {
            FieldName = "invalidField",
            FilterOperation = TextFieldFilterOperations.Equals,
            Value = "testValue"
        };
        
        TestFieldFilterNameIsInvalidValidation(textFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterOperationNotSupportedValidation()
    {
        var textFieldFilter = new TextFieldFilter()
        {
            FieldName = "data.firstName",
            FilterOperation = (TextFieldFilterOperations)9999,
            Value = "firstName"
        };
        
        TestFieldFilterOperationNotSupportedValidation(textFieldFilter);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void FieldFilterValueIsRequiredValidation()
    {
        var textFieldFilter = new TextFieldFilter()
        {
            FieldName = "data.firstName",
            FilterOperation = TextFieldFilterOperations.Equals,
            Value = null
        };
        
        TestFieldFilterValueIsRequiredValidation(textFieldFilter);
    }
    
    #endregion Test Methods
}
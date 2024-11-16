using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

[TestClass]
public class OperatorFilterValidatorTest : FilterValidatorTest<OperatorFilterValidator, OperatorFilter>
{
    #region Test Methods

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void OperatorFilterOperationNotSupportedValidation()
    {
        var operatorFilter = new OperatorFilter()
        {
            Operator = (FilterOperators)9999,
            Filters =
            [
                new TextFieldFilter()
                {
                    FieldName = "data.firstName",
                    FilterOperation = TextFieldFilterOperations.Equals,
                    Value = "test"
                }
            ]
        };
        
        TestFilterValidationRule(CreateValidator(), operatorFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
    }

    [TestMethod]
    [TestCategory(TESTCATEGORY_FILTER_VALIDATION_RULE_TESTS)]
    public void OperatorFilterFiltersIsRequiredValidation()
    {
        var operatorFilter = new OperatorFilter();
        
        TestFilterValidationRule(CreateValidator(), operatorFilter, ServiceErrorCodes.CommonErrorCodes.OperatorFilterFiltersIsRequired);
    }
    
    #endregion Test Methods
}
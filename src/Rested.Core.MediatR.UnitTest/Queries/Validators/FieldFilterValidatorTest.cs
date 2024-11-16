using Rested.Core.Data.Search;
using Rested.Core.MediatR.Queries.Validators;

namespace Rested.Core.MediatR.UnitTest.Queries.Validators;

public abstract class FieldFilterValidatorTest<TFieldFilterValidator, TFieldFilter> :
    FilterValidatorTest<TFieldFilterValidator, TFieldFilter>
    where TFieldFilterValidator : FieldFilterValidator<TFieldFilter>
    where TFieldFilter : IFieldFilter
{
    #region Methods
    
    protected void TestFieldFilterNameIsRequiredValidation(TFieldFilter fieldFilter) =>
        TestFilterValidationRule(CreateValidator(), fieldFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterNameIsRequired);
    
    protected void TestFieldFilterNameIsInvalidValidation(TFieldFilter fieldFilter) =>
        TestFilterValidationRule(CreateValidator(), fieldFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterNameIsInvalid);
    
    protected void TestFieldFilterValueIsRequiredValidation(TFieldFilter fieldFilter) =>
        TestFilterValidationRule(CreateValidator(), fieldFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterValueIsRequired);
    
    protected void TestFieldFilterOperationNotSupportedValidation(TFieldFilter fieldFilter) =>
        TestFilterValidationRule(CreateValidator(), fieldFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterOperationNotSupported);
    
    protected void TestFieldFilterToValueIsRequiredValidation(TFieldFilter fieldFilter) =>
        TestFilterValidationRule(CreateValidator(), fieldFilter, ServiceErrorCodes.CommonErrorCodes.FieldFilterToValueIsRequired);
        
    #endregion Methods
}
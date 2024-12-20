﻿namespace Rested.Core.Data.Search;

/// <summary>
/// The sort direction for a field.
/// </summary>
public enum FieldSortDirection
{
    Ascending = 0,
    Descending = 1,
}

/// <summary>
/// The filter types for a field.
/// </summary>
public enum FilterTypes
{
    TextFieldFilter = 0,
    NumberFieldFilter = 1,
    DateFieldFilter = 2,
    DateTimeFieldFilter = 3,
    OperatorFilter = 4,
}

/// <summary>
/// The filter operations for a field.
/// </summary>
public enum FieldFilterOperations
{
    /// <summary>
    /// Filter operation to evaluate if a field value is equal to the filter value.
    /// </summary>
    Equals = 0,

    /// <summary>
    /// Filter operation to evaluate if a field value is not equal to the filter value.
    /// </summary>
    NotEquals = 1,

    /// <summary>
    /// Filter operation to evaluate if a field value contains the filter value.
    /// </summary>
    Contains = 2,

    /// <summary>
    /// Filter operation to evaluate if a field value does not contain the filter value.
    /// </summary>
    NotContains = 3,

    /// <summary>
    /// Filter operation to evaluate if a field value starts with the filter value.
    /// </summary>
    StartsWith = 4,

    /// <summary>
    /// Filter operation to evaluate if a field value ends with the filter value.
    /// </summary>
    EndsWith = 5,

    /// <summary>
    /// Filter operation to evaluate if a field value is less than the filter value.
    /// </summary>
    LessThan = 6,

    /// <summary>
    /// Filter operation to evaluate if a field value is less than or equal to the filter value.
    /// </summary>
    LessThanOrEqual = 7,

    /// <summary>
    /// Filter operation to evaluate if a field value is greater than the filter value.
    /// </summary>
    GreaterThan = 8,

    /// <summary>
    /// Filter operation to evaluate if a field value is greater than or equal to the filter value.
    /// </summary>
    GreaterThanOrEqual = 9,

    /// <summary>
    /// Filter operation to evaluate if a field value is in range of the filter value and filter to value.
    /// </summary>
    InRange = 10,

    /// <summary>
    /// Filter operation to evaluate if a field value is blank.
    /// </summary>
    Blank = 11,

    /// <summary>
    /// Filter operation to evaluate if a field value is not blank.
    /// </summary>
    NotBlank = 12,

    /// <summary>
    /// Filter operation to evaluate if a field value is empty.
    /// </summary>
    Empty = 13
}

/// <summary>
/// The filter operations for a TextFieldFilter.
/// </summary>
public enum TextFieldFilterOperations
{
    /// <summary>
    /// Text filter operation to evaluate if a field value is equal to the filter value.
    /// </summary>
    Equals = FieldFilterOperations.Equals,

    /// <summary>
    /// Text filter operation to evaluate if a field value is not equal to the filter value.
    /// </summary>
    NotEquals = FieldFilterOperations.NotEquals,

    /// <summary>
    /// Text filter operation to evaluate if a field value contains the filter value.
    /// </summary>
    Contains = FieldFilterOperations.Contains,

    /// <summary>
    /// Text filter operation to evaluate if a field value does not contain the filter value.
    /// </summary>
    NotContains = FieldFilterOperations.NotContains,

    /// <summary>
    /// Text filter operation to evaluate if a field value starts with the filter value.
    /// </summary>
    StartsWith = FieldFilterOperations.StartsWith,

    /// <summary>
    /// Text filter operation to evaluate if a field value ends with the filter value.
    /// </summary>
    EndsWith = FieldFilterOperations.EndsWith,

    /// <summary>
    /// Text filter operation to evaluate if a field value is blank.
    /// </summary>
    Blank = FieldFilterOperations.Blank,

    /// <summary>
    /// Text filter operation to evaluate if a field value is not blank.
    /// </summary>
    NotBlank = FieldFilterOperations.NotBlank,

    /// <summary>
    /// Text filter operation to evaluate if a field value is empty.
    /// </summary>
    Empty = FieldFilterOperations.Empty,
}

/// <summary>
/// The filter operations for a NumberFieldFilter.
/// </summary>
public enum NumberFieldFilterOperations
{
    /// <summary>
    /// Number filter operation to evaluate if a field value is equal to the filter value.
    /// </summary>
    Equals = FieldFilterOperations.Equals,

    /// <summary>
    /// Number filter operation to evaluate if a field value is not equal to the filter value.
    /// </summary>
    NotEquals = FieldFilterOperations.NotEquals,

    /// <summary>
    /// Number filter operation to evaluate if a field value is less than the filter value.
    /// </summary>
    LessThan = FieldFilterOperations.LessThan,

    /// <summary>
    /// Number filter operation to evaluate if a field value is less than or equal to the filter value.
    /// </summary>
    LessThanOrEqual = FieldFilterOperations.LessThanOrEqual,

    /// <summary>
    /// Number filter operation to evaluate if a field value is greater than the filter value.
    /// </summary>
    GreaterThan = FieldFilterOperations.GreaterThan,

    /// <summary>
    /// Number filter operation to evaluate if a field value is greater than or equal to the filter value.
    /// </summary>
    GreaterThanOrEqual = FieldFilterOperations.GreaterThanOrEqual,

    /// <summary>
    /// Number filter operation to evaluate if a field value is in range of the filter value and filter to value.
    /// </summary>
    InRange = FieldFilterOperations.InRange,

    /// <summary>
    /// Number filter operation to evaluate if a field value is blank.
    /// </summary>
    Blank = FieldFilterOperations.Blank,

    /// <summary>
    /// Number filter operation to evaluate if a field value is not blank.
    /// </summary>
    NotBlank = FieldFilterOperations.NotBlank,

    /// <summary>
    /// Number filter operation to evaluate if a field value is empty.
    /// </summary>
    Empty = FieldFilterOperations.Empty,
}

/// <summary>
/// The filter operations for a DateFieldFilter.
/// </summary>
public enum DateFieldFilterOperations
{
    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is equal to the filter value.
    /// </summary>
    Equals = FieldFilterOperations.Equals,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is not equal to the filter value.
    /// </summary>
    NotEquals = FieldFilterOperations.NotEquals,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is less than the filter value.
    /// </summary>
    LessThan = FieldFilterOperations.LessThan,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is greater than the filter value.
    /// </summary>
    GreaterThan = FieldFilterOperations.GreaterThan,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is in range of the filter value and filter to value.
    /// </summary>
    InRange = FieldFilterOperations.InRange,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is blank.
    /// </summary>
    Blank = FieldFilterOperations.Blank,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is not blank.
    /// </summary>
    NotBlank = FieldFilterOperations.NotBlank,

    /// <summary>
    /// DateOnly filter operation to evaluate if a field value is empty.
    /// </summary>
    Empty = FieldFilterOperations.Empty,
}
    
/// <summary>
/// The filter operations for a DateTimeFieldFilter.
/// </summary>
public enum DateTimeFieldFilterOperations
{
    /// <summary>
    /// DateTime filter operation to evaluate if a field value is equal to the filter value.
    /// </summary>
    Equals = FieldFilterOperations.Equals,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is not equal to the filter value.
    /// </summary>
    NotEquals = FieldFilterOperations.NotEquals,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is less than the filter value.
    /// </summary>
    LessThan = FieldFilterOperations.LessThan,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is greater than the filter value.
    /// </summary>
    GreaterThan = FieldFilterOperations.GreaterThan,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is in range of the filter value and filter to value.
    /// </summary>
    InRange = FieldFilterOperations.InRange,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is blank.
    /// </summary>
    Blank = FieldFilterOperations.Blank,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is not blank.
    /// </summary>
    NotBlank = FieldFilterOperations.NotBlank,

    /// <summary>
    /// DateTime filter operation to evaluate if a field value is empty.
    /// </summary>
    Empty = FieldFilterOperations.Empty,
}

/// <summary>
/// The filter operators for an OperatorFilter.
/// </summary>
public enum FilterOperators
{
    /// <summary>
    /// Filter operator that performs conditional AND operation against all filters in the OperatorFilter.
    /// </summary>
    And = 0,

    /// <summary>
    /// Filter operator that performs conditional OR operation against all filters in the OperatorFilter.
    /// </summary>
    Or = 1,
}
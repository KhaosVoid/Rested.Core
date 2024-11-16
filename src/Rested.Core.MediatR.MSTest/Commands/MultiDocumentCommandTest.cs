using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.MediatR.Commands;
using Rested.Core.MediatR.Validation;

namespace Rested.Core.MediatR.MSTest.Commands;

public abstract class MultiDocumentCommandTest<TData, TDocument, TMultiDocumentCommand, TMultiDocumentCommandValidator, TMultiDocumentCommandHandler> :
    CommandTest<List<TDocument>, TMultiDocumentCommand, TMultiDocumentCommandValidator, TMultiDocumentCommandHandler>
    where TData : IData
    where TDocument : IDocument<TData>
    where TMultiDocumentCommand : MultiDocumentCommand<TData, TDocument>
    where TMultiDocumentCommandValidator : MultiDocumentCommandValidator<TData, TDocument, TMultiDocumentCommand>
    where TMultiDocumentCommandHandler : MultiDocumentCommandHandler<TData, TDocument, TMultiDocumentCommand>
{
    #region Properties

    protected List<TDocument> TestDocuments { get; set; }

    #endregion Properties

    #region Members

    protected readonly string ASSERTMSG_VALIDATION_ERROR_COUNT_NOT_EQUAL = "expected the number of validation errors to equal the number documents";
    protected readonly string ASSERTMSG_SAME_VALIDATION_ERROR_FOR_ALL_DOCUMENTS = "expected the same validation error for each document for this test";

    #endregion Members

    #region Initialization

    protected override void OnInitialize()
    {
        base.OnInitialize();

        TestContext.WriteLine("Initializing Test Documents...");
        OnInitializeTestDocuments();
    }

    protected abstract void OnInitializeTestDocuments();
    protected abstract List<TData> InitializeTestData();

    #endregion Initialization

    #region Methods

    protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
    protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

    protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
    protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

    protected void TestCommandValidationRule(CommandActions action, ServiceErrorCode serviceErrorCode, bool duplicateRules = false, params object[] messageFormatArgs)
    {
        var validationResult = ExecuteCommandValidation(action);

        if (duplicateRules)
        {
            validationResult.Errors.Count.Should().Be(
                expected: TestDocuments.Count,
                because: ASSERTMSG_VALIDATION_ERROR_COUNT_NOT_EQUAL);

            validationResult
                .Errors
                .All(
                    error =>
                        error.ErrorMessage == string.Format(serviceErrorCode.Message, messageFormatArgs) &&
                        error.ErrorCode == serviceErrorCode.ExtendedStatusCode)
                .Should()
                .BeTrue(because: ASSERTMSG_SAME_VALIDATION_ERROR_FOR_ALL_DOCUMENTS);
        }

        else
        {
            TestCommandValidationRule(action, serviceErrorCode, messageFormatArgs);
        }
    }

    #endregion Methods
}
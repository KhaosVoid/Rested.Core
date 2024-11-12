using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rested.Core.Data;
using Rested.Core.Data.Document;
using Rested.Core.MediatR.Commands;

namespace Rested.Core.MediatR.MSTest.Commands
{
    public abstract class DocumentCommandTest<TData, TDocument, TDocumentCommand, TDocumentCommandValidator, TDocumentCommandHandler> :
        CommandTest<TDocument, TDocumentCommand, TDocumentCommandValidator, TDocumentCommandHandler>
        where TData : IData
        where TDocument : IDocument<TData>
        where TDocumentCommand : DocumentCommand<TData, TDocument>
        where TDocumentCommandValidator : DocumentCommandValidator<TData, TDocument, TDocumentCommand>
        where TDocumentCommandHandler : DocumentCommandHandler<TData, TDocument, TDocumentCommand>
    {
        #region Properties

        protected TDocument TestDocument { get; set; }

        #endregion Properties

        #region Initialization

        protected override void OnInitialize()
        {
            base.OnInitialize();

            TestContext.WriteLine("Initializing Test Document...");
            OnInitializeTestDocument();
        }

        protected abstract void OnInitializeTestDocument();
        protected abstract TData InitializeTestData();

        #endregion Initialization

        #region Methods

        protected TDocument CreateDocument(TData data = default) => (TDocument)CreateDocument<TData>(data);
        protected abstract IDocument<T> CreateDocument<T>(T data = default) where T : IData;

        protected string GenerateNameFromData(int number = 1) => TestingUtils.GenerateNameFromData<TData>(number);
        protected string GenerateDescriptionFromData(int number = 1) => TestingUtils.GenerateDescriptionFromData<TData>(number);

        #endregion Methods
    }
}

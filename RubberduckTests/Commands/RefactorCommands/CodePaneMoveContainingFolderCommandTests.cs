﻿using System;
using Moq;
using NUnit.Framework;
using Rubberduck.Interaction;
using Rubberduck.Parsing.Rewriter;
using Rubberduck.Parsing.UIContext;
using Rubberduck.Parsing.VBA;
using Rubberduck.Refactorings;
using Rubberduck.Refactorings.MoveFolder;
using Rubberduck.Refactorings.MoveToFolder;
using Rubberduck.UI.Command;
using Rubberduck.UI.Command.Refactorings;
using Rubberduck.UI.Command.Refactorings.Notifiers;
using Rubberduck.VBEditor;
using Rubberduck.VBEditor.SafeComWrappers;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;
using Rubberduck.VBEditor.Utility;

namespace RubberduckTests.Commands.RefactorCommands
{
    [TestFixture]
    public class CodePaneRefactorMoveContainingFolderCommandTests : RefactorCodePaneCommandTestBase
    {
        //The only relevant test is in the base class.

        protected override CommandBase TestCommand(
            IVBE vbe, 
            RubberduckParserState state, 
            IRewritingManager rewritingManager,
            ISelectionService selectionService)
        {
            var factory = new Mock<IRefactoringPresenterFactory>().Object;
            var msgBox = new Mock<IMessageBox>().Object;

            var uiDispatcherMock = new Mock<IUiDispatcher>();
            uiDispatcherMock
                .Setup(m => m.Invoke(It.IsAny<Action>()))
                .Callback((Action action) => action.Invoke());
            var userInteraction = new RefactoringUserInteraction<IMoveMultipleFoldersPresenter, MoveMultipleFoldersModel>(factory, uiDispatcherMock.Object);

            var annotationUpdater = new AnnotationUpdater();
            var moveToFolderAction = new MoveToFolderRefactoringAction(rewritingManager, annotationUpdater);
            var moveFolderAction = new MoveFolderRefactoringAction(rewritingManager, moveToFolderAction);
            var moveMultipleFoldersAction = new MoveMultipleFoldersRefactoringAction(rewritingManager, moveFolderAction);

            var selectedDeclarationProvider = new SelectedDeclarationProvider(selectionService, state);

            var refactoring = new MoveContainingFolderRefactoring(moveMultipleFoldersAction, selectedDeclarationProvider, selectionService, userInteraction, state, state);
            var notifier = new MoveContainingFolderRefactoringFailedNotifier(msgBox);

            return new CodePaneRefactorMoveContainingFolderCommand(refactoring, notifier, selectionService, state, selectedDeclarationProvider);
        }

        protected override IVBE SetupAllowingExecution()
        {
            const string input =
                @"Public Sub Foo()
End Sub";
            var selection = Selection.Home;
            return TestVbe(input, selection, ComponentType.ClassModule);
        }
    }
}
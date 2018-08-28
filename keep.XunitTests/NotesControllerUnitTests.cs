using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using keep.Controllers;
using keep.Services;
using keep.Models;
using keep.Contracts;
using Microsoft.EntityFrameworkCore;

namespace keep.XunitTests
{
    public class NotesControllerUnitTests
    {

        MockDB mockDbHelper = new MockDB();
        //private readonly NotesController _controller;
        //public NotesController GetController()
        //{
        //    var OptionBuilder = new DbContextOptionsBuilder<DataContextt>();
        //    OptionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        //    keepContext kContext = new keepContext(OptionBuilder.Options);

        //    CreateData(OptionBuilder.Options);
        //    return new NotesController(kContext);
        //}

        //public void CreateData(DbContextOptions<keepContext> options)
        //{
        //    using (var kContext = new keepContext(options))
        //    {
        //        var Notes = new List<Note>
        //    {new Note()
        //    {
        //        ID = 1,
        //        Title = "First Note",
        //        PlainText = "Text in the first Note",
        //        PinnedStatus = true,

        //        Labels = new List<Label>
        //        {
        //            new Label{LabelText="label 1 in second NOte"},
        //            new Label{LabelText="label 2 in first Note"}
        //        },
        //        CheckList=new List<CheckListItem>
        //        {
        //            new CheckListItem{CheckListText="checklist 1 in first Note"},
        //            new CheckListItem {CheckListText="checklist 2 in first Note"}

        //        }
        //    },
        //    new Note()
        //    {
        //        ID = 2,
        //        Title = "Second Note",
        //        PlainText = "Text in the second Note",
        //        PinnedStatus = true,

        //        Labels = new List<Label>
        //        {
        //            new Label{LabelText="label 1 in second NOte"},
        //            new Label{LabelText="label 2 in second NOte"}
        //        },
        //        CheckList=new List<CheckListItem>
        //        {
        //            new CheckListItem{CheckListText="checklist 1 in second NOte"},
        //            new CheckListItem {CheckListText="checklist 2 in second NOte"}

        //        }
        //    }
        //    };
        //        kContext.Note.AddRange(Notes);
        //        kContext.SaveChanges();
        //    }
        //}

        //[Fact]
        //public async void TestGetNoteById()
        //{
        //    var _controller = GetController();
        //    var result = await _controller.GetNoteById(1);
        //    var objectresult = result as OkObjectResult;
        //    var notes = objectresult.Value as Note;
        //    Assert.Equal(1, notes.ID);
        //}

        [Fact]
        public async void TestGetNoteByLabel()
        {
            // Arrange
            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            MockDB mockDbHelper = new MockDB();
            mockRepo.Setup(service => service.GetByLabel("label 1 in second NOte")).Returns(mockDbHelper.GetTestResultListAsync());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.GetByLabel("label 1 in second NOte");
            OkObjectResult objectResult = result as OkObjectResult;
            Note objectResultValue = objectResult.Value as Note;
            // Assert
            Assert.Equal(200, objectResult.StatusCode);

            //    var _controller = GetController();
            //    var result = await _controller.GetNoteByLabel("label 1 in second NOte");
            //    var objectresult = result as OkObjectResult;
            //    var notes = objectresult.Value as List<Note>;
            //    //foreach (var x in notes)
            //    //{
            //    //    Assert.Equal("label 1 in second NOte", );
            //    //}
            //    Assert.Equal(2, notes.Count);
        }

        [Fact]
        public async void TestGetNoteByPin()
        {
            // Arrange
            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            MockDB mockDbHelper = new MockDB();
            mockRepo.Setup(service => service.GetByPin(true)).Returns(mockDbHelper.GetTestResultListAsync());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.GetByPinnedStatus(true);
            OkObjectResult objectResult = result as OkObjectResult;
            Note objectResultValue = objectResult.Value as Note;
            // Assert
            Assert.Equal(200, objectResult.StatusCode);

            //var _controller = GetController();
            //var result = await _controller.GetNoteByPin(true);
            //var objectresult = result as OkObjectResult;
            //var notes = objectresult.Value as List<Note>;
            ////foreach (var x in notes)
            ////{
            ////    Assert.Equal("label 1 in second NOte", );
            ////}
            //Assert.Equal(2, notes.Count);
        }

        [Fact]
        public async void TestSearchByTitle()
        {
            // Arrange
            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            MockDB mockDbHelper = new MockDB();
            mockRepo.Setup(service => service.SearchByTitle("First Note")).Returns(mockDbHelper.GetTestResultListAsync());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.GetByTitile("First Note");
            OkObjectResult objectResult = result as OkObjectResult;
            Note objectResultValue = objectResult.Value as Note;
            // Assert
            Assert.Equal(200, objectResult.StatusCode);

            //    var _controller = GetController();
            //    var result = await _controller.SearchNoteByTitle("First Note");
            //    var objectresult = result as OkObjectResult;
            //    var notes = objectresult.Value as List<Note>;
            //    //foreach (var x in notes)
            //    //{
            //    //    Assert.Equal("label 1 in second NOte", );
            //    //}
            //    Assert.Equal(1, notes.Count);
        }

        

        [Fact]
        public async void TestGetAllNotes()
        {
            // Arrange
            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            MockDB mockDbHelper = new MockDB();
            mockRepo.Setup(service => service.GetAllNotes()).Returns(mockDbHelper.GetTestResultListAsync());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            List<Note> objectResultValue = objectResult.Value as List<Note>;
            // Assert
            Assert.Equal(200, objectResult.StatusCode);
            //Assert.Equal(1, objectResultValue.ID);



            //var _controller = GetController();
            //var result = await _controller.GetNote();
            //var objectresult = result as OkObjectResult;
            //var notes = objectresult.Value as List<Note>;
            //Assert.Equal(2, notes.Count());
        }

        [Fact]
        public async void TestPutById()
        {

            // Arrange
            Note testNote = await mockDbHelper.GetTestResultData();

        Mock<IKeepService> mockRepo = new Mock<IKeepService>();
        mockRepo.Setup(repo => repo.UpdateNote(1, testNote)).Returns(mockDbHelper.GetTestResultData());
        NotesController controller = new NotesController(mockRepo.Object);

        // Act
        var result = await controller.Put(1, testNote);
        OkObjectResult objectResult = result as OkObjectResult;
        //Note objectResultValue = objectResult.Value as Note;
        // Assert
        //Assert.True(Assert.Equal(title,result.Result))
        Assert.Equal(200, objectResult.StatusCode);
            //    var note = new Note()
            //    {
            //        ID = 2,
            //        Title = "SECOND NOTE",
            //        PlainText = "Text in the second Note",
            //        PinnedStatus = true,


            //        Labels = new List<Label>
            //        {
            //            new Label{LabelText="label 1 in second NOte"},
            //            new Label{LabelText="label 2 in second NOte"}
            //        },
            //        CheckList = new List<CheckListItem>
            //        {
            //            new CheckListItem{CheckListText="checklist 1 in second NOte"},
            //            new CheckListItem {CheckListText="checklist 2 in second NOte"}

            //        }
            //    };
            //    var _controller = GetController();
            //    var result = await _controller.PutById(2, note);
            //    var objectresult = result as OkObjectResult;
            //    var notes = objectresult.Value as Note;
            //    Assert.Equal(2, notes.ID);
        }

        [Fact]
        public async void TestPost()
        {
            // Arrange
            Note testNote = await mockDbHelper.GetTestResultData();

            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            mockRepo.Setup(repo => repo.InsertNote(testNote)).Returns(mockDbHelper.GetTestResultData());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.Post(testNote);
            OkObjectResult objectResult = result as OkObjectResult;
            //Note objectResultValue = objectResult.Value as Note;
            // Assert
            //Assert.True(Assert.Equal(title,result.Result))
            Assert.Equal(200, objectResult.StatusCode);


            //    var note = new Note()
            //    {
            //        Title = "Third Note",
            //        PlainText = "Text in the third Note",
            //        PinnedStatus = true,

            //        Labels = new List<Label>
            //        {
            //            new Label{LabelText="label 1 in third Note"},
            //            new Label{LabelText="label 2 in third Note"}
            //        },
            //        CheckList = new List<CheckListItem>
            //        {
            //            new CheckListItem{CheckListText="checklist 1 in third Note"},
            //            new CheckListItem {CheckListText="checklist 2 in third Note"}

            //        }
            //    };
            //    var _controller = GetController();
            //    var result = _controller.Post(note);
            //    var objectresult = result.IsCompleted;
            //    Assert.True(objectresult);
        }


        [Fact]
        public async void TestDeleteNoteById()
        {
            // Arrange
            Mock<IKeepService> mockRepo = new Mock<IKeepService>();
            MockDB mockDbHelper = new MockDB();
            mockRepo.Setup(service => service.DeleteNote(1)).Returns(mockDbHelper.GetTestResultData());
            NotesController controller = new NotesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1);
            OkObjectResult objectResult = result as OkObjectResult;
            Note objectResultValue = objectResult.Value as Note;
            // Assert
            Assert.Equal(200, objectResult.StatusCode);

            //    var _controller = GetController();
            //    var result = _controller.Delete(1);

            //    Assert.True(result.IsCompletedSuccessfully);
            //    //result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


}
}

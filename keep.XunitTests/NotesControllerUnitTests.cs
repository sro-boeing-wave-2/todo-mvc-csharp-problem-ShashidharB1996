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
        private readonly NotesController _controller;
        public NotesControllerUnitTests()
        {
            //var OptionBuilder = new DbContextOptionsBuilder<keepContext>();
            //OptionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var OptionBuilder = new DbContextOptionsBuilder<keepContext>();
            OptionBuilder.UseInMemoryDatabase("TestDB");
            keepContext kContext = new keepContext(OptionBuilder.Options);
            _controller = new NotesController(kContext);
            CreateData(kContext);
        }


        public async void CreateData(keepContext kContext)
        {
            var Notes = new List<Note>
            {new Note()
            {
                Title = "First Note",
                PlainText = "Text in the first Note",
                PinnedStatus = true,
                
                Label = new List<Label>
                {
                    new Label{LabelText="label 1 in first Note"},
                    new Label{LabelText="label 2 in first Note"}
                },
                ChkList=new List<CheckList>
                {
                    new CheckList{CheckListText="checklist 1 in first Note"},
                    new CheckList {CheckListText="checklist 2 in first Note"}

                }
            },
            new Note()
            {
                Title = "Second Note",
                PlainText = "Text in the second Note",
                PinnedStatus = true,

                Label = new List<Label>
                {
                    new Label{LabelText="label 1 in second NOte"},
                    new Label{LabelText="label 2 in second NOte"}
                },
                ChkList=new List<CheckList>
                {
                    new CheckList{CheckListText="checklist 1 in second NOte"},
                    new CheckList {CheckListText="checklist 2 in second NOte"}

                }
            }
            };
            kContext.Note.AddRange(Notes);
            await kContext.SaveChangesAsync();
        }

        [Fact]
        public async void TestGetAllNotes()
        {
            var result = await _controller.GetNote();
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as List<Note>;
            Assert.Equal(2, notes.Count());
        }

        [Fact]
        public async void TestGetNoteById()
        {
            var result = await _controller.GetNoteById(1);

            // Console.Write(result.Result);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as Note;
            Assert.Equal("First Note", notes.Title);
        }

        [Fact]
        public void TestPost()
        {
            var note = new Note()
            {
                Title = "Third Note",
                PlainText = "Text in the third Note",
                PinnedStatus = true,

                Label = new List<Label>
                {
                    new Label{LabelText="label 1 in third Note"},
                    new Label{LabelText="label 2 in third Note"}
                },
                ChkList = new List<CheckList>
                {
                    new CheckList{CheckListText="checklist 1 in third Note"},
                    new CheckList {CheckListText="checklist 2 in third Note"}

                }
            };
            var result = _controller.Post(note);
            var objectresult = result.IsCompleted;

            Assert.True(objectresult);
        }

        [Fact]
        public async void TestPutById()
        {
            var note = new Note()
            {
                ID = 2,
                Title = "SECOND NOTE",
                PlainText = "Text in the second Note",
                PinnedStatus = true,
                

                Label = new List<Label>
                {
                    new Label{LabelText="label 1 in second NOte"},
                    new Label{LabelText="label 2 in second NOte"}
                },
                ChkList = new List<CheckList>
                {
                    new CheckList{CheckListText="checklist 1 in second NOte"},
                    new CheckList {CheckListText="checklist 2 in second NOte"}

                }
            };
            var result = await _controller.PutById(2, note);
            //var objectresult = result as OkObjectResult;
            //var notes = objectresult.Value as Note;
            //Assert.Equal("SECOND NOTE", notes.Title);

            var objectResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            //var n = okResult.Value.Should().BeAssignableTo<Note>().Subject;
        }


        [Fact]
        public async void TestDeleteNoteById()
        {
            var result = await _controller.Delete(3);

            //Console.Write(result.Result);

            var objectResult = result.Should().BeOfType<OkResult>().Subject;
            //var notes = objectresult.Value as Note;
            //Assert.Equal("First Note", notes.Title);
        }


        //[Fact]
        //public async void TestDeleteAllNotes()
        //{
        //    var result = await _controller.DeleteAll();

        //    //Console.Write(result.Result);

        //    var objectResult = result.Should().BeOfType<OkResult>().Subject;
        //    //var notes = objectresult.Value as Note;
        //    //Assert.Equal("First Note", notes.Title);
        //}



        //[Fact]
        //public async Task Notes_Get_All()
        //{
        //    // Arrange
        //    var controller = new NotesController(new KeepService());

        //    // Act
        //    var result = await controller.GetNote();

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var notes = okResult.Value.Should().BeAssignableTo<IEnumerable<Note>>().Subject;

        //    notes.Count().Should().Be(50);
        //}

        //[Fact]
        //public async Task Notes_Get_From_Moq()
        //{
        //    // Arrange
        //    var serviceMock = new Mock<IKeepService>();
        //    IEnumerable<Note> notes = new List<Note>
        //    {
        //        new Note{ID=1, Title="Foo", PlainText="Bar"},
        //        new Note{ID=2, Title="John", PlainText="Doe"},
        //        new Note{ID=3, Title="Juergen", PlainText="Gutsch"},
        //    };
        //    serviceMock.Setup(x => x.GetAllItems()).Returns(() => Task.FromResult(notes));
        //    var controller = new NotesController(serviceMock.Object);

        //    // Act
        //    var result = await controller.GetNote();

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var actual = okResult.Value.Should().BeAssignableTo<IEnumerable<Note>>().Subject;

        //    notes.Count().Should().Be(3);
        //}

        //[Fact]
        //public async Task Notes_Get_Specific()
        //{
        //    // Arrange
        //    var controller = new NotesController(new KeepService());

        //    // Act
        //    var result = await controller.GetNoteById(16);

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var note = okResult.Value.Should().BeAssignableTo<Note>().Subject;
        //    note.ID.Should().Be(16);
        //}

        //[Fact]
        //public async Task Notes_Add()
        //{
        //    // Arrange
        //    var controller = new NotesController(new KeepService());
        //    var newNote = new Note
        //    {
        //        PlainText = "John",
        //        Title = "FooBar",
        //        PinnedStatus = true

        //    };

        //    // Act
        //    var result = await controller.Post(newNote);

        //    // Assert
        //    var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        //    var note = okResult.Value.Should().BeAssignableTo<Note>().Subject;
        //    note.ID.Should().Be(51);
        //}

        //[Fact]
        //public async Task Notes_Change()
        //{
        //    // Arrange
        //    var service = new KeepService();
        //    var controller = new NotesController(service);
        //    var newNote = new Note
        //    {
        //        PlainText = "irony",
        //        Title = "Status",
        //        PinnedStatus = false

        //    };

        //    // Act
        //    var result = await controller.PutById(2, newNote);

        //    // Assert
        //    var NoContentResult = result.Should().BeOfType<NotFoundResult>().Subject;

        //    var note = await service.GetById(2);
        //    note.ID.Should().Be(2);
        //    note.PlainText.Should().Be("irony");
        //    note.Title.Should().Be("Status");
        //    note.PinnedStatus.Should().Be(false);

        //}

        //[Fact]
        //public async Task Notes_Delete()
        //{
        //    // Arrange
        //    var service = new KeepService();
        //    var controller = new NotesController(service);

        //    // Act
        //    var result = await controller.Delete(20);

        //    // Assert
        //    var NoContentResult = result.Should().BeOfType<OkResult>().Subject;
        //    // should throw an exception, 
        //    // because the person with id==20 doesn't exist enymore
        //    //AssertionExtensions.ShouldThrow<InvalidOperationException>(
        //      //   () => service.Get(20));
        //}

        //[Fact]
        //public async Task Notes_Delete_Fail()
        //{
        //    // Arrange
        //    var service = new KeepService();
        //    var controller = new NotesController(service);

        //    // Act
        //    var result = await controller.Delete(20);

        //    // Assert
        //    var NoContentResult = result.Should().BeOfType<OkResult>().Subject;
        //    // should throw an eception, 
        //    // because the person with id==20 doesn't exist enymore
        //    //AssertionExtensions.ShouldThrow<InvalidOperationException>(
        //    //    () => service.Get(15));
        //}



    }
}

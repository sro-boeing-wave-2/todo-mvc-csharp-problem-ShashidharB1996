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
        //private readonly NotesController _controller;
        public NotesController GetController()
        {
            var OptionBuilder = new DbContextOptionsBuilder<keepContext>();
            OptionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            //var OptionBuilder = new DbContextOptionsBuilder<keepContext>();
            //OptionBuilder.UseInMemoryDatabase("TestDB");
            keepContext kContext = new keepContext(OptionBuilder.Options);
            //_controller = new NotesController(kContext);
            CreateData(OptionBuilder.Options);
            return new NotesController(kContext);
        }



        public void CreateData(DbContextOptions<keepContext> options)
        {
            using (var kContext = new keepContext(options))
            {
                var Notes = new List<Note>
            {new Note()
            {
                ID = 1,
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
                ID = 2,
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
                var countOfEntitiesBeingTracked = kContext.ChangeTracker.Entries().Count();
                kContext.SaveChanges();
            }
        }




        [Fact]
        public async void TestGetNoteById()
        {
            var _controller = GetController();
            var result = await _controller.GetNoteById(1);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as Note;
            Assert.Equal(1, notes.ID);
        }

        [Fact]
        public async void TestGetAllNotes()
        {
            var _controller = GetController();
            var result = await _controller.GetNote();
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as List<Note>;
            Assert.Equal(2, notes.Count());
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
            var _controller = GetController();
            var result = await _controller.PutById(2, note);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as Note;
            Assert.Equal(2, notes.ID);
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
            var _controller = GetController();
            var result = _controller.Post(note);
            var objectresult = result.IsCompleted;
            Assert.True(objectresult);
        }




        [Fact]
        public void TestDeleteNoteById()
        {
            //var result = await _controller.Delete(3);

            ////Console.Write(result.Result);

            //var objectResult = result.Should().BeOfType<OkResult>().Subject;
            ////var notes = objectresult.Value as Note;
            ////Assert.Equal("First Note", notes.Title);

            var _controller = GetController();
            var result = _controller.Delete(1);
            Assert.True(result.IsCompletedSuccessfully);
        }





    }
}

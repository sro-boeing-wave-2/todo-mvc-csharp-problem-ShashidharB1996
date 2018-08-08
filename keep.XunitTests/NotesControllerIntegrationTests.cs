using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Net.Http.Headers;
using keep;
using keep.Models;


namespace keep.XunitTests
{
    public class NotesControllerIntegrationTests
    {
        //private readonly TestServer _server;
        private readonly HttpClient _client;

        private readonly keepContext _context;

        public NotesControllerIntegrationTests()
        {
            // Arrange
            var host = new TestServer(new WebHostBuilder().UseEnvironment("Testing")
                .UseStartup<Startup>());
            _context = host.Host.Services.GetService(typeof(keepContext)) as keepContext;

            _client = host.CreateClient();

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
            _context.Note.AddRange(Notes);
            _context.SaveChanges();
        }

        //_client.DefaultRequestHeaders.Accept.Clear();
        //_client.DefaultRequestHeaders.Accept.Add(
        //   new MediaTypeWithQualityHeaderValue("application/json"));





        [Fact]
        public async Task IntegrationTestGetAllNotes()
        {
            //Act
            var response = await _client.GetAsync("/api/Notes");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var notes = JsonConvert.DeserializeObject<List<Note>>(responseString);


            notes.Count().Should().Be(2);
            Console.WriteLine("GetAllNotes" + notes.Count);
            //Console.WriteLine(notes);
        }


        [Fact]
        public async Task IntegrationTestPostNote()
        {
            // Arrange
            var put = new Note
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
            var content = JsonConvert.SerializeObject(put);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Notes", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<Note>(responseString);
            notes.ID.Should().Be(3);
            Console.WriteLine("PostNoteID" + notes.ID);

            //_context.Note.Add(put);
            //_context.SaveChanges();
        }

        [Fact]
        public async Task IntegrationTestPostNoteAgain()
        {
            // Arrange
            var put = new Note
            {

                Title = "Fourth Note",
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
            var content = JsonConvert.SerializeObject(put);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Notes", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<Note>(responseString);
            notes.ID.Should().Be(3);
            Console.WriteLine("PostNoteID" + notes.ID);

            //_context.Note.Add(put);
            //_context.SaveChanges();
        }


        [Fact]
        public async Task IntegrationTestGetSpecificNote()
        {
            // Act
            var response = await _client.GetAsync("/api/Notes/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<Note>(responseString);
            notes.ID.Should().Be(1);
            Console.WriteLine("GetNoteByID" + notes.ID);

            //_context.SaveChanges();

        }

        [Fact]
        public async Task PostNoteSpecificInvalid()
        {
            // Arrange
            var noteToAdd = new Note { PlainText = "John" };
            var content = JsonConvert.SerializeObject(noteToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Notes", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            //var responseString = await response.Content.ReadAsStringAsync();
            //responseString.Should().Contain("The Label and ChkList field is required");
            //  .And.Contain("The LastName field is required")
            //  .And.Contain("The Phone field is required");
        }



        [Fact]
        public async Task PutNoteSpecficInvalid()
        {
            // Arrange
            var noteToChange = new Note { PlainText = "John" };
            var content = JsonConvert.SerializeObject(noteToChange);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Notes/16", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            //var responseString = await response.Content.ReadAsStringAsync();
            //responseString.Should().Contain("The Label and ChkList field is required")
            //    .And.Contain("The Label field is required")
            //    .And.Contain("The ChkList field is required");
        }

        [Fact]
        public async Task DeleteNoteSpecific()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync("/api/Notes/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }




        [Fact]
        public async Task IntegrationPutNoteSpecific()
        {
            // Arrange
            var note = new Note
            {
                ID = 1,
                Title = "THIRD THIRD THIRD Note",
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
            var content = JsonConvert.SerializeObject(note);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Notes/1", stringContent);

            //_context.Note.Update(note);
            //_context.SaveChanges();

            // Assert
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            //responseString.Should().Be(String.Empty);
        }
        [Fact]
        public async Task IntegrationTestDeleteAllNotes()
        {

        }



    }
}


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

        public NotesControllerIntegrationTests()
        {
            // Arrange
            var host = new TestServer(new WebHostBuilder().UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = host.CreateClient();
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(
             //   new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task IntegrationTestGetAllNotes()
        {
            //Act
           var response = await _client.GetAsync("/api/Notes");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<Note>>(responseString);
            notes.Count().Should().Be(0);
            Console.WriteLine(notes.Count);
        }

        [Fact]
        public async Task IntegrationTestPost()
        {
            // Arrange
            var NoteToAdd = new Note
            {
                Title = "First Note",
                PlainText = "Text in the first Note",
                PinnedStatus = true,

                Label = new List<Label>
                {
                    new Label{LabelText="label 1 in first Note"},
                    new Label{LabelText="label 2 in first Note"}
                },
                ChkList = new List<CheckList>
                {
                    new CheckList{CheckListText="checklist 1 in first Note"},
                    new CheckList {CheckListText="checklist 2 in first Note"}

                }
            };
            var content = JsonConvert.SerializeObject(NoteToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Notes", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<Note>(responseString);
            note.ID.Should().Be(1);
            Console.WriteLine(note.ID);
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
            //notes.ID.Should().Be(1);
            Console.WriteLine(notes.ID);

        }

        //        [Fact]
        //        public async Task Notes_Post_Specific_Invalid()
        //        {
        //            // Arrange
        //            var noteToAdd = new Note { PlainText = "John" };
        //            var content = JsonConvert.SerializeObject(noteToAdd);
        //            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //            // Act
        //            var response = await _client.PostAsync("/api/Notes", stringContent);

        //            // Assert
        //            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            responseString.Should().Contain("The Label and ChkList field is required");
        //              //  .And.Contain("The LastName field is required")
        //              //  .And.Contain("The Phone field is required");
        //        }

        //        [Fact]
        //        public async Task Notes_Put_Specific()
        //        {
        //            // Arrange
        //            var noteToChange = new Note
        //            {
        //                ID = 16,
        //                PlainText = "John",
        //                PinnedStatus = true,
        //                Title = "Something",

        //            };
        //            var content = JsonConvert.SerializeObject(noteToChange);
        //            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //            // Act
        //            var response = await _client.PutAsync("/api/Notes/16", stringContent);

        //            // Assert
        //            response.EnsureSuccessStatusCode();
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            responseString.Should().Be(String.Empty);
        //        }

        //        [Fact]
        //        public async Task Notes_Put_Specific_Invalid()
        //        {
        //            // Arrange
        //            var noteToChange = new Note { PlainText = "John" };
        //            var content = JsonConvert.SerializeObject(noteToChange);
        //            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //            // Act
        //            var response = await _client.PutAsync("/api/Notes/16", stringContent);

        //            // Assert
        //            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            responseString.Should().Contain("The Label and ChkList field is required")
        //                .And.Contain("The Label field is required")
        //                .And.Contain("The ChkList field is required");
        //        }

        //        [Fact]
        //        public async Task Notes_Delete_Specific()
        //        {
        //            // Arrange

        //            // Act
        //            var response = await _client.DeleteAsync("/api/Notes/16");

        //            // Assert
        //            response.EnsureSuccessStatusCode();
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            responseString.Should().Be(String.Empty);
        //        }
    }
}

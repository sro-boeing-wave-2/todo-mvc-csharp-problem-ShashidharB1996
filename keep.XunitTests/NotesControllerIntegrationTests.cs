//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Linq;
//using Xunit;
//using FluentAssertions;
//using System.Net.Http.Headers;
//using keep;
//using keep.Models;


//namespace keep.XunitTests
//{
//    public class NotesControllerIntegrationTests
//    {
//        //private readonly TestServer _server;
//        private readonly HttpClient _client;

//        private readonly keepContext _context;

//        public NotesControllerIntegrationTests()
//        {
//            Arrange
//           var host = new TestServer(new WebHostBuilder()
//               .UseEnvironment("Development")
//               .UseStartup<Startup>());
//            _context = host.Host.Services.GetService(typeof(keepContext)) as keepContext;

//            _client = host.CreateClient();
//        }


//        [Fact]
//        public async Task IntegrationTestPostNote()
//        {
//            Arrange
//           var put = new Note
//           {
//               Title = "Third Note",
//               PlainText = "Text in the third Note",
//               PinnedStatus = false,

//               Labels = new List<Label>
//                       {
//                            new Label{LabelText="label 1 in third Note"},
//                            new Label{LabelText="label 2 in third Note"}
//                       },
//               CheckList = new List<CheckListItem>
//                       {
//                            new CheckListItem{CheckListText="checklist 1 in third Note"},
//                            new CheckListItem {CheckListText="checklist 2 in third Note"}

//                       }
//           };
//            var content = JsonConvert.SerializeObject(put);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            Act
//           var response = await _client.PostAsync("/api/Notes", stringContent);

//            Assert
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<Note>(responseString);
//            notes.ID.Should().Be(3);
//            notes.Title.Should().Be("Third Note");
//            notes.PlainText.Should().Be("Text in the third Note");
//            notes.PinnedStatus.Should().BeFalse();
//            notes.Labels[0].LabelText.Should().Be("label 1 in third Note");
//            notes.CheckList[0].CheckListText.Should().Be("checklist 1 in third Note");
//            notes.CheckList[1].CheckListText.Should().Be("checklist 2 in third Note");
//            Console.WriteLine("PostNoteID" + notes.ID);

//            _context.Note.Add(put);
//            _context.SaveChanges();
//        }







//        [Fact]
//        public async Task IntegrationTestGetAllNotes()
//        {
//            //Act
//            var response = await _client.GetAsync("/api/Notes");

//            //Assert
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();

//            var notes = JsonConvert.DeserializeObject<List<Note>>(responseString);


//            notes.Count().Should().Be(2);
//            Console.WriteLine("GetAllNotes" + notes.Count);
//            //Console.WriteLine(notes);
//        }





//        [Fact]
//        public async Task IntegrationTestGetSpecificNote()
//        {
//            // Act
//            var response = await _client.GetAsync("/api/Notes/1");

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<Note>(responseString);
//            notes.ID.Should().Be(1);
//            Console.WriteLine("GetNoteByID" + notes.ID);

//            //_context.SaveChanges();

//        }


//        //[Fact]
//        //public async Task IntegrationTestGetSpecificNoteByLabel()
//        //{
//        //    // Act
//        //    var response = await _client.GetAsync("/api/Notes/label/label 1 in second NOte");

//        //    // Assert
//        //    response.EnsureSuccessStatusCode();
//        //    var responseString = await response.Content.ReadAsStringAsync();

//        //    var notes = JsonConvert.DeserializeObject<List<Note>>(responseString);


//        //    notes.Count().Should().Be(2);

//        //    //_context.SaveChanges();

//        //}


//        //[Fact]
//        //public async Task IntegrationTestGetSpecificNoteByPin()
//        //{
//        //    // Act
//        //    var response = await _client.GetAsync("/api/Notes/Pin/true");

//        //    // Assert
//        //    response.EnsureSuccessStatusCode();
//        //    var responseString = await response.Content.ReadAsStringAsync();

//        //    var notes = JsonConvert.DeserializeObject<List<Note>>(responseString);


//        //    notes.Count().Should().Be(2);

//        //    //_context.SaveChanges();

//        //}






//        [Fact]
//        public async Task PostNoteSpecificInvalid()
//        {
//            // Arrange
//            var noteToAdd = new Note { PlainText = "John" };
//            var content = JsonConvert.SerializeObject(noteToAdd);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            // Act
//            var response = await _client.PostAsync("/api/Notes", stringContent);

//            // Assert
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

//        }



//        [Fact]
//        public async Task PutNoteSpecficInvalid()
//        {
//            // Arrange
//            var noteToChange = new Note { PlainText = "John" };
//            var content = JsonConvert.SerializeObject(noteToChange);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            // Act
//            var response = await _client.PutAsync("/api/Notes/16", stringContent);

//            // Assert
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

//        }

//        [Fact]
//        public async Task DeleteNoteSpecific()
//        {
//            // Arrange

//            // Act
//            var response = await _client.DeleteAsync("/api/Notes/1");

//            // Assert
//            response.EnsureSuccessStatusCode();

//        }




//        [Fact]
//        public async Task IntegrationPutNoteSpecific()
//        {
//            // Arrange
//            var note = new Note
//            {
//                ID = 1,
//                Title = "THIRD THIRD THIRD Note",
//                PlainText = "Text in the third Note",
//                PinnedStatus = true,

//                Labels = new List<Label>
//                        {
//                            new Label{LabelText="label 1 in third Note"},
//                            new Label{LabelText="label 2 in third Note"}
//                        },
//                CheckList = new List<CheckListItem>
//                        {
//                            new CheckListItem{CheckListText="checklist 1 in third Note"},
//                            new CheckListItem {CheckListText="checklist 2 in third Note"}

//                        }
//            };
//            var content = JsonConvert.SerializeObject(note);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            // Act
//            var response = await _client.PutAsync("/api/Notes/1", stringContent);

//            //_context.Note.Update(note);
//            //_context.SaveChanges();

//            // Assert
//            response.EnsureSuccessStatusCode();

//        }
//    }
//}


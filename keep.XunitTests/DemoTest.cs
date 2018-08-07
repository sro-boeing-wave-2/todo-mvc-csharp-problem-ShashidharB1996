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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace keep.XunitTests
{
    public class DemoTest
    {
        //private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private readonly keepContext _context;

        public DemoTest()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<keepContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=your_db_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);

            _context = new keepContext(builder.Options);
            _context.Database.Migrate();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnListOfObjectDtos()
        {
            // Arrange database data
            _context.Note.Add(new Note {PlainText = "PTF0001", Title = "Portfolio One" });
            _context.Note.Add(new Note {PlainText = "PTF0002", Title = "Portfolio Two" });

            // Act
            var response = await _client.GetAsync("/api/route");
            response.EnsureSuccessStatusCode();


            // Assert
            var result = Assert.IsType<OkResult>(response);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

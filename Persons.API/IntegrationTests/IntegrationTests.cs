
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Person.Api.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Program>();
            TestClient = appFactory.CreateClient();
        }
    }
}
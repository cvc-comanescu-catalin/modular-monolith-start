using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.Endpoints;
using Xunit;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookList(Fixture fixture, ITestOutputHelper outputHelper) : 
#pragma warning disable CS0618 // Type or member is obsolete
  TestClass<Fixture>(fixture, outputHelper)
#pragma warning restore CS0618 // Type or member is obsolete
{
  [Fact]
  public async Task ReturnsThreeBooksAsync()
  {
    var testResult = await Fixture.Client.GETAsync<List, ListBooksResponse>();

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count.Should().Be(3);
  }
}


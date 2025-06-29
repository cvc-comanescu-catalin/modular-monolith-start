using FastEndpoints.Testing;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

#pragma warning disable CS0618 // Type or member is obsolete
public class Fixture(IMessageSink messageSink) : TestFixture<Program>(messageSink)
#pragma warning restore CS0618 // Type or member is obsolete
{
  protected override Task SetupAsync()
  {
    Client = CreateClient();

    return Task.CompletedTask;
  }

  protected override Task TearDownAsync()
  {
    Client.Dispose();

    return base.TearDownAsync();
  }
}

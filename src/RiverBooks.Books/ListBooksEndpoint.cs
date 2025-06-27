using FastEndpoints;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(IBookService bookService) : 
    EndpointWithoutRequest<ListBooksResponse>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
        //Description(x => x.Produces<ListBooksResponse>());
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var books = _bookService.ListBooks();

        if (books == null || !books.Any())
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(new ListBooksResponse { Books = books }, cancellation: ct);
    }
}
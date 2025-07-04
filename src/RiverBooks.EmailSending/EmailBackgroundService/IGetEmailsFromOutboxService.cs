using Ardalis.Result;

namespace RiverBooks.EmailSending.EmailBackgroundService;

internal interface IGetEmailsFromOutboxService
{
  Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity();
}

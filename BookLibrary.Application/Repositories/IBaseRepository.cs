namespace BookLibrary.Application.Repositories;

public interface IBaseRepository
{
    Task SaveChangesAsync(CancellationToken token);
}
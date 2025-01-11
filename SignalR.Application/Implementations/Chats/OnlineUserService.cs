namespace SignalR.Application.Implementations.Chats;

public class OnlineUserService(IApplicationDbContext applicationDbContext,
    ILogger<OnlineUserService> logger
    , CancellationToken cancellationToken) : IOnlineUserService
{
    public async Task AddEdit(string? userId, bool isOnline)
    {
        try
        {
            if (!int.TryParse(userId, out var id))
                return;

            if (!isOnline)
            {
                //user already disconnected so cancellationToken will be cancelled
                cancellationToken = new CancellationToken();
            }

            var onlineEntity = await applicationDbContext
                .OnlineUsers
                .FirstOrDefaultAsync(a => a.UserId == id, cancellationToken);

            onlineEntity!.LastModified = DateTime.Now;
            applicationDbContext.Update(onlineEntity);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(AddEdit));
        }
    }
}
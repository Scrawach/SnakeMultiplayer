namespace Network.Services.RoomHandlers
{
    public interface INetworkStatusProvider
    {
        string SessionId { get; }
        bool IsPlayer(string sessionId);
    }
}
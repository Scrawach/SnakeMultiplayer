namespace Network.Services
{
    public interface INetworkStatusProvider
    {
        string SessionId { get; }
        bool IsPlayer(string sessionId);
    }
}
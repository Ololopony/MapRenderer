using System.Threading.Tasks;

public interface IAiApiConnector
{
    public Task ConnectToAi();
    public Task<string> GetResponce(string prompt);
}

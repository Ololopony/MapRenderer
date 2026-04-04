using System.Text.Json;

public class AiReturnHandler
{
    public string HandleReturn(string aiReturn)
    {
        try
        {
            JsonDocument.Parse(aiReturn);
            return aiReturn;
        }
        catch (JsonException)
        {
            return string.Empty;
        }
    }
}

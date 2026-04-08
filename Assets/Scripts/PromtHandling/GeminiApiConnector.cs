using UnityEngine;
using System.Threading.Tasks;

public class GeminiApiConnector : IAiApiConnector
{
    private Google.GenAI.Client _client;
    private AiReturnHandler _aiReturnHandler = new AiReturnHandler();

    public async Task ConnectToAi()
    {
        _client = new Google.GenAI.Client(null, "123");
    }

    public async Task<string> GetResponce(string prompt)
    {
        if (!prompt.Equals(string.Empty))
        {
            try
            {
                var response = await _client.Models.GenerateContentAsync(
                    model: "gemini-2.5-flash-lite", contents: prompt
                );

                return _aiReturnHandler.HandleReturn(response.Candidates[0].Content.Parts[0].Text);
            }
            catch (Google.GenAI.ClientError ex)
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }
}

using UnityEngine;
using System.Threading.Tasks;

public class GeminiApiConnector : IAiApiConnector
{
    private Google.GenAI.Client _client;
    private AiReturnHandler _aiReturnHandler = new AiReturnHandler();

    public async Task ConnectToAi()
    {
        _client = new Google.GenAI.Client(null, "123");
        Debug.Log("Connected to Gemini API");
    }

    public async Task<string> GetResponce(string prompt)
    {
        try
        {
            Debug.Log("Sending prompt to Gemini API: " + prompt);
            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash-lite", contents: prompt
            );

            Debug.Log(response.PromptFeedback);
            Debug.Log(response.ModelStatus);

            Debug.Log(response.Candidates[0].Content.Parts[0].Text);
            return _aiReturnHandler.HandleReturn(response.Candidates[0].Content.Parts[0].Text);
        }
        catch (Google.GenAI.ClientError ex)
        {
            Debug.LogError("Error while connecting to Gemini API: " + ex.Message);
            return string.Empty;
        }
    }
}

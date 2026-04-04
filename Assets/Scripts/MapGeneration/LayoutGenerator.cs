using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MapLayoutGenerator;
using UnityEngine;
using UnityEngine.Rendering;

public class LayoutGenerator
{
    private Layout _layout;
    private LayoutFiller _layoutFiller;
    private JSONToCellTypeDictionaryDeserialiser _jSONToCellTypeDictionaryDeserialiser = new JSONToCellTypeDictionaryDeserialiser();
    private Dictionary<string, int> _typesStringDictionary = new Dictionary<string, int>();
    private Dictionary<CellType, int> _typesDictionary = new Dictionary<CellType, int>();
    private string _jsonFilePath = "E:/MapRendering/MapRenderer/Assets/Scripts/JSON/generateSettings.json"; 
    private string _jsonString = "";
    private string _additionalInfoToPromptFilePath = "E:/MapRendering/MapRenderer/Assets/Scripts/JSON/additionalInfoToPrompt.txt";
    private string _additionalInfoToPrompt = "";
    private GeminiApiConnector _geminiApiConnector = new GeminiApiConnector();
    private bool _connectetToAiApi;

    private async Task ConnectToGemini()
    {
        await _geminiApiConnector.ConnectToAi();
        _connectetToAiApi = true;
    }

    private async Task<string> GetJsonFromGemini(string prompt)
    {
        return await _geminiApiConnector.GetResponce(prompt);
    }

    public async Task InitiateGenerator(string prompt)
    {
        await ConnectToGemini();
        if (_connectetToAiApi)
        {
            _jsonString = await GetJsonFromGemini(prompt);

            if (_jsonString.Equals(string.Empty))
            {
                _jsonString = File.ReadAllText(_jsonFilePath);
            }
        }
        else
        {
            _jsonString = File.ReadAllText(_jsonFilePath);
        }
        _additionalInfoToPrompt = File.ReadAllText(_additionalInfoToPromptFilePath);
        _jsonFilePath += " " + _additionalInfoToPrompt;
        _layout = new Layout(3, 3);
        _typesStringDictionary = _jSONToCellTypeDictionaryDeserialiser.DeserialiseJSONRulesToCellTypeDictionary(_jsonString);
        CellTypeConnectionRules cellTypeConnectionRules;

        foreach (var type in _typesStringDictionary)
        {
            switch (type.Key)
            {
                case "mountains":
                    cellTypeConnectionRules = Resources.Load<CellTypeConnectionRules>("ScriptableObjects/CellTypeConnectionRules/MountainConnectionRules");
                    _typesDictionary.Add(new MountainType(cellTypeConnectionRules), type.Value);
                    continue;
                case "forests":
                    cellTypeConnectionRules = Resources.Load<CellTypeConnectionRules>("ScriptableObjects/CellTypeConnectionRules/ForestConnectionRules");
                    _typesDictionary.Add(new ForestType(cellTypeConnectionRules), type.Value);
                    continue;
                case "lakes":
                    cellTypeConnectionRules = Resources.Load<CellTypeConnectionRules>("ScriptableObjects/CellTypeConnectionRules/LakeConnectionRules");
                    _typesDictionary.Add(new LakeType(cellTypeConnectionRules), type.Value);
                    continue;
            }
        }
        
        _layoutFiller = new LayoutFiller(_layout, _typesDictionary);
    }

    public void GenerateLayout()
    {
        _layoutFiller.FillLayoutWithEmptyCells();
        _layoutFiller.AssignNewNeighbourCells();
        _layoutFiller.AssignTypesToCell();
    }

    public Layout GetLayout()
    {
        return _layout;
    }
}

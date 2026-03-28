using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MapLayoutGenerator;
using UnityEngine;

public class LayoutGenerator : MonoBehaviour
{
    private Layout _layout;
    private LayoutFiller _layoutFiller;
    private JSONToCellTypeDictionaryDeserialiser _jSONToCellTypeDictionaryDeserialiser = new JSONToCellTypeDictionaryDeserialiser();
    private Dictionary<string, int> _typesStringDictionary = new Dictionary<string, int>();
    private Dictionary<CellType, int> _typesDictionary = new Dictionary<CellType, int>();
    private string _jsonFilePath = "E:/MapRendering/MapRenderer/Assets/Scripts/JSON/generateSettings.json"; 
    private string _jsonString = "";


    private void Awake()
    {
        _jsonString = File.ReadAllText(_jsonFilePath);
        _layout = new Layout(10, 10);
        _typesStringDictionary = _jSONToCellTypeDictionaryDeserialiser.DeserialiseJSONRulesToCellTypeDictionary(_jsonString);

        foreach (var type in _typesStringDictionary)
        {
            switch (type.Key)
            {
                case "mountains":
                    _typesDictionary.Add(new MountainType(), type.Value);
                    continue;
                case "forests":
                    _typesDictionary.Add(new ForestType(), type.Value);
                    continue;
                case "lakes":
                    _typesDictionary.Add(new LakeType(), type.Value);
                    continue;
            }
        }
        
        _layoutFiller = new LayoutFiller(_layout, _typesDictionary);
    }
}

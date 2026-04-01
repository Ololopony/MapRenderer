using System.Collections.Generic;
using System.IO;
using MapLayoutGenerator;
using UnityEngine;

public class LayoutGenerator
{
    private Layout _layout;
    private LayoutFiller _layoutFiller;
    private JSONToCellTypeDictionaryDeserialiser _jSONToCellTypeDictionaryDeserialiser = new JSONToCellTypeDictionaryDeserialiser();
    private Dictionary<string, int> _typesStringDictionary = new Dictionary<string, int>();
    private Dictionary<CellType, int> _typesDictionary = new Dictionary<CellType, int>();
    private string _jsonFilePath = "E:/MapRendering/MapRenderer/Assets/Scripts/JSON/generateSettings.json"; 
    private string _jsonString = "";


    public void InitiateGenerator()
    {
        _jsonString = File.ReadAllText(_jsonFilePath);
        _layout = new Layout(10, 10);
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

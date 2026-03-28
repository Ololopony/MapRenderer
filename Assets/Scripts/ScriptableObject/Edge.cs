using System.Security.Cryptography;
using MapLayoutGenerator;
using UnityEngine;

public class Edge : ScriptableObject
{
    [SerializeField]
    private RelativeDirection _direction;
    [SerializeField]
    private string _name;

    public RelativeDirection GetDirection()
    {
        return _direction;
    }

    public string GetName()
    {
        return _name;
    }
}

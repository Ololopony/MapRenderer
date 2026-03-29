using MapLayoutGenerator;
using UnityEngine;

[CreateAssetMenu (fileName = "Edge", menuName = "Edge")]
public class Edge : ScriptableObject
{
    [SerializeField]
    private RelativeDirection _direction;
    [SerializeField]
    private EdgeTypes _edgeType;

    public RelativeDirection GetDirection()
    {
        return _direction;
    }

    public EdgeTypes GetEnumEdgeType()
    {
        return _edgeType;
    }
}

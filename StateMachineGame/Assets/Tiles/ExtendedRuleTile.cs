using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Extended Rule Tile", menuName = "2D/Tiles/Extended Rule Tile")]
public class ExtendedRuleTile : RuleTile
{

    public string thisType;
    public string tile;
    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        ExtendedRuleTile otherTile = other as ExtendedRuleTile;

        if (otherTile == null)
            return base.RuleMatch(neighbor, other);

        switch (neighbor)
        {
            case TilingRule.Neighbor.This: 
                return thisType == otherTile.thisType;

            case TilingRule.Neighbor.NotThis:
                return thisType != otherTile.thisType;
        }
        return true;

    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
    {
        //return base.StartUp(position, tilemap, instantiatedGameObject);
        if (instantiatedGameObject != null)
        {
            instantiatedGameObject.transform.position = (Vector3)position / 2f;
        }
        return true;
    }
}
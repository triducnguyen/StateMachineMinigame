using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileDictionary : Singleton<TileDictionary>
{
    public List<ExtendedRuleTile> tileList;

    public Dictionary<float, List<Vector2>> radius2lattice = new Dictionary<float, List<Vector2>>();

    public List<RadiusLattice> lattices = new List<RadiusLattice>();
    

    public Dictionary<string, ExtendedRuleTile> tiles
    {
        get
        {
            return tileList.ToDictionary(x => x.tile);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        //add common lengths to reduce computation during play
        for (var i = 1; i<11; i++)
        {
            //calculate lattice points for a circle of radius i
            CalculateLattices(i);
        }

        foreach (var key in radius2lattice.Keys)
        {
            var val = radius2lattice[key];
            lattices.Add(new RadiusLattice() { r = key, lattices = val.ToArray() });
        }
    }

    //returns lattice points that fit
    public void CalculateLattices(float r)
    {
        //only search one quadrant

        List<Vector2> lattices = new List<Vector2>();
        //search a quadrant for lattice points that lie on or within a radius
        for (var i = Mathf.RoundToInt(-r); i<0; i++)
        {
            for (var k = Mathf.RoundToInt(-r); k<0; k++)
            {
                //check if sum of squares is lessthan or equal to the radius squared
                if (Mathf.Pow(i,2)+Mathf.Pow(k,2) <= Mathf.Pow(r,2))
                {
                    //this lattice is within our circle, add it
                    lattices.Add(new Vector2(i, k));
                }
            }
        }
        //flip found lattices
        foreach (var lattice in lattices.ToArray())
        {
            //flip x
            lattices.Add(new Vector2(-lattice.x, lattice.y));
            //flip y
            lattices.Add(new Vector2(lattice.x, -lattice.y));
            //flip x and y
            lattices.Add(new Vector2(-lattice.x, -lattice.y));
        }
        lattices.Add(Vector2.zero);

        // add all latice points to dictionary
        radius2lattice[r] = lattices;
    }
}

[System.Serializable]
public struct RadiusLattice
{
    public float r;
    public Vector2[] lattices;
}
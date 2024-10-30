using System.Collections.Generic;
using UnityEngine;

public class MovementRangeCalculator
{
    private MovementPattern movementPattern;

    public MovementRangeCalculator(MovementPattern pattern)
    {
        this.movementPattern = pattern;
    }

    public List<GameObject> CalculateRange(GameObject unit, int range)
    {
        return movementPattern.GetValidCubes(unit, range);
    }
}
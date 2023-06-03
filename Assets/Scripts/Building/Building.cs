using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableOdject
{

    [SerializeField] private int _price;
    [SerializeField] private int _xSixe = 3;
    [SerializeField] private int _zSize = 3;

    private void OnDrawGizmos()
    {
        float cellSize = FindObjectOfType<BuildingPlacer>().CellSize;

        for (int x = 0; x < _xSixe; x++)
        {
            for (int z = 0; z < _zSize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x,0,z) * cellSize, new Vector3(1, 0f, 1) * cellSize);
            }
        }
        
    }
}

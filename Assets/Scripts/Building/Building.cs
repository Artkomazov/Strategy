using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableOdject
{
    private Color _startColor;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private int _xSize = 3;
    [SerializeField] private int _zSize = 3;
    [SerializeField] private int _price = 1;

    public int XSize { get { return _xSize; } set { _xSize = value; } }
    public int ZSize { get { return _zSize; } set { _zSize = value; } }
    public int Price { get { return _price; } set { _price = value; } }

    private void Awake()
    {
        _startColor = _renderer.material.color;
    }

    private void OnDrawGizmos()
    {
        float cellSize = FindObjectOfType<BuildingPlacer>().CellSize;

        for (int x = 0; x < _xSize; x++)
        {
            for (int z = 0; z < _zSize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x, 0, z) * cellSize, new Vector3(1, 0f, 1) * cellSize);
            }
        }

    }

    public void DisplayUnacceptablePosition()
    {
        _renderer.material.color = Color.red;
    }
    public void DisplayAcceptablePosition()
    {
        _renderer.material.color = _startColor;
    }
}

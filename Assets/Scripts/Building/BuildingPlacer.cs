using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1f;

    [SerializeField] private Camera _ñamera;
    [SerializeField] private Building _currentBuilding;
    private Plane _plane;

    private Dictionary<Vector2Int, Building> _buildingDictionary = new();

    void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
    void Update()
    {
        if (_currentBuilding == null)
        {
            return;
        }

        Ray ray = _ñamera.ScreenPointToRay(Input.mousePosition);
        _plane.Raycast(ray, out float distanse);
        Vector3 point = ray.GetPoint(distanse) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        _currentBuilding.transform.position = new Vector3(x,0,z) * CellSize;
        if (CheckAllow(x, z, _currentBuilding))
        {
            _currentBuilding.DisplayAcceptablePosition();
            if (Input.GetMouseButtonDown(0))
            {
                InstalBuilding(x, z, _currentBuilding);
                _currentBuilding = null;
            }
        }
        else
        {
            _currentBuilding.DisplayUnacceptablePosition();
        }
    }

    public bool CheckAllow(int xPosition, int zPosition, Building building)
    {
        for (int x = 0; x < building.XSize; x++)
        {
            for (int z = 0; z < building.ZSize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                if (_buildingDictionary.ContainsKey(coordinate))
                {
                    return false;
                }
                
            }
        }

        return true;
    }

    public void InstalBuilding(int xPosition, int zPosition, Building building)
    {
        for (int x = 0; x < building.XSize; x++)
        {
            for (int z = 0; z < building.ZSize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                _buildingDictionary.Add(coordinate, _currentBuilding);
            }
        }
    }

    public void CreateBuilding(GameObject buildingPrefab)
    {
        GameObject newBuilding = Instantiate(buildingPrefab);
        _currentBuilding = newBuilding.GetComponent<Building>();
    }
}

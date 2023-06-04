using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1f;

    [SerializeField] private Camera _�amera;
    [SerializeField] private Building _currentBuilding;
    private Plane _plane;


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

        Ray ray = _�amera.ScreenPointToRay(Input.mousePosition);
        float distanse;
        _plane.Raycast(ray, out distanse);
        Vector3 point = ray.GetPoint(distanse) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        _currentBuilding.transform.position = new Vector3(x,0,z) * CellSize;

        if (Input.GetMouseButtonDown(0))
        {
            _currentBuilding = null;
        }
    }
}

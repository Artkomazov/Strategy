using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private BuildingPlacer _buildingPlacer;
    [SerializeField] private GameObject _buildingPrefab;

    public void TryBye()
    {
        int price = _buildingPrefab.GetComponent<Building>().Price;
        if (FindObjectOfType<Resources>().Money >= price)
        {
            FindObjectOfType<Resources>().Money -= price;
            _buildingPlacer.CreateBuilding(_buildingPrefab);
        }
        else
        {
            Debug.Log("Мало денег");
        }
    }
}

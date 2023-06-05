using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Building
{
    [SerializeField] private Transform _spawn;

    public void CreateUnit(GameObject unitPrefab)
    {
        Instantiate(unitPrefab, _spawn.position, Quaternion.identity);
    }
}

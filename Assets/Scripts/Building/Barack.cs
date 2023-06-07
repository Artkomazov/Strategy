using UnityEngine;

public class Barack : Building
{
    [SerializeField] private Transform _spawn;

    [System.Obsolete]
    public void CreateUnit(GameObject unitPrefab)
    {
        GameObject newUnit = Instantiate(unitPrefab, _spawn.position, Quaternion.identity);
        Vector3 posposition = _spawn.position + new Vector3(Random.Range(-1.5f, 1.5f), 0f, Random.Range(-1.5f, 1.5f));
        newUnit.GetComponent<Unit>().WhenClickOnGround(posposition);
    }
}

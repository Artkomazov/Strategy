using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    WalkToBuilding,
    WalkToUnit,
    Attack
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _currentEnemyState;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Building _targetBuilding;
    [SerializeField] private Unit _targetUnit;

    [SerializeField] private int _health;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _distanceToFollow = 7f;
    [SerializeField] private float _distanceToAttack = 1f;
    [SerializeField] private float _attackPeriod = 1f;
    private float _timer;

    private void Start()
    {
        SetState(EnemyState.WalkToBuilding);
    }

    private void Update()
    {
        if (_currentEnemyState == EnemyState.Idle)
        {
            FindClosestUnit();
        }
        else if (_currentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestUnit();
            FindClosestBuilding();
            if (_targetBuilding == null)
            {
                SetState(EnemyState.Idle);
            }
        }
        else if (_currentEnemyState == EnemyState.WalkToUnit)
        {
            if (_targetUnit)
            {
                _navMeshAgent.SetDestination(_targetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, _targetUnit.transform.position);
                if (distance > _distanceToFollow)
                {
                    SetState(EnemyState.WalkToBuilding);
                }
                if (distance < _distanceToAttack)
                {
                    SetState(EnemyState.Attack);
                }
            }
            else
            {
                SetState(EnemyState.WalkToBuilding);
            }
        }
        else if (_currentEnemyState == EnemyState.Attack)
        {
            if (_targetUnit)
            {
                float distance = Vector3.Distance(transform.position, _targetUnit.transform.position);
                if (distance > _distanceToAttack)
                {
                    SetState(EnemyState.WalkToUnit);
                }
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    _targetUnit.TakeDamage(_damage);
                }
            }
            else
            {
                SetState(EnemyState.WalkToBuilding);
            }
        }
    }

    public void SetState(EnemyState enemyState)
    {
        _currentEnemyState = enemyState;
        if (_currentEnemyState == EnemyState.Idle)
        {

        }
        else if (_currentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestBuilding();
            if (_targetBuilding)
            {
                _navMeshAgent.SetDestination(_targetBuilding.transform.position);
            }
        }
        else if (_currentEnemyState == EnemyState.WalkToUnit)
        {

        }
        else if (_currentEnemyState == EnemyState.Attack)
        {
            _timer = 0;
        }
    }

    public void FindClosestBuilding()
    {
        Building[] allBuildings = FindObjectsOfType<Building>();
        Building closestBuilding = null;

        float minDistance = Mathf.Infinity;
        for (int i = 0; i < allBuildings.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allBuildings[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBuilding = allBuildings[i];
            }
        }

        _targetBuilding = closestBuilding;
    }
    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        Unit closestUnit = null;

        float minDistance = Mathf.Infinity;
        for (int i = 0; i < allUnits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = allUnits[i];
            }
        }
        if (minDistance < _distanceToFollow)
        {
            _targetUnit = closestUnit;
            SetState(EnemyState.WalkToUnit);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToAttack);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToFollow);
    }
#endif
}

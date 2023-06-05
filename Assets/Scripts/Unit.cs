using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableOdject
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private int _price = 0;
    public int Price { get { return _price; } set { _price = value; } }

    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);
        _navMeshAgent.SetDestination(point);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [SerializeField] private int _money = 0;

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }
}

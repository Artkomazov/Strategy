using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableOdject : MonoBehaviour
{
    [SerializeField] private GameObject _selectIndicator;
    public virtual void Start()
    {
        _selectIndicator.SetActive(false);
    }


    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f; 
    }
    public virtual void OnUnHover()
    {
        transform.localScale = Vector3.one;
    }
    public virtual void Select()
    {
        _selectIndicator.SetActive(true);
    }
    public virtual void UnSelect()
    {
        _selectIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector3 point)
    {
        
    }
}

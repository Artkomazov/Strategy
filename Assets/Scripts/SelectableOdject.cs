using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableOdject : MonoBehaviour
{
    [SerializeField] private GameObject _selectIndicator;
    private void Start()
    {
        _selectIndicator.SetActive(false);
    }


    protected virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f; 
    }
    protected virtual void OnUnHover()
    {
        transform.localScale = Vector3.one;
    }
    protected virtual void Select()
    {
        _selectIndicator.SetActive(true);
    }
    protected virtual void UnSelected()
    {
        _selectIndicator.SetActive(false);
    }
}

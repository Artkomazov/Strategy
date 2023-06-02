using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managment : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableOdject _hovered;
    [SerializeField] private List<SelectableOdject> _listOfSelected;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                SelectableOdject hitSelectable = hit.collider.GetComponent<SelectableCollider>().SelectableOdject;
                if (_hovered)
                {
                    if (_hovered != hitSelectable)
                    {
                        _hovered.OnUnHover();
                        _hovered = hitSelectable;
                        _hovered.OnHover();
                    }
                }
                else
                {
                    _hovered = hitSelectable;
                    _hovered.OnHover();
                }
            }
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_hovered)
            {
                if (Input.GetKey(KeyCode.LeftControl) == false)
                {
                    UnSelectAll();
                }
                Select(_hovered);
            }
            //else
            //{
            //    UnSelectAll();
            //}

            if (hit.collider.tag == "Ground")
            {
                for (int i = 0; i < _listOfSelected.Count; i++)
                {
                    _listOfSelected[i].WhenClickOnGround(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonUp(1)) {
            UnSelectAll();
        }
    }

    private void Select(SelectableOdject selectableOdject) {
        if (_listOfSelected.Contains(selectableOdject) == false) {
            _listOfSelected.Add(selectableOdject);
            _hovered.Select();
        }
    }
    private void UnSelectAll()
    {
        for (int i = 0; i < _listOfSelected.Count; i++)
        {
            _listOfSelected[i].UnSelect();
        }
        _listOfSelected.Clear();
    }
    private void UnhoverCurrent()
    {
        if (_hovered)
        {
            _hovered.OnUnHover();
            _hovered = null;
        }
    }
}

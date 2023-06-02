using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Managment : MonoBehaviour
{
    [SerializeField] private SelectionState _currentSelectionState;

    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableOdject _hovered;
    [SerializeField] private List<SelectableOdject> _listOfSelected;
    [SerializeField] private Image _frameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

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
                _currentSelectionState = SelectionState.UnitsSelected;
                Select(_hovered);
            }
        }

        if (_currentSelectionState == SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.tag == "Ground")
                {
                    for (int i = 0; i < _listOfSelected.Count; i++)
                    {
                        _listOfSelected[i].WhenClickOnGround(hit.point);
                    }
                }
            }
        }
        
           
        if (Input.GetMouseButtonUp(1)) {
            UnSelectAll();
        }


        // Frame Selection
        if (Input.GetMouseButtonDown(0))
        {
            _frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _frameEnd = Input.mousePosition;
            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 sizeFrame = max - min;

            if (sizeFrame.magnitude > 10)
            {
                _frameImage.enabled = true;
                _frameImage.rectTransform.anchoredPosition = min;
                _frameImage.rectTransform.sizeDelta = sizeFrame;
                Rect rect = new Rect(min, sizeFrame);

                UnSelectAll();
                Unit[] allUnits = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnits.Length; i++)
                {
                    Vector2 screenPosition = _camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Select(allUnits[i]);
                    }
                }
                _currentSelectionState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _frameImage.enabled = false;
            if (_listOfSelected.Count > 0)
            {
                _currentSelectionState = SelectionState.UnitsSelected;
            }
            else 
            {
                _currentSelectionState = SelectionState.Other;
            }
        }
    }

    private void Select(SelectableOdject selectableOdject) {
        if (_listOfSelected.Contains(selectableOdject) == false) {
            _listOfSelected.Add(selectableOdject);
            selectableOdject.Select();
        }
    }
    private void UnSelectAll()
    {
        for (int i = 0; i < _listOfSelected.Count; i++)
        {
            _listOfSelected[i].UnSelect();
        }
        _listOfSelected.Clear();
        _currentSelectionState = SelectionState.Other;
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

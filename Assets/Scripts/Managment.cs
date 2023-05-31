using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managment : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableOdject _hovered;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>()) 
            {
                _hovered = hit.collider.GetComponent<SelectableCollider>().SelectableOdject;
            }
        }
    }
}

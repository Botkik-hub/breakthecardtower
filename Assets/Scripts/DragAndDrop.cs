using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


/// <summary>
/// Used to drag player around the map
/// </summary>
public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private bool _isDragged = false;
    private Camera _camera;
    private float _positionZ;

    public UnityEvent onDrag;
    public UnityEvent onDrop;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (!_isDragged) BeginDrag();
    }

    private void OnMouseUp()
    {
        EndDrag();
    }

    private void BeginDrag()
    {
        _isDragged = true;
        _positionZ = transform.position.z;
        onDrag.Invoke();
    }

    private void EndDrag()
    {
        _isDragged = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, _positionZ);
        onDrop.Invoke();
    }

    private void UpdateDrag()
    {
        var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _positionZ);
        transform.position = _camera.ScreenToWorldPoint(position) + offset;
    }
    
    private void Update()
    {
        if (_isDragged) UpdateDrag();
    }
}

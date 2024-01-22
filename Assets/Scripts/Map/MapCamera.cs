using System;
using UnityEngine;

/// <summary>
/// Scrollable camera
/// Scroll between two positions on the map
/// </summary>
public class MapCamera : MonoBehaviour
{
    [SerializeField] private Vector3 positionMin;
    [SerializeField] private Vector3 positionMax;

    [SerializeField] private float moveMultiplier = 1.0f;
    
    [Space]
    [SerializeField] private float drawRadius = 0.3f;

    private void Start()
    {
        transform.position = positionMin;
    }

    private void Update()
    {
        var moveBy = Input.GetAxis("Mouse ScrollWheel") * moveMultiplier;
        var currentPos = transform.position;
        var moveDirection = (positionMax - positionMin);
        moveDirection.Normalize();
        var newPos = Clamp(currentPos + moveDirection * moveBy, positionMin, positionMax);

        transform.position = newPos;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(positionMin, drawRadius);
        Gizmos.DrawSphere(positionMax, drawRadius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(positionMax, positionMin);
    }
    
    private Vector3 Clamp(Vector3 vec, Vector3 vecMin, Vector3 vecMax)
    {
        vec.x = Math.Clamp(vec.x, vecMin.x, vecMax.x);
        vec.y = Math.Clamp(vec.y, vecMin.y, vecMax.y);
        vec.z = Math.Clamp(vec.z, vecMin.z, vecMax.z);
        return vec;
    }
}

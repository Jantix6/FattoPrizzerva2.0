using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker
{
    Transform hostTransform;
    Transform targetTransform;

    Vector3 distanceVector;
    float distance;

    public Vector3 DistanceVector { get => distanceVector; }
    public float Distance { get => distance; }

    public DistanceChecker(Transform _hostTransform, Transform _targetTransform)
    {
        this.hostTransform = _hostTransform;
        this.targetTransform = _targetTransform;
    }

    public void CheckDistance()
    {
        Vector3 hostPosition = hostTransform.position;
        Vector3 targetPosition = targetTransform.position;

        distanceVector = targetPosition - hostPosition;
        distance = distanceVector.magnitude;

        Debug.DrawLine(hostPosition, targetPosition, Color.blue, 1f);
    }

}

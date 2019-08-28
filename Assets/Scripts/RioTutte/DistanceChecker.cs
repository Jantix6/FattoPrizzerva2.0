using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DistanceChecker
{
    [SerializeField] Transform hostTransform;
    [SerializeField] Transform targetTransform;

    [SerializeField] Vector3 distanceVector;
    [SerializeField] float distance;

    bool isDebugOnline;

    public Vector3 DistanceVector { get => distanceVector; }
    public float Distance { get => distance; }

    public DistanceChecker(Transform _hostTransform, Transform _targetTransform, bool _debug = false)
    {
        this.hostTransform = _hostTransform;
        this.targetTransform = _targetTransform;

        this.isDebugOnline = _debug;
    }

    public void CheckDistance()
    {
        Vector3 hostPosition = hostTransform.position;
        Vector3 targetPosition = targetTransform.position;

        distanceVector = targetPosition - hostPosition;
        distance = distanceVector.magnitude;

        if (isDebugOnline)
            Debug.DrawLine(hostPosition, targetPosition, Color.blue, 1f);
    }

}

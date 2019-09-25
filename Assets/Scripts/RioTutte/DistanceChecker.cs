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

    public Vector3 DistanceVector { get => distanceVector; }
    public float Distance { get => distance; }

    public DistanceChecker(Transform _hostTransform, Transform _targetTransform)
    {
        this.hostTransform = _hostTransform;
        this.targetTransform = _targetTransform;
    }

    public void CheckDistance()
    {
        Vector3 originPosition = hostTransform.position;
        Vector3 targetPosition = targetTransform.position;

        distanceVector = targetPosition - originPosition;
        distance = distanceVector.magnitude;

    }

}

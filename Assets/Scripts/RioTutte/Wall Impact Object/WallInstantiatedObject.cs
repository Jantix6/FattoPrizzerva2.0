using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInstantiatedObject : MonoBehaviour
{
    private DistanceChecker distanceCheker;

    [SerializeField] private Transform objectToTrack;
    [SerializeField] private BaseState teleportScript;
    [SerializeField] private int maxDistanceBeforeTp = 2;

    [SerializeField] private float currentDistance;
    [SerializeField] private float minDistance = Mathf.Infinity;

    public void Initialize (Transform _objectToTrack, BaseState _teleportScript)
    {
        objectToTrack = _objectToTrack;
        teleportScript = _teleportScript;

        if (objectToTrack == null)
            throw new UnassignedReferenceException();
        if (teleportScript == null)
            throw new UnassignedReferenceException();

        distanceCheker = new DistanceChecker(this.transform, objectToTrack);
    }

    private void FixedUpdate()
    {
        // en algunos momentos la referencia al objecToTtrack passa a ser Missing pero parece algo aleatorio

        if (distanceCheker == null)
            distanceCheker = new DistanceChecker(this.transform, objectToTrack);

        distanceCheker.CheckDistance();

        // get the current distance
        currentDistance = distanceCheker.Distance;

        // set the mindistance
        if (currentDistance <= minDistance)
            minDistance = currentDistance;

        // check and execute
        if (currentDistance >= maxDistanceBeforeTp + minDistance)
        {
            Debug.Log("Current distance is greater than " + maxDistanceBeforeTp);
            teleportScript.Execute();
                
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInstantiatedObject : MonoBehaviour
{
    [SerializeField] private DistanceChecker distanceCheker;

    [SerializeField] private Transform objectToTrack;
    [SerializeField] private BaseState teleportScript;
    [SerializeField] private int maxDistanceBeforeTp = 2;

    [SerializeField] private float currentDistance;
    [SerializeField] private float minDistance = Mathf.Infinity;

    public void Initialize (Transform _objectToTrack, BaseState _stateToExecute)
    {
        teleportScript = _stateToExecute;
        Debug.LogError(teleportScript);

        objectToTrack = _objectToTrack;
        distanceCheker = new DistanceChecker(this.transform, objectToTrack);
        Debug.LogError(distanceCheker);
    }

    private void FixedUpdate()
    {
        Debug.LogError("FixedUpdate");
        Debug.LogError(distanceCheker);

        // en algunos momentos la referencia al objecToTtrack passa a ser Missing pero parece algo aleatorio
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


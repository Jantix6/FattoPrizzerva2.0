using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalinaScript : MonoBehaviour, IAdrenalinable
{

    [SerializeField] private float currentAdrenalina;
    [SerializeField] private float maxAdrenalina = 100;

    public float decreseAdrenalinaPerSecond = 5f;

    public float Adrenalina { get => currentAdrenalina; set => currentAdrenalina = value; }
    public float MaxAdrenalina { get => maxAdrenalina; set => maxAdrenalina = value; }

    public void ReduceAdrenalina()
    {
        currentAdrenalina -= decreseAdrenalinaPerSecond * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdrenalinable
{
    float Adrenalina { get; set; }
    float MaxAdrenalina { get; set; }

    void ReduceAdrenalina();
}

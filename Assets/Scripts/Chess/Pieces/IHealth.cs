using UnityEngine;
using System.Collections;

public interface IHealth
{
    float Health { get; set; }

    void GetDamage(float damage);
    void Die();
}

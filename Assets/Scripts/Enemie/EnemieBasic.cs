using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBasic : MonoBehaviour
{
    protected float damage = 6;
    protected float speedEmpuje = 0;
    protected float maxTime = 0;
    public enum TypeOfDamage { NADA, EMPUJAENEMIGO, EMPUJAAMBOS, PLAYERREBOTA, PLAYEREMPUJADO, EMPUJADOCONCONDICIÓN };
    protected TypeOfDamage currentDamage = 0;
    protected Vector3 directionKnockback;
    // Start is called before the first frame update
    void Awake()
    {
    }


    public void MoveDirectionHit(Vector3 _direction, float _damage, bool _condición = false)
    {
        if (_damage <= damage)
            currentDamage = TypeOfDamage.PLAYERREBOTA;
        else
        {
            if (_condición)
                currentDamage = TypeOfDamage.EMPUJADOCONCONDICIÓN;
            else
                currentDamage = TypeOfDamage.EMPUJAENEMIGO;

            if (_damage - damage / 1.5f > 25)
                _damage = 25 + damage / 1.5f;

            directionKnockback = new Vector3(_direction.x, 0, _direction.z).normalized;
            maxTime = _damage / damage;
            speedEmpuje = _damage - damage / 1.5f;


            maxTime = Mathf.Lerp(0, 0.25f, maxTime);
        }

    }

    public void SetDamageMin(float _damage)
    {
        damage = _damage;
    }

    public float GetDamageMin()
    {
        return damage;
    }

    public TypeOfDamage GetTypeOfDamage()
    {
        return currentDamage;
    }

    public void RerstartTypeOfDamage()
    {
        currentDamage = TypeOfDamage.NADA;
    }

    public float GetTimeKnockBack()
    {
        return maxTime;
    }

    public float GetSpeedKnockback()
    {
        return speedEmpuje;
    }

    public Vector3 GetDirectionKnockback()
    {
        return directionKnockback;
    }
}

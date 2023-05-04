using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    IDamageable damageable;

    internal void TakeDamage(int damage)
    {
        if (damageable == null)
        {
            damageable = GetComponent<IDamageable>();
        }
        damageable.CalculateDamage(ref damage);
        damageable.ApplyDamage(damage);

       
        GameManeger.instance.mesageSystem.PostMessage(transform.position, damage.ToString());
        damageable.CheckState();
    }
}

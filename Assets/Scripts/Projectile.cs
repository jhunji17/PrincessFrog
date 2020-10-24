using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;

    public Constants.Type type;

    public abstract void Fire(float angle);

    public abstract GameObject Absorb();
}

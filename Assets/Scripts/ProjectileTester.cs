using System;
using UnityEngine;

public class ProjectileTester : MonoBehaviour
{
    public GameObject weaponType;
    public GameObject target;

    public void Start()
    {
        GameObject weapon = Instantiate(weaponType, transform.position, transform.rotation);
        Box b = weapon.GetComponent<Box>();
        b.Fire(target.transform);
        // b.Fire(0);

    }
}

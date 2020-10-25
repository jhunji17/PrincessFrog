using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public Constants.Type type;
    public float speed;
    protected Rigidbody2D weapon;
    
    public void Awake()
    {
        weapon = GetComponent<Rigidbody2D>();
    }

    public void Fire(float angle)
    
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        weapon.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle),Mathf.Sin(Mathf.Deg2Rad * angle)) * speed;
    }

    public void Fire(Transform playerPos)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        weapon.velocity = Vector2.MoveTowards(transform.position, playerPos.position, speed);
    }
    
    public void Absorb()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

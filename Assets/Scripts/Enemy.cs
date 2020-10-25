using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed;
    public GameObject target;
    public Vector2 playerXAxis;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
    }
    void Update()
    {
        playerXAxis = new Vector2(target.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerXAxis, speed * Time.deltaTime);
    }
}

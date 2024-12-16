using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    private Vector3 moveDirection;

    public Rigidbody theRB;

    private void Start()
    {
        foreach (Transform pp in patrolPoints)
        {
            pp.SetParent(null);
        }
    }

    private void Update()
    {
        
    }
}

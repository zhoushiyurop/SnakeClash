using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementDirection;
    private float targetAngle;
    private float movementSpeed;
    private float timeToRotate;
    private System.Random rand;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rand = new System.Random();
        timeToRotate = rand.Next(300, 800) / 100;
        targetAngle = rand.Next(0, 360);
        movementDirection = new Vector3(0, 0, 1);
        movementSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timeToRotate -= Time.deltaTime;
    }

    private void Move()
    {
        if(timeToRotate < 0)
        {
            timeToRotate = rand.Next(300, 800) / 100;
            targetAngle = rand.Next(0, 360);
        }
        rb.transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
        rb.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetAngle, 0), 180f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            targetAngle += 180;
        }
    }
}

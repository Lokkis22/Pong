using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupScript : MonoBehaviour
{

    public Transform ball;
    private Vector3 ballPos;
    private int life;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        life = 2; // full cup
        ballPos = ball.position;
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1) // if cup is empty
        {
            done = true;
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
            Destroy(transform.gameObject, 0.7f);
            ball.GetComponent<Rigidbody>().position = ballPos;
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody>().useGravity = false;
            //ball.GetComponent<ballScript>().inCup = false;
        }

    }

    // if ball hits in cup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball" && ball.GetComponent<ballScript>().inCup == true && ball.GetComponent<ballScript>().hitRim == true)
        {
            life -= 2;
            Debug.Log("full cup");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if ball hits rim of cup
        if (collision.collider.gameObject.tag == "ball" && ball.GetComponent<ballScript>().hitRim == true)
        {
            life -= 1; // half cup
            Debug.Log("half cup");
        }
    }
}

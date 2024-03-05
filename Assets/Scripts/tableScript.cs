using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableScript : MonoBehaviour
{

    public Transform paddle1;
    public Transform paddle2;
    public Transform ball;
    private Vector3 ballPos;
    public int bounceCount;

    // Start is called before the first frame update
    void Start()
    {
        bounceCount = 0;
        ballPos = ball.position;
    }

    // Update is called once per frame
    void Update()
    {

        // if the ball bounced more than once
       if (bounceCount > 1)
        {
            ball.position = ballPos; // reset ball position
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody>().useGravity = false;
            bounceCount = 0; // reset bounce count
        }
    }

    // when ball hits the table
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            bounceCount += 1; // update bounce count
        }
    }
}

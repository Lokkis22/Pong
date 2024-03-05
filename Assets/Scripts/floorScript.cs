using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorScript : MonoBehaviour
{

    public Transform player, cpu, ball, table;
    private Vector3 pos;
    private Vector3 cpuServePos;
    public bool hitFloor;

    // Start is called before the first frame update
    void Start()
    {
        pos = ball.position;
        cpuServePos = new Vector3(-2.74f, 0.821f, 0.012f);// dont like it when i hard-code it but dont know what else to do rn
        hitFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            hitFloor = true;

            // if the player hit the ball and it went straight to the floor or cpu hit the ball and it bounced once then hit the floor
            if (player.GetComponent<RacketController>().hit == true && table.GetComponent<tableScript>().bounceCount != 1 || player.GetComponent<RacketController>().hit == false && table.GetComponent<tableScript>().bounceCount == 1)
            {
                // reset ball to player serve
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                ball.GetComponent<Rigidbody>().useGravity = false;
                ball.position = pos;
            }
            else
            {
                // reset ball to cpu serve
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                ball.GetComponent<Rigidbody>().useGravity = false;
                ball.position = cpuServePos;
                Debug.Log("Ball gravity: " + ball.GetComponent<Rigidbody>().useGravity);
            }
            Invoke("toggleHitFloor", 3.0f);
        }
    }

    void toggleHitFloor()
    {
        hitFloor = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    float speed = 60;
    public Transform ball;
    public Transform cup;
    float force = 4.2f;
    public Transform player;
    public Transform table;
    public Transform floor;
    private bool underServe;

    Vector3 position;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        initialPos = transform.position;
        underServe = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
    {
        position.z = ball.position.z; // target z coordinate

        // move paddle
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        
        // if cpu should serve
        if (shouldServe() && underServe == false)
        {
            Invoke("performServe", 1.5f);
            Debug.Log("performServe invoked");
            underServe = true;
        }
    }

    // when cpu paddle hits ball
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Vector3 dir = cup.position - ball.position; // direction for ball (aim)

            ball.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 4.7f, 0); // speed/force on ball

            if (ball.GetComponent<Rigidbody>().useGravity == false) // turn on gravity if off
            {
                ball.GetComponent<Rigidbody>().useGravity = true;
            }

            player.GetComponent<RacketController>().hit = false; // set player hit to false
        }

        table.GetComponent<tableScript>().bounceCount = 0; // reset bounce count


    }

    // placeholder for serve
    void callServe()
    {
        Invoke("performServe", 1.5f);
    }

    // to serve the ball
    void performServe()
    {
        initialPos.x = -2.62f;

        transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);

        underServe = false;
    }


    // to check if the cpu should serve
    bool shouldServe()
    {
        if (player.GetComponent<RacketController>().hit == true)
        {
            if (ball.GetComponent<ballScript>().inCup == true && cup.GetComponent<cupScript>().done == true)
            {
                return true;
            }else if (ball.GetComponent<ballScript>().hitRim == true && table.GetComponent<tableScript>().bounceCount > 1 || floor.GetComponent<floorScript>().hitFloor == true)
            {
                return true;
            }else if (table.GetComponent<tableScript>().bounceCount == 1 && floor.GetComponent<floorScript>().hitFloor == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (table.GetComponent<tableScript>().bounceCount != 1 && floor.GetComponent<floorScript>().hitFloor == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

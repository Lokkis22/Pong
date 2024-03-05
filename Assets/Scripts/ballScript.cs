using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{

    public Transform ball;
    public bool inCup;
    public bool hitRim;

    // Start is called before the first frame update
    void Start()
    {
        inCup = false;
        hitRim = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "cup")
        {
            hitRim = true;
            Debug.Log("Hit the rim");

            Invoke("toggleSwitches", 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cup")
        {
            inCup = true;
            Debug.Log("In the cup");

            Invoke("toggleSwitches", 1.0f);
        }
    }

    private void toggleSwitches()
    {
        if (hitRim == true)
        {
            hitRim = false;
        }
        if (inCup == true)
        {
            inCup = false;
        }
    }
}

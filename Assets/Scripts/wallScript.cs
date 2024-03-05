using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{

    public Transform cup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // when ball hit the wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log("HIT WALL");
            other.GetComponent<Rigidbody>().velocity = cup.GetComponent<Rigidbody>().position.normalized * 4 + new Vector3(0,3,0) ;
        }
    }
}

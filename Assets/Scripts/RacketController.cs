using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;
    private float force = 4.5f;
    private Vector3 initPos;
    public bool hit;
    public Transform CPU;
    public Transform table;
    public GameObject[] cups;

    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 2.7f; // to control speed of movement of paddle
        initPos = transform.position; // inital position of the paddle
        hit = true;

        Application.targetFrameRate = 60; // to set the frame rate, dont know if it works
    }

    // Update is called once per frame
    void Update()
    { 

        // when touching the screen
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
    

            // so paddle moves to wherever the touch is happening
            if (touch.phase == TouchPhase.Began) {

                // get the position of the touch and manually set z position in front of camera
                Vector3 touchPosition = touch.position;
                touchPosition.z = Camera.main.nearClipPlane + 2;

                
                // convert touchPosition into world units
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                worldPosition.y = 1; // set y to 1 so it will stay at that height

                transform.position = worldPosition; // set paddle to worldPosition
            }
            

            // when the paddle is being moved
            if (touch.phase == TouchPhase.Moved)
            {
                // update position of the paddle
                transform.position = new Vector3(
                    transform.position.x - (touch.deltaPosition.y / Screen.width) * speedModifier,
                    transform.position.y,
                    transform.position.z + (touch.deltaPosition.x / Screen.height) * speedModifier);

            }
        }

    }

    // when paddle hits ball
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ball")
        {
            Vector3 dir = new Vector3(transform.position.x - touch.deltaPosition.y, transform.position.y, transform.position.z + touch.deltaPosition.x);
            collider.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0,4.7f,0);
            if (collider.GetComponent<Rigidbody>().useGravity == false) {
                collider.GetComponent<Rigidbody>().useGravity = true;
            }
            hit = true; // set hit bool to true
            table.GetComponent<tableScript>().bounceCount = 0; // reset the bounce count
        }
    }
}
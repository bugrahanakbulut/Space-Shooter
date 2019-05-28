using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 7.5f;


	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -3, 0);
	}
	
	// Update is called once per frame
	void Update () {
        movementController();
	}

    private void movementController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 horizontalDirection = Vector3.right * speed * horizontalInput * Time.deltaTime;
        Vector3 verticalDirection = Vector3.up * speed * verticalInput * Time.deltaTime;

        if (transform.position.x < -6.5f)
            transform.position = new Vector3(-6.5f, transform.position.y, transform.position.z);
        else if (transform.position.x > 8.0f)
            transform.position = new Vector3(8.0f, transform.position.y, transform.position.z);

        if (transform.position.y < -3.5f)
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        else if (transform.position.y > -1.0f)
            transform.position = new Vector3(transform.position.x, -1.0f, transform.position.z);
        

        transform.Translate(horizontalDirection + verticalDirection);
    }
}

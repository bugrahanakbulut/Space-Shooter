using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 5.0f;


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

        transform.Translate(horizontalDirection + verticalDirection);
    }
}

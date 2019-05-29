using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject laserPrefab;

    private float fireRate = 0.25f;
    private float canFire = 0.0f;
    
    private float speed = 7.5f;


	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -3, 0);
	}
	
	// Update is called once per frame
	void Update () {
        movementController();
        laserController();
	}

    private void movementController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 horizontalDirection = Vector3.right * speed * horizontalInput * Time.deltaTime;
        Vector3 verticalDirection = Vector3.up * speed * verticalInput * Time.deltaTime;

        // Player movement limitations on x-axis
        if (transform.position.x < -6.0f)
            transform.position = new Vector3(-6.0f, transform.position.y, transform.position.z);
        else if (transform.position.x > 6.0f)
            transform.position = new Vector3(6.0f, transform.position.y, transform.position.z);

        // Player movement limitations on y-axis
        if (transform.position.y < -3.5f)
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        else if (transform.position.y > -1.0f)
            transform.position = new Vector3(transform.position.x, -1.0f, transform.position.z);
        

        transform.Translate(horizontalDirection + verticalDirection);
    }

    private void laserController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
             shootLaser();
    }

    private void shootLaser()
    {
        if (Time.time > canFire)
            {
                Vector3 relativeSpawnPos = new Vector3(0.0f, 0.8f);
                Instantiate(laserPrefab, transform.position + relativeSpawnPos, Quaternion.identity);
                canFire = Time.time + fireRate;
            }
    }
}

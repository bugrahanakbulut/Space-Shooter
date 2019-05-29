using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab;

    private bool powerUpTripleShot = false;

    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    
    private float _speed = 7.5f;


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


        Vector3 horizontalDirection = Vector3.right * _speed * horizontalInput * Time.deltaTime;
        Vector3 verticalDirection = Vector3.up * _speed * verticalInput * Time.deltaTime;

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
        if (Input.GetKey(KeyCode.Space))
             shootLaser();
    }

    private void shootLaser()
    {
        if (Time.time > _canFire)
        {
            List<Vector3> laserSpawnVectors = new List<Vector3>();
            laserSpawnVectors.Add(transform.position + new Vector3(0.0f, 0.8f));

            if (powerUpTripleShot)
            {
                laserSpawnVectors.Add(transform.position + new Vector3(0.55f, -0.11f));
                laserSpawnVectors.Add(transform.position + new Vector3(-0.55f, -0.11f));
            }

            foreach (Vector3 vect in laserSpawnVectors)
                Instantiate(_laserPrefab, vect, Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    public void EnableTripleShotPowerUp()
    {
        powerUpTripleShot = true;
        StartCoroutine(DisableTripleShotPowerUpCoRoutine());
    }

    private IEnumerator DisableTripleShotPowerUpCoRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        powerUpTripleShot = false;
    }
}

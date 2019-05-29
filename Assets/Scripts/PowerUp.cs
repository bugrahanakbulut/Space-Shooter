using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float speed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        movementController();
	}

    private void movementController()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            // get player
            Player player = other.GetComponent<Player>();
            // null check for player
            if (player != null)
            {
                // enable power up
                player.EnableTripleShotPowerUp();
                // destroy power up game object
                Destroy(gameObject);
            }
            
        }
    }
}

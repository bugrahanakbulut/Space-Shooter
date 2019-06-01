using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float speed = 3.0f;
    [SerializeField]
    private int id; 

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
                // enable triple shot
                if (id == 0)
                    player.EnableTripleShotPowerUp();
                else if (id == 1)
                {
                    player.EnableSpeedPowerUp();
                }
                else if (id == 2)
                {
                    player.EnableSheildPowerUp();
                }
                    
                
                // destroy power up game object
                Destroy(gameObject);
            }
            
        }
    }
}

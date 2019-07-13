using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 5.0f;
    // Use this for initialization

    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private float collusionDamageOnPlayer = 50.0f;

    [SerializeField]
    private GameObject explosionAnim;

    private UIManager _uiManager;

    void Start () {
        transform.position = new Vector3(3.5f, -7.0f, 0.0f);
        initUIManager();
    }
	
	// Update is called once per frame
	void Update () {
        movementController();
        positionController();
	}

    private void movementController()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void positionController()
    {
        if (transform.position.y < -7)
        {
            float xRandom = Random.Range(-6.5f, 6.5f);
            transform.position = new Vector3(xRandom, 7.0f, 0.0f);
        }
    }

    private void initUIManager()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void doDamage(float damage)
    {
        if ((health - damage) > 0)
            health -= damage;
        else
        {
            Instantiate(explosionAnim, this.transform.position, Quaternion.identity);
            _uiManager.UpdatePlayerScore();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.doDamage(collusionDamageOnPlayer);
            

            Instantiate(explosionAnim, this.transform.position, Quaternion.identity);
            _uiManager.UpdatePlayerScore();
            Destroy(gameObject);
        }

        else if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null)
            {
                doDamage(laser.damage);
                other.gameObject.SetActive(false);
            }
            
        }
    }

}

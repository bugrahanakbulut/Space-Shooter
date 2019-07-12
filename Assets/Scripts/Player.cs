using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab;

    private ObjectPooler _pooler;

    //[SerializeField]
    //private GameObject _sheild;

    [SerializeField]
    private GameObject _explosionAnim;

    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private GameObject _sheild;
    private bool powerUpSheildEnabled = false;
    private float sheildAmount = 50.0f;
    private float remainingSheild = 50.0f;

    private bool powerUpTripleShot = false;

    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    
    private float _speed = 5.0f;
    private float _speedPowerUpScale = 1.5f;

    private float _playerPowerUpTime = 5.0f;

    private UIManager _uiManager;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -3, 0);
        _pooler = GameObject.Find("ScriptHolder").GetComponent<ObjectPooler>();
        initUIManager();
	}
	
	// Update is called once per frame
	void Update () {
        movementController();
        laserController();
	}

    private void initUIManager()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdatePlayerLives(lives);

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
            {
                GameObject laser = _pooler.GetPooledObject();
                laser.transform.position = vect;
                laser.SetActive(true);
            }
                
            _canFire = Time.time + _fireRate;
        }
    }

    public void doDamage(float damage)
    {
        // if sheild power up enabled
        if (powerUpSheildEnabled)
        {
            // if sheild does not destroyed by damage
            if (remainingSheild - damage > 0)
            { 
                remainingSheild -= damage;
                return;
            }
            // if damage destroys sheild
            else
            {
                // reset sheild
                powerUpSheildEnabled = false;
                _sheild.SetActive(false);
                remainingSheild = sheildAmount; 
                // handle the not absorbed damage by shield
                damage -= remainingSheild;
            }

        }

        if (health - damage > 0)
            health -= damage;
        else
        {
            lives--;
            _uiManager.UpdatePlayerLives(lives);
            if (lives > 0)
                health = health - damage + 100;
            else
                killPlayer();
        }

        _uiManager.UpdatePlayerHealth(health);

    }

    public void EnableTripleShotPowerUp()
    {
        powerUpTripleShot = true;
        StartCoroutine(DisableTripleShotPowerUpCoRoutine());
    }
    
    private IEnumerator DisableTripleShotPowerUpCoRoutine()
    {
        yield return new WaitForSeconds(_playerPowerUpTime);
        powerUpTripleShot = false;
    }

    public void EnableSpeedPowerUp()
    {
        _speed *= _speedPowerUpScale;
        StartCoroutine(DisableSpeedPowerUpCoRoutuine());
    }

    private IEnumerator DisableSpeedPowerUpCoRoutuine()
    {
        yield return new WaitForSeconds(_playerPowerUpTime);
        _speed /= _speedPowerUpScale;
    }

    public void EnableSheildPowerUp()
    {
        powerUpSheildEnabled = true;
        _sheild.SetActive(true);
        StartCoroutine(DisableSheildPowerUpCoRoutine());
    }

    private IEnumerator DisableSheildPowerUpCoRoutine()
    {
        yield return new WaitForSeconds(_playerPowerUpTime);
        powerUpSheildEnabled = false;
        _sheild.SetActive(false);
        remainingSheild = sheildAmount;
    }

    private void killPlayer()
    {
        Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        _uiManager.LoadMainMenu();
        Destroy(gameObject);

    }
}

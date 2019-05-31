using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroyer : MonoBehaviour {

    [SerializeField]
    private float animDuration;

	// Use this for initialization
	void Start () {
        Debug.Log("should be destroyed");
        StartCoroutine(DestroyAnimCoRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private IEnumerator DestroyAnimCoRoutine()
    {
        yield return new WaitForSeconds(animDuration);
        Destroy(this.gameObject);
    }
}

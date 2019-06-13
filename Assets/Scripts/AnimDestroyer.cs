using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroyer : MonoBehaviour {

    [SerializeField]
    private float animDuration;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAnimCoRoutine());
	}
	
    private IEnumerator DestroyAnimCoRoutine()
    {
        yield return new WaitForSeconds(animDuration);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPool;
    [SerializeField]
    private List<GameObject> pooledObjects;
    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int amountToPool;




    // Start is called before the first frame update
    void Start()
    {
        InitPooler();
    }


    void InitPooler()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.transform.parent = _laserPool.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // no active item in pool
        GameObject obj = (GameObject)Instantiate(objectToPool);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        obj.transform.parent = _laserPool.transform;
        return obj;
    }
}


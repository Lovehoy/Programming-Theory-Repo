using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private int activeObjectsCount; // Track the number of active objects

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize list of pooled objects
        pooledObjects = new List<GameObject>();

        // Create and add objects to the pool
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        // Initialize active objects count
        activeObjectsCount = 0;
    }

    // Retrieve a pooled object
    public GameObject GetPooledObject()
    {
        // Iterate through pooled objects
        foreach (GameObject obj in pooledObjects)
        {
            // Check if the object is not active
            if (obj != null && !obj.activeSelf)
            {
                // Increment active objects count
                activeObjectsCount++;
                // Return the inactive object
                return obj;
            }
        }

        // No inactive object found
        // Reset active objects count if all objects are used
        if (activeObjectsCount >= amountToPool)
        {
            activeObjectsCount = 0;
        }

        // Create a new object if all pooled objects are used
        GameObject newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        activeObjectsCount++; // Increment active objects count
        return newObj;
    }

    // Method to reset a destroyed object and return it to the pool
    public void ResetAndAddToPool(GameObject obj)
    {
        obj.SetActive(false); // Deactivate the object
        // Reset any necessary properties of the object here

        // Add the object back to the pool
        pooledObjects.Add(obj);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    public Queue<GameObject> prefabQueue;

    private void Awake()
    {
        prefabQueue = new Queue<GameObject>();
        Initialize(10);
    }

    private void Initialize(int count)
    {
        for(int i = 0; i < count; i++)
        {
            prefabQueue.Enqueue(CreateNewObject());
        }
    }

    private GameObject CreateNewObject()
    {
        var newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.SetParent(gameObject.transform);
        return newObject;
    }

    public GameObject GetObject()
    {
        //Debug.Log("GetObject 호출. 현재 큐의 크기 : " + prefabQueue.Count);
        if(prefabQueue.Count > 0)
        {
            GameObject objectToReturn = prefabQueue.Dequeue();
            objectToReturn.transform.SetParent(null);
            objectToReturn.SetActive(true);
            return objectToReturn;
        }
        else
        {
            GameObject newObject = CreateNewObject();
            newObject.SetActive(true);
            return newObject;
        }
    }

    public void ReleaseObject(GameObject objectToRelease)
    {
        objectToRelease.SetActive(false);
        objectToRelease.transform.SetParent(gameObject.transform);
        prefabQueue.Enqueue(objectToRelease);
    }
}

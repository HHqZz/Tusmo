using System.Collections.Generic;

using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject prefab;

    public int poolSize = 10;

    public int maxPoolSize = 50;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private int activeCount = 0;

    private void Start() {

        for (int i = 0; i < poolSize; i++) {

            GameObject obj = Instantiate(prefab);

            obj.SetActive(false);

            pool.Enqueue(obj);

        }

    }

    public GameObject GetObject() {

        GameObject obj;

        if (pool.Count > 0) {

            obj = pool.Dequeue();

        } else if (activeCount < maxPoolSize) {

            obj = Instantiate(prefab);

        } else {

            // Recycle (simplifié)

            obj = pool.Dequeue();

        }

        obj.SetActive(true);

        activeCount++;

        return obj;

    }

    public void ReturnObject(GameObject obj) {

        obj.SetActive(false);

        pool.Enqueue(obj);

        activeCount--;

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public GameObject objectPooled;
	public int poolSize = 100;
	public bool willGrow = true;

	public List<GameObject> pooledObjects;

	/*
		Le plus ancien sera toujours en début de stack, le plus récent à la fin
	*/

	void Start(){
		pooledObjects = new List<GameObject>();

		// On initialise les objets
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = Instantiate(objectPooled);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		GameObject candidate = null;
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			// On cherche un projectile inactif
			if (!pooledObjects[i].activeInHierarchy)
			{
				candidate = pooledObjects[i];
				break;
			}
		}

		if (candidate != null)
		{
			pooledObjects.Remove(candidate);
			pooledObjects.Add(candidate);
			candidate.SetActive(true);
			return candidate;
		}
		else 
		{
			// Si on en a pas trouvé, on peut essayer d'en créer un
			if (willGrow)
			{
				GameObject obj = Instantiate(objectPooled);
				pooledObjects.Add(obj);
				return obj;
			}
			else 
			{
				if (pooledObjects.Count >= 1)
				{
					candidate = pooledObjects[0];
					pooledObjects.Remove(candidate);
					pooledObjects.Add(candidate);
					candidate.SetActive(true);
					return candidate;
				}
				return null;
			}
		}
	}
}

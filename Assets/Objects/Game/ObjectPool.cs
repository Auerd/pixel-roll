using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool 
    {
        readonly Stack<GameObject> pool;
        GameObject currObj;

        public ObjectPool() 
        {
            pool = new();
        }

        public ObjectPool(params GameObject[] objects)
        {
            pool = new();
            foreach(var obj in objects)
                pool.Push(obj);
        }

        private GameObject FindObject()
        {
            if(pool.Count == 0)
                return null;
            return pool.Pop();
        }

        public GameObject Get(GameObject prefab)
        {
            currObj = FindObject();
            if (currObj == null)
                return Object.Instantiate(prefab);
            currObj.SetActive(true);
            return currObj;
        }

        public GameObject Get(GameObject prefab, in Vector2 pos = default, in Quaternion rot = default)
        {
            currObj = Get(prefab);
            currObj.transform.SetLocalPositionAndRotation(pos, rot);
            return currObj;
        }

        public GameObject Get(GameObject prefab, Transform parent, in Vector2 pos = default, in Quaternion rot = default)
        {
            currObj = FindObject();
            if(currObj == null)
                currObj = Object.Instantiate(prefab, parent);
            currObj.SetActive(true);
            currObj.transform.SetLocalPositionAndRotation(pos, rot);
            return currObj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Push(obj);
        }
    }
}
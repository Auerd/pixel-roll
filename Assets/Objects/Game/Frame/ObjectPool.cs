using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Game
{
    public class ObjectPool 
    {
        private readonly List<GameObject> pool;
        private GameObject currObj;

        public ObjectPool() 
        {
            pool = new();
        }

        public ObjectPool(params GameObject[] objects)
        {
            pool = new();
            pool.AddRange(objects);
        }

        private GameObject FindObject(GameObject go)
        {
            foreach (var obj in pool)
            {
                if (!obj.activeSelf)
                {
                    pool.Remove(obj);
                    obj.SetActive(true);
                    return obj;
                }
            }
            return null;
        }

        public GameObject Get(GameObject prefab)
        {
            currObj = FindObject(prefab);
            if (currObj == null)
                return Object.Instantiate(prefab);
            return currObj;
        }

        public GameObject Get(GameObject prefab, in Vector2 pos, in Quaternion rot)
        {
            currObj = Get(prefab);
            currObj.transform.SetLocalPositionAndRotation(pos, rot);
            return currObj;
        }

        public GameObject Get(GameObject prefab, Transform parent, in Vector2 pos = new(), in Quaternion rot = new())
        {
            currObj = FindObject(prefab);
            if(currObj == null)
                currObj = Object.Instantiate(prefab, parent);
            currObj.transform.SetLocalPositionAndRotation(pos, rot);
            return currObj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
}
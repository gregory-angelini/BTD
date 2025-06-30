using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        Queue<T> pool = new Queue<T>();
        T prefab;


        public void Initialize(T prefab, int size)
        {
            this.prefab = prefab;

            for (int i = 0; i < size; i++)
            {
                var obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            T obj = pool.Count > 0 ? pool.Dequeue() : GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
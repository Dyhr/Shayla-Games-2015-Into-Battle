using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class HitPool : MonoBehaviour
{

    public static HitPool instance;
    public GameObject prefab;
    public int size;

    private List<GameObject> inactive;
    private List<GameObject> active;

    public void Start()
    {
        instance = this;
        inactive = new List<GameObject>(size);
        active = new List<GameObject>(size);
        for (int i = 0; i < size; ++i)
        {
            var go = Instantiate(prefab);
            go.SetActive(false);
            inactive.Add(go);
        }
    }

    public GameObject Poll(Vector3 pos)
    {
        if (inactive.Any())
        {
            var go = inactive.Last();
            go.transform.position = pos;
            inactive.Remove(go);
            active.Add(go);
            go.SetActive(true);
            return go;
        }
        else
        {
            var go = Instantiate(prefab);
            go.transform.position = pos;
            active.Add(go);
            return go;
        }
    }
    public void Update()
    {
        inactive.AddRange(active.Where(o => !o.activeSelf));
        active.RemoveAll(o => !o.activeSelf);
    }
}

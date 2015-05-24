using UnityEngine;
using System.Collections;

public class SeaNoiseFollow : MonoBehaviour
{
    public Transform target;
    public void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, Mathf.Min(target.position.z,0));
        }
    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public void Start()
    {
        foreach (var boat in GameObject.FindGameObjectsWithTag("Boat"))
        {
            boat.GetComponent<Boat>().Ready = true;
        }
    }

    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point + Vector3.up * 1.83f;
        }
    }
}

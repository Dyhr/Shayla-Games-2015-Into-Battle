using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Boat : MonoBehaviour
{
    public float Speed;
    public Transform Door;
    public Boat Prefab;

    private Rigidbody _body;
    private bool _stopped;
    private float _time = 5;
    private float oriY;

    public void Start()
    {
        oriY = transform.position.y;
        _body = GetComponent<Rigidbody>();
        _stopped = false;
        var soldiers = new List<Transform>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            var t = transform.GetChild(i);
            if(t.GetComponent<Soldier>() != null) soldiers.Add(t);
        }
        foreach(var soldier in soldiers)
            soldier.parent = transform.parent;
    }

    public void Update()
    {
        if (!_stopped)
        {
            _body.AddForce(0, 0, Speed - _body.velocity.z, ForceMode.VelocityChange);
        }
        else
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beach") && !_stopped) { 
            Destroy(Door.gameObject);
            Instantiate(Prefab, new Vector3(transform.position.x,oriY,transform.position.z) - Vector3.forward*128, Quaternion.Euler(0,0,90));
            _stopped = true;
        }
    }
}

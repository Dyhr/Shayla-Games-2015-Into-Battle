using System.Linq;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Soldier : MonoBehaviour
{
    public float Speed;
    public uint HP = 4;
    private Rigidbody _body;

    public void Start()
    {
        _body = GetComponent<Rigidbody>();

        if (Gun.soldiers == null)
            Gun.soldiers = GameObject.FindGameObjectsWithTag("Soldier").Select(o => o.transform).ToList();
        if(!Gun.soldiers.Contains(transform))Gun.soldiers.Add(transform);
    }

    public void Update()
    {
        _body.AddForce(0, 0, Speed - _body.velocity.z, ForceMode.Force);
    }

    public bool Hit()
    {
        HP--;
        if(HP==0)Destroy(gameObject);
        return HP == 0;
    }
}

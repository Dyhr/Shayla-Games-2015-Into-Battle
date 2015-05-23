using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float FireRate;
    public float Accuracy;

    private Transform target;
    public static List<Transform> soldiers;
    private float timer;
    private float watch_timer;

    public void Start()
    {
        timer = 0;
        watch_timer = Random.value;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        watch_timer -= Time.deltaTime;
        if (watch_timer <= 0)
        {
            watch_timer = 1f;
            if (target == null)
            {
                soldiers = soldiers.OrderBy(x => x.position.z).ToList();
                foreach (var soldier in soldiers)
                {
                    if (soldier == null || Mathf.Abs(soldier.position.x-transform.position.x) > 64) continue;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, (soldier.position - transform.position).normalized, out hit) &&
                        hit.transform == soldier)
                    {
                        target = soldier;
                    }
                }
            }
        }
        if (target != null && timer <= 0)
        {
            timer = 1/FireRate;

            var t = transform.position;
            var dir = (target.position - t).normalized;
            dir = Vector3.RotateTowards(dir, Random.onUnitSphere, Accuracy*Random.value, 1000).normalized;
            Debug.DrawLine(transform.position, transform.position + dir*1000);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit) && hit.transform.CompareTag("Soldier"))
            {
                if (hit.transform.GetComponent<Soldier>().Hit())
                {
                    soldiers.Remove(hit.transform);
                    if (target == hit.transform)
                        target = null;
                }
            }
            else
            {
                target = null;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,3);
    }
}

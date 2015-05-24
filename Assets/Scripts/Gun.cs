using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public float FireRate;
    public float Accuracy;
    public LineRenderer Line;

    private Transform target;
    public static List<Transform> soldiers;
    private float timer;
    private float watch_timer;
    private AudioSource source;
    private uint misses;

    public void Start()
    {
        timer = 0;
        watch_timer = Random.value;
        source = GetComponent<AudioSource>();
        Line.SetPosition(0, transform.position);
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
                    if (soldier == null || Mathf.Abs(soldier.position.x-transform.position.x) > 64 || Random.value < 0.1) continue;
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
            if (!source.isPlaying) source.Play();
            timer = 1/FireRate;

            var t = transform.position;
            var dir = (target.position - t).normalized;
            dir = Vector3.RotateTowards(dir, Random.onUnitSphere, Accuracy*Random.value, 1000).normalized;
            //Debug.DrawLine(transform.position, transform.position + dir*1000);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit) && hit.transform.CompareTag("Soldier"))
            {
                Line.SetPosition(1, hit.point);
                HitPool.instance.Poll(hit.point);
                misses = 0;
                if (hit.transform.GetComponent<Soldier>() != null) { 
                    if (hit.transform.GetComponent<Soldier>().Hit())
                    {
                        soldiers.Remove(hit.transform);
                        if (target == hit.transform)
                            target = null;
                    }
                }
                else if (hit.transform.GetComponent<OVRPlayerController>() != null && Random.value < 0.2)
                {
                    soldiers.Remove(hit.transform);
                    //Destroy(hit.transform.gameObject);
                    hit.transform.GetComponentInChildren<OVRCameraRig>().leftEyeCamera.clearFlags = CameraClearFlags.SolidColor;
                    hit.transform.GetComponentInChildren<OVRCameraRig>().leftEyeCamera.backgroundColor = Color.black;
                    hit.transform.GetComponentInChildren<OVRCameraRig>().rightEyeCamera.clearFlags = CameraClearFlags.SolidColor;
                    hit.transform.GetComponentInChildren<OVRCameraRig>().rightEyeCamera.backgroundColor = Color.black;
                    hit.transform.position = Vector3.forward*10000;
                    OVRPlayerController.move = Vector3.zero;
                }
            }
            else
            {
                Line.SetPosition(1, hit.point);
                HitPool.instance.Poll(hit.point);
                misses++;
                if (misses == 4) target = null;
            }
        }
        else
        {
            Line.SetPosition(1,transform.position);
        }

        if(target == null && source.isPlaying)source.Stop();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,3);
    }
}

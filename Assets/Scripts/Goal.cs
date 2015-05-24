using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    private bool done;
    public Timer timer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OVRPlayerController>() != null && !done)
        {
            done = true;
            transform.parent = other.transform;
            transform.localPosition = Vector3.up*3;
            foreach (var child in GetComponentsInChildren<ParticleSystem>())
            {
                child.Play();
            }
            Timer.instance.StopTimer();
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}

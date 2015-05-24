using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    private bool done;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OVRPlayerController>() != null && !done)
        {
            done = true;
            transform.parent = other.transform;
            transform.localPosition = Vector3.up;
            foreach (var child in GetComponentsInChildren<ParticleSystem>())
            {
                child.Play();
            }
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform cam;
    public AudioSource StepSource;
    private Vector3 dest;
    public void Start()
    {
        foreach (var boat in GameObject.FindGameObjectsWithTag("Boat"))
        {
            boat.GetComponent<Boat>().Ready = true;
        }
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, dest) < 2) OVRPlayerController.move = Vector3.zero;
        if (StepSource.isPlaying && OVRPlayerController.move == Vector3.zero) StepSource.Stop();
        if (!StepSource.isPlaying && OVRPlayerController.move != Vector3.zero) StepSource.Play();
        RaycastHit hit;
        if (Input.GetButton("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit, 100/*, 1 << LayerMask.NameToLayer("Buttons")*/))
            {
                if (hit.transform.GetComponent<FancyButton>() != null)
                {
                    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hit.point, Quaternion.identity);

                    hit.transform.GetComponent<FancyButton>().Press();
                }
                else
                {
                    OVRPlayerController.move = (hit.point-transform.position).normalized*0.25f;
                    dest = hit.point;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class Disable : MonoBehaviour
{
    public float Timer;
    private float _timer;

    public void OnEnable()
    {
        _timer = Timer;
        GetComponent<AudioSource>().Play();
        GetComponent<ParticleSystem>().Play();
    }
    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

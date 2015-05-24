using UnityEngine;
using System.Collections;

public class Disable : MonoBehaviour
{
    public float Timer;
    private float _timer;

    public void Awake()
    {
        _timer = Timer;
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

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class WaterMover : MonoBehaviour
{

    public Vector2 Scroll;

    private MeshRenderer _renderer;
    
    public void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
	public void Update ()
	{
	    _renderer.material.mainTextureOffset += Scroll*Time.deltaTime;
	}
}

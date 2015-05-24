using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{

    public static Fader instance;

	void Start ()
	{
	    instance = this;
	    gameObject.SetActive(false);
	}
}

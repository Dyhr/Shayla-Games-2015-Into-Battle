using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FancyButton))]
public class StartButton : MonoBehaviour
{

    public void Start()
    {
        GetComponent<FancyButton>().Action = () =>
        {
            foreach (var boat in GameObject.FindGameObjectsWithTag("Boat"))
            {
                boat.GetComponent<Boat>().Ready = true;
            }
        };
    }   
}

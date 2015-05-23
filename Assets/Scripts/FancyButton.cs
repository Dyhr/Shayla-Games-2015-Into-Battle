﻿using System;
using UnityEngine;
using System.Collections;

public class FancyButton : MonoBehaviour
{
    public Action Action;

    private Vector3 size;

    public void Start()
    {
        size = Vector3.one;
    }

    public void Update()
    {
        var dir = size - transform.localScale;
        if(dir.magnitude > 1)dir.Normalize();
        transform.localScale = transform.localScale - dir;
    }

    public void Press()
    {
        if (Action != null) { 
            size = Vector3.zero;
            Action();
        }
    }
}

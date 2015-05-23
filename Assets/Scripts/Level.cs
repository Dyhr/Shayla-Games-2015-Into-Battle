using System;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Terrain))]
public class Level : MonoBehaviour
{
    public Vector2 Size;
    public Vector2 Offset;
    public Rigidbody TankTrapPrefab;
    public uint Amount;
    public float Bury;

    private List<Rigidbody> _traps;
    private float time;

    public void Start()
    {
        _traps = new List<Rigidbody>();
        time = 0;
        for (int i = 0; i < Amount; ++i)
            _traps.Add((Rigidbody)Instantiate(TankTrapPrefab, transform.position + new Vector3(Random.value * Size.x, 30, Random.value * Size.y) + new Vector3(Offset.x, 0, Offset.y), Random.rotation));

        var terrain = GetComponent<Terrain>();
        var data = terrain.terrainData;

        var height = new float[data.heightmapWidth, data.heightmapHeight];
        for (int x = 0; x < data.heightmapWidth; ++x)
        {
            for (int y = 0; y < data.heightmapHeight; ++y)
            {
                var a = ((float) x/data.heightmapWidth);
                var b = 1 - a/2;
                height[x, y] = Mathf.PerlinNoise((float) x/120, (float) y/120) * b * Mathf.Min(a+0.1f,1.0f) + a/2;
            }
        }
        data.SetHeights(0,0,height);
        terrain.terrainData = data;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 10) {
            foreach(var trap in _traps)
            {
                var t = trap.transform;
                Destroy(trap);
                t.Translate(0,-Bury,0,Space.World);
            }
            Destroy(this);
        }
    }

    public void OnDrawGizmos()
    {
        var s = new Vector3(Size.x, 30, Size.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + s / 2 + new Vector3(Offset.x, 0, Offset.y), s);
    }
}

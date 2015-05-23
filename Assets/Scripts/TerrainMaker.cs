using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TerrainMaker : MonoBehaviour
{
    public float Size = 10;
    public int Recursion = 7;
    public float[] NoiseAmount = {10};
    public float[] NoiseDensity = {0.001f};

    public void Awake()
    {
        var filter = GetComponent<MeshFilter>();
        var collid = GetComponent<MeshCollider>();

        var vertices = new[]
        {
            new Vector3(Mathf.Cos(0)*Size, 0, Mathf.Sin(0)*Size),
            new Vector3(Mathf.Cos(2*Mathf.PI/3)*Size, 0, Mathf.Sin(2*Mathf.PI/3)*Size),
            new Vector3(Mathf.Cos(4*Mathf.PI/3)*Size, 0, Mathf.Sin(4*Mathf.PI/3)*Size),
        };
        var normals = new[]
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
        };
        var uv = new[]
        {
            Vector2.zero,
            Vector2.zero,
            Vector2.zero,
        };
        var triangles = new[]
        {
            1, 0, 2,
        };

        for (int i = 0; i < Recursion; i++)
            Subdivide(ref vertices, ref normals, ref uv, ref triangles);
        for (int i = 0; i < NoiseAmount.Length; i++)
        {
            Jitter(ref vertices, NoiseAmount[i], NoiseDensity[i]);
        }

        var mesh = new Mesh
        {
            name = "Terrain",
            vertices = vertices,
            normals = normals,
            uv = uv,
            triangles = triangles,
        };

        mesh.RecalculateNormals();
        mesh.Optimize();
        filter.sharedMesh = mesh;
        collid.sharedMesh = mesh;

        Destroy(this);
    }

    private void Jitter(ref Vector3[] verts, float amount, float density)
    {
        var vertHash = new Dictionary<Vector3, int>();
        for (int i = 0; i < verts.Length; i++)
        {
            if (!vertHash.ContainsKey(verts[i]))
            {
                vertHash[verts[i]] = i;
                verts[i] += Vector3.up * (Mathf.PerlinNoise(verts[i].x * density, verts[i].z * density) * amount);
            }
            else
            {
                verts[i] = verts[vertHash[verts[i]]];
            }
        }
    }

    private void Subdivide(ref Vector3[] vertices, ref Vector3[] normals, ref Vector2[] uv, ref int[] triangles)
    {
        var newVertices = new Vector3[vertices.Length * 4];
        var newTriangles = new int[triangles.Length * 4];
        var newNormals = new Vector3[normals.Length * 4];
        var newUv = new Vector2[uv.Length * 4];

        int vc = 0;
        int tc = 0;
        int nc = 0;
        int uc = 0;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            newVertices[vc++] = vertices[triangles[i + 0]];
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 0]], vertices[triangles[i + 1]], 0.5f);
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 0]], vertices[triangles[i + 2]], 0.5f);
            newTriangles[tc++] = vc - 3;
            newTriangles[tc++] = vc - 2;
            newTriangles[tc++] = vc - 1;

            newVertices[vc++] = vertices[triangles[i + 1]];
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 1]], vertices[triangles[i + 2]], 0.5f);
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 1]], vertices[triangles[i + 0]], 0.5f);
            newTriangles[tc++] = vc - 3;
            newTriangles[tc++] = vc - 2;
            newTriangles[tc++] = vc - 1;

            newVertices[vc++] = vertices[triangles[i + 2]];
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 2]], vertices[triangles[i + 0]], 0.5f);
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 2]], vertices[triangles[i + 1]], 0.5f);
            newTriangles[tc++] = vc - 3;
            newTriangles[tc++] = vc - 2;
            newTriangles[tc++] = vc - 1;

            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 0]], vertices[triangles[i + 1]], 0.5f);
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 1]], vertices[triangles[i + 2]], 0.5f);
            newVertices[vc++] = Vector3.Lerp(vertices[triangles[i + 2]], vertices[triangles[i + 0]], 0.5f);
            newTriangles[tc++] = vc - 3;
            newTriangles[tc++] = vc - 2;
            newTriangles[tc++] = vc - 1;

            // Garbage but required
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newNormals[nc++] = Vector3.up;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
            newUv[uc++] = Vector2.zero;
        }

        vertices = newVertices;
        triangles = newTriangles;
        normals = newNormals;
        uv = newUv;
    }
}

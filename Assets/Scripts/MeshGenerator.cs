using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Author: Marting Zier - HTW Berlin 
 *  Editor: Bartholomäus Berresheim, Marie Lencer, Pauline Röhr
 */
public class MeshGenerator : MonoBehaviour
{
    private Mesh generatedMesh = null;
    [SerializeField] private MeshFilter meshfilter = null;
    [SerializeField] private MeshCollider meshcollider = null;
    [Range(1, 5000)] [SerializeField] private int subdivisionSize = 25;

    private enum Surfaces
    {
        Dini,
        Enneper,
        Torus,
        Figure8,
        Flower,
        BoySurface,
        SpiralTorus,
        Butterfly,
        Trefoil
    }

    private Vector2 uBounds;
    private Vector2 vBounds;

    [SerializeField] private Surfaces surfaceType;

    // Dini, Enneper, torus By @Bartholomäus Berresheim
    // Figure8, Flower, BoySurface By @Marie Lencer
    // SpiralTorus, Butterfly, Trefoil By @Pauline Röhr
    
    private void Start()
    {
        switch (surfaceType)
        {
            case Surfaces.Dini:
                uBounds = new Vector2(0, (float) (Math.PI * 4));
                vBounds = new Vector2(0.001f, 2);
                break;
            case Surfaces.Enneper:
                uBounds = new Vector2(0, 1.15f);
                vBounds = new Vector2((float) (-Math.PI), (float) (Math.PI));
                break;
            case Surfaces.Torus:
                uBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                vBounds = new Vector2(.001f, (float) (Math.PI * 2));
                break;
            case Surfaces.Figure8:
                uBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                vBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                break;
            case Surfaces.Flower:
                uBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                vBounds = new Vector2(0.001f, (float) (Math.PI));
                break;
            case Surfaces.BoySurface:
                uBounds = new Vector2((float) (Math.PI / -2), (float) (Math.PI / 2));
                vBounds = new Vector2(0.001f, (float) (Math.PI));
                break;
            case Surfaces.SpiralTorus:
                uBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                vBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                break;
            case Surfaces.Butterfly:
                uBounds = new Vector2(0.001f, (float) (Math.PI * 12));
                vBounds = new Vector2(0.001f, (float) (Math.PI * 2));
                break;
            case Surfaces.Trefoil:
                uBounds = new Vector2((float) -Math.PI, (float) (Math.PI * 3));
                vBounds = new Vector2((float) -Math.PI, (float) (Math.PI * 3));
                break;
        }

        Generate();
    }

    // By @Martin Zier
    void Generate()
     {
         generatedMesh = new Mesh();
         meshfilter.mesh = generatedMesh;
         
         var subdivisions = new Vector2Int(subdivisionSize, subdivisionSize);     
         var vertexSize = subdivisions + new Vector2Int(1, 1);            
                                                                                        
         var vertices = new Vector3[vertexSize.x * vertexSize.y];                     
         var uvs = new Vector2[vertices.Length];                                     

         for (var y = 0; y < vertexSize.y; y++)                            
         {
             var vNormalized = (1f / subdivisions.y) * y;                                
             var v = Mathf.Lerp(vBounds.x, vBounds.y, vNormalized);                     // boundary check

             for (var x = 0; x < vertexSize.x; x++)                                   
             {
                 var uNormalized = (1f / subdivisions.x) * x;                       
                 var u = Mathf.Lerp(uBounds.x, uBounds.y, uNormalized);                  // boundary check  
                 
                 var vertex = CalculateVertex(u,v);                                
                 
                 var uv = new Vector2(u, v);                                       

                 var arrayIndex = x + y * vertexSize.x;

                 vertices[arrayIndex] = vertex;
                 uvs[arrayIndex] = uv;
             }
         }

         var triangles = new int[subdivisions.x * subdivisions.y * 6];

         for (var i = 0; i < subdivisions.x * subdivisions.y; i++)
         {
             var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;

             var indexer = i * 6;

             triangles[indexer + 0] = triangleIndex;
             triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
             triangles[indexer + 2] = triangleIndex + 1;
             
             triangles[indexer + 3] = triangleIndex + 1;
             triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
             triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
         }

         generatedMesh.vertices = vertices;
         generatedMesh.uv = uvs;
         generatedMesh.triangles = triangles;

         generatedMesh.RecalculateBounds();
         generatedMesh.RecalculateNormals();
         generatedMesh.RecalculateTangents();

         meshfilter.mesh= generatedMesh;
         meshcollider.sharedMesh = generatedMesh;
     }
     
    // Dini, Enneper, Torus By @Bartholomäus Berresheim
    // Figure8, Flower, BoySurface By @Marie Lencer
    // SpiralTorus, Butterfly, Trefoil By @Pauline Röhr
    
    Vector3 CalculateVertex(float u, float v)
    {
        float x = u;
        float y = v;
        float z = 0;
        switch (surfaceType)
        {
            case Surfaces.Dini:
                x = (float) (Math.Cos(u) * Math.Sin(v));
                y = (float) (Math.Sin(u) * Math.Sin(v));
                z = (float) (Math.Cos(v) + Math.Log(Math.Tan(v / 2)) + 0.2 * u);
                break;
            case Surfaces.Enneper:
                x = u * (1 - u * u / 3 + v * v) / 3;
                y = v * (1 - v * v / 3 + u * u) / 3;
                ;
                z = (u * u - v * v) / 3;
                break;
            case Surfaces.Torus:
                float R = 1f;
                float r = 0.5f;
                x = (float) (R * Math.Cos(u) + r * Math.Cos(v) * Math.Cos(u));
                y = (float) (R * Math.Sin(u) + r * Math.Cos(v) * Math.Sin(u));
                z = (float) (r * Math.Sin(v));
                break;
            case Surfaces.Figure8:
                float a = 2.77f;
                x = (float) ((a + Math.Cos(u / 2) * Math.Sin(v) - Math.Sin(u / 2) * Math.Sin(2 * v)) * Math.Cos(u));
                y = (float) ((a + Math.Cos(u / 2) * Math.Sin(v) - Math.Sin(u / 2) * Math.Sin(2 * v)) * Math.Sin(u));
                z = (float) (Math.Sin(u / 2) * Math.Sin(v) + Math.Cos(u / 2) * Math.Sin(2 * v));
                break;
            case Surfaces.Flower:
                float q = (float) (2 + Math.Sin(7 * u + 5 * v));
                x = (float) (q * Math.Cos(u) * Math.Sin(v));
                y = (float) (q * Math.Sin(u) * Math.Sin(v));
                z = (float) (q * Math.Cos(v));
                break;
            case Surfaces.BoySurface:
                x = (float) ((Math.Sqrt(2) * Math.Pow(Math.Cos(v), 2) * Math.Cos(2 * u) + Math.Cos(u) * Math.Sin(2 * v))
                             / (2 - Math.Sqrt(2) * Math.Sin(3 * u) * Math.Sin(2 * v)));
                y = (float) ((Math.Sqrt(2) * Math.Pow(Math.Cos(v), 2) * Math.Sin(2 * u) - Math.Sin(u) * Math.Sin(2 * v))
                             / (2 - Math.Sqrt(2) * Math.Sin(3 * u) * Math.Sin(2 * v)));
                z = (float) ((3 * Math.Pow(Math.Cos(v), 2))
                             / (2 - Math.Sqrt(2) * Math.Sin(3 * u) * Math.Sin(2 * v)));
                break;
            case Surfaces.SpiralTorus:
                x = (float) (Math.Cos(v) * (5 - (Math.Sin(3 * u) * Math.Sin(u - 3 * v))));
                y = (float) (Math.Sin(v) * (5 - (Math.Sin(3 * u) * Math.Sin(u - 3 * v))));
                z = (float) (-Math.Cos(u - 3 * v) * (Math.Sin(3 * u)));
                break;
            case Surfaces.Butterfly:
                float part = (float) (Math.Pow(Math.E, Math.Cos(u)) - 2 * Math.Cos(4 * u) -
                                      Math.Pow(Math.Sin(u / 12f), 5));
                float butterflyRadius = 0.1f;
                float twoDimX = (float) (Math.Sin(u) * part);
                float twoDimY = (float) (Math.Cos(u) * part);
                float angle = (float) Math.Atan(twoDimY / twoDimX);
                x = (float) (Math.Sin(angle) * butterflyRadius * Math.Sin(v) + twoDimX);
                y = (float) (Math.Cos(angle) * butterflyRadius * Math.Sin(v) + twoDimY);
                z = (float) (butterflyRadius * Math.Cos(v));
                break;
            case Surfaces.Trefoil:
                float trefoilRadius = 1;
                x = (float) (trefoilRadius * (Math.Sin(u) + 2 * Math.Sin(2 * u)) /
                             (2 + Math.Cos(v + Math.PI * 2 / 3f)));
                y = (float) (trefoilRadius / 2 * (Math.Cos(u) - 2 * Math.Cos(2 * u)) * (2 + Math.Cos(v)) *
                    (2 + Math.Cos(v + Math.PI * 2 / 3f)) / 4);
                z = (float) (trefoilRadius * Math.Sin(3 * u) / (2 + Math.Cos(v)));
                break;
        }

        return new Vector3(x, y, z);
    }
}
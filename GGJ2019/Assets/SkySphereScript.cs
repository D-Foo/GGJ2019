using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySphereScript : MonoBehaviour
{
	void Start ()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;

        Vector3[] norms = mesh.normals;
        for (int i = 0; i < norms.Length; i++)
        {
            norms[i] = -1 * norms[i];
        }

        mesh.normals = norms;

        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] tris = mesh.GetTriangles(i);
            for (int j = 0; j < tris.Length; j += 3)
            {
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }
            mesh.SetTriangles(tris, i);
        }
	}
}

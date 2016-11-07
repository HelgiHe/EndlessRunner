using UnityEngine;
using System.Collections;

public class UV : MonoBehaviour {

	public Vector3[] vertices;
	public Vector2[] uvs;


	void Start() {
		
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		vertices = mesh.vertices; //array af vertices

		uvs = new Vector2[vertices.Length];

		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
		}

		mesh.uv = uvs;
		print (uvs);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_script : MonoBehaviour
{

    Vector3 point_on_plane, normal_to_plane;

    public Vector3 normal { get { return normal_to_plane; }  }

    public Vector3 plane_point;
    public Vector3 plane_normal;

    // Start is called before the first frame update
    void Start()
    {
        define_plane(plane_point, plane_normal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void define_plane(Vector3 point, Vector3 normal)
    {
        point_on_plane = point;
        normal_to_plane = normal.normalized;
        transform.position = point_on_plane;

        transform.up = normal_to_plane;


    }

    internal bool CollidesWith(Sphere_physics sphere)
    {
        return distance_to(sphere.transform.position) < sphere.radius_of_sphere;  
    }

    public float  distance_to(Vector3 point)
    {
        Vector3 point_on_plane_to_point = point - point_on_plane;

        return Vector3.Dot(point_on_plane_to_point, normal_to_plane);

    }
}

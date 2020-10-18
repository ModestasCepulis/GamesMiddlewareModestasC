using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_physics : MonoBehaviour
{
    Plane_script plane;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, -9.8f, 0);

    public float radius_of_sphere;//use transform scale value instead;

    public float Coefficient_of_Restitution = 0.6f;

    public float distance_from_center_to_plane;
    public float time_of_impact;
    public float d2;


    //collisions
    Vector3 difference_between_two_spheres;
    Vector3 sphere_position;

    // Start is called before the first frame update
    void Start()
    {
      //  plane = FindObjectOfType<Plane_script>();

        //unity spheres: diameter = 1
        //so diamater = scale, thus radius = scale / 2.0f;

        radius_of_sphere = transform.localScale.x / 2.0f;

        //collisions
        sphere_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        define_sphere(sphere_position);

    }

    internal bool CollidesWith(Sphere_physics sphere2)
    {
        return Vector3.Distance(transform.position, sphere2.transform.position) < (radius_of_sphere + sphere2.radius_of_sphere);
    }

    // Update is called once per frame
    void Update()
    {
        //float d1 = plane.distance_to(transform.position) - radius_of_sphere;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

/*        distance_from_center_to_plane = plane.distance_to(transform.position);
        d2 = distance_from_center_to_plane - radius_of_sphere;*/

    }

    public Vector3 perpendicular_component(Vector3 vec, Vector3 normal)
    {
        return vec - parallell_component(vec, normal);
    }

    public Vector3 parallell_component(Vector3 vector, Vector3 normal)
    {
        //v*u.v
        return Vector3.Dot(vector, normal) * normal;
    }
    public void define_sphere(Vector3 point)
    {
        transform.position = point;
    }
}

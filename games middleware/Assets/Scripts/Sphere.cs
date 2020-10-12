using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    Plane_script plane;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, -9.8f, 0);

    float radius_of_sphere = 0.5f;//use transform scale value instead;

    float Coefficient_of_Restitution = 0.6f;



    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<Plane_script>();
    }

    // Update is called once per frame
    void Update()
    {
        float d1 = plane.distance_to(transform.position) - radius_of_sphere;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        /*        if ((transform.position.y - 0.5f) < 0f)
                {
                    transform.position -= velocity * Time.deltaTime;
                    velocity = -velocity * 0.5f;
                }*/

        float distance_from_center_to_plane = plane.distance_to(transform.position);
        float d2 = distance_from_center_to_plane - radius_of_sphere;


        if (d2<=0)
        {
            Vector3 para = parallell_component(velocity, plane.normal);
            Vector3 perp = perpendicular_component(velocity, plane.normal);


            float time_of_impact = Time.deltaTime * d1 / (d1 - d2);

            transform.position -= velocity * (Time.deltaTime - time_of_impact);


            velocity = perp - Coefficient_of_Restitution * para;

            transform.position += velocity * (Time.deltaTime - time_of_impact);

            /*            float overlap = radius_of_sphere - distance_from_center_to_plane;
                        transform.position += overlap * plane.normal;*/

            //transform.position -= velocity * Time.deltaTime;

        }

    }

    private Vector3 perpendicular_component(Vector3 vec, Vector3 normal)
    {
        return vec - parallell_component(vec, normal);
    }

    private Vector3 parallell_component(Vector3 vector, Vector3 normal)
    {
        //v*u.v
        return Vector3.Dot(vector, normal) * normal;
    }
}

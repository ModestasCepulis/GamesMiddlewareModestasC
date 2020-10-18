using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Object_Manager : MonoBehaviour
{
    List<Sphere_physics> allSpheres;
    List<Plane_script> allPlanes;

    float differenceAsFloat;
    float S1;
    float S2;
    float S22;
    float SCombined;
    float distance_from_center_to_sphere;
    Vector3 difference;
    Vector3 differenceNormalized;
    float sphereRadiusCombined;

    // Start is called before the first frame update
    void Start()
    {
        allPlanes = FindObjectsOfType<Plane_script>().ToList();
        allSpheres = FindObjectsOfType<Sphere_physics>().ToList();

        
    }

    // Update is called once per frame
    void Update()
    {


        foreach (Sphere_physics sphere in allSpheres)
            foreach (Plane_script plane in allPlanes)
                if (plane.CollidesWith(sphere))
                {
                    Debug.Log("plain collision detected");
                    processCollisionBetween(plane, sphere);
                }

        for (int i = 0; i < allSpheres.Count - 1; i++)
            for (int j = i + 1; i < allSpheres.Count; j++)
            {
                Sphere_physics sphere1 = allSpheres[i], sphere2 = allSpheres[j];

                difference = sphere1.transform.position - sphere2.transform.position;
                differenceAsFloat = difference.sqrMagnitude;
                sphereRadiusCombined = sphere1.radius_of_sphere + sphere2.radius_of_sphere;
                distance_from_center_to_sphere = Vector3.Distance(sphere1.transform.position, sphere2.transform.position);

                S1 = Vector3.Distance(sphere1.transform.position, sphere2.transform.position) - (sphereRadiusCombined);
                S2 = distance_from_center_to_sphere - sphereRadiusCombined / allSpheres.Count;

                if (sphere1.CollidesWith(sphere2))
                {
                    Debug.Log("Spheres Collide");
                    processCollisionBetween(sphere1, sphere2);
                }

            }
    }

    private void processCollisionBetween(Sphere_physics sphere1, Sphere_physics sphere2)
    {


        if (differenceAsFloat <= sphereRadiusCombined)
        {
            Vector3 paraS1 = sphere1.parallell_component(sphere1.velocity, difference);
            Vector3 perpS1 = sphere1.perpendicular_component(sphere1.velocity, difference);

            Vector3 paraS2 = sphere2.parallell_component(sphere2.velocity, difference);
            Vector3 perpS2 = sphere2.perpendicular_component(sphere2.velocity, difference);

            float time_of_impact = Time.deltaTime * (S1 / (S1 - S2));

            //sphere1
            sphere1.transform.position -= sphere1.velocity * (Time.deltaTime - time_of_impact);

            sphere1.velocity = perpS1 - sphere1.Coefficient_of_Restitution * paraS1;
            sphere1.transform.position += sphere1.velocity * (Time.deltaTime - time_of_impact);

            //sphere2
            sphere2.transform.position -= sphere2.velocity * (Time.deltaTime - time_of_impact);

            sphere2.velocity = perpS2 - sphere2.Coefficient_of_Restitution * paraS2;
            sphere2.transform.position += sphere2.velocity * (Time.deltaTime - time_of_impact);
        }
    }

    private void processCollisionBetween(Plane_script plane, Sphere_physics sphere)
    {
        float d1 = plane.distance_to(sphere.transform.position) - sphere.radius_of_sphere;

/*        if ((transform.position.y - 0.5f) < 0f)
        {
            transform.position -= velocity * Time.deltaTime;
            velocity = -velocity * 0.5f;
        }*/

        if (d1 <= 0)
        {
            Vector3 para = sphere.parallell_component(sphere.velocity, plane.normal);
            Vector3 perp = sphere.perpendicular_component(sphere.velocity, plane.normal);

            sphere.distance_from_center_to_plane = plane.distance_to(transform.position);
            sphere.d2 = sphere.distance_from_center_to_plane - sphere.radius_of_sphere;

            sphere.time_of_impact = Time.deltaTime * d1 / (d1 - sphere.d2);

            sphere.transform.position -= sphere.velocity * (Time.deltaTime - sphere.time_of_impact);

            //- gonna changed to + after working out the momentum
            sphere.velocity = perp - sphere.Coefficient_of_Restitution * para;

            sphere.transform.position += sphere.velocity * (Time.deltaTime - sphere.time_of_impact);

            /*            float overlap = radius_of_sphere - distance_from_center_to_plane;
                        transform.position += overlap * plane.normal;*/

            //transform.position -= velocity * Time.deltaTime;
        }
    }
}

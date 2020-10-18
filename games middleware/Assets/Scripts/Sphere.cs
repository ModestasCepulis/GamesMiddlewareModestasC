using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public static Vector3 point_on_sphere, normal_to_sphere;
    GameObject[] ListOfSpheres;
    public Vector3 normal
    {get{return normal_to_sphere;}}

    // Start is called before the first frame update
    void Start()
    {
       ListOfSpheres = GameObject.FindGameObjectsWithTag("Sphere");
    }

    public float distance_to(Vector3 point)
    {
        Vector3 point_on_sphere_to_point = point - point_on_sphere;

        return Vector3.Dot(point_on_sphere_to_point, normal_to_sphere);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

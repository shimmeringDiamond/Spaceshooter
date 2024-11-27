using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid_rotation : MonoBehaviour
{
    public GameObject asteroid;
    private Rigidbody rb;
    private Rigidbody shiprb;

    void Start()
    {
        
        rb = asteroid.GetComponent<Rigidbody>();
        if (asteroid.name == "_asteroid_01(Clone)")
        {
            rb.AddTorque(new Vector3(230, -300f, 100f));
        }
        if (asteroid.name == "_asteroid_02(Clone)")
        {
            rb.AddTorque(new Vector3(-200f, 35000f, 120f));
        }
        if (asteroid.name == "_asteroid_03(Clone)")
        {
            rb.AddTorque(new Vector3(-300f, 210f, -255f));
        }
        if (asteroid.name == "_asteroid_04(Clone)")
        {
            rb.AddTorque(new Vector3(50f, 100f, -30f));
        }
        if (asteroid.name == "_asteroid_05")
        {
            rb.AddTorque(new Vector3(-30, 40f, -130f));
        }
        if (asteroid.name == "_asteroid_06")
        {
            rb.AddTorque(new Vector3(200f, -60f, 95f));
        }
        if (asteroid.name == "_asteroid_07")
        {
            rb.AddTorque(new Vector3(220f, -130f, 70f)); 
        }
        if (asteroid.name == "_asteroid_08")
        {
            rb.AddTorque(new Vector3(-110f, 62f, -100f));
        }
        if (asteroid.name == "_asteroid_09")
        {
            rb.AddTorque(new Vector3(30, 300f, -100f));
        }
        if (asteroid.name == "_asteroid_010")
        {
            rb.AddTorque(new Vector3(-100f, 40f, 23f));
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ship_1(Clone)") {
            shiprb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = shiprb.velocity;
        }
    }
}

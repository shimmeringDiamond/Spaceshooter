using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionTimeHandler : MonoBehaviour
{
    public GameObject self;
    public float radius = 40f;
    public float force = 140f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(boom());
        var allObject = Physics.OverlapSphere(transform.position, radius);

        foreach (var item in allObject)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // make sure that position placeholder of the below function is position of  
                // the object on which script is attached
                rb.AddExplosionForce(force, transform.position, radius);

                // or else weird stuff will happen
                //like this live videos https://youtu.be/6F3AhLpNwec
                // or source link 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator boom()
    {
        yield return new WaitForSeconds(0.7f);
        transform.localScale = new Vector3(0.000001f, 0.000001f, 0.000001f);
        Destroy(self,2);
    }
}

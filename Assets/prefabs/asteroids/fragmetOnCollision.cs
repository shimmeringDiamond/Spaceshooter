using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragmetOnCollision : MonoBehaviour
{
    public GameObject self;
    public Renderer rend;
    public Material damage1;
    public int coolDownTime =1;
    public GameObject sheildBat;
    public GameObject ammoBox;
    public GameObject explosion;

    private Material ogmat;
    private bool breaktime;
    private Vector3 randoffset;
    private GameObject clone1;
    private GameObject clone2;
    private Vector3 vect;
   
    // Start is called before the first frame update
    void Start()
    {
        ogmat = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "player")
        {
            if (collision.gameObject.GetComponentInChildren<camera_movement>().boosteffect == true)
            {
                explode();
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
       if (collision.attachedRigidbody.name == "Capsule(Clone)"){
            Destroy(collision.gameObject);
            if (breaktime)
            {
                explode();
            }

            Debug.Log("ching");
            updateTexture();
        }
    }
    private void updateTexture()
    {
        rend.material = damage1;
        breaktime = true;
        StartCoroutine(coolDown());
    }
    private void explode()
    {
        Destroy(self);

       

       

        randoffset = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        clone2 = Instantiate(sheildBat, self.transform.position + randoffset, Quaternion.identity);

        randoffset = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        clone1 = Instantiate(ammoBox, self.transform.position-randoffset, Quaternion.identity);

        Instantiate(explosion, self.transform.position, Quaternion.identity);

    }
    private IEnumerator coolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        rend.material = ogmat;
        breaktime = false;
        StopCoroutine(coolDown());
    }
    
}

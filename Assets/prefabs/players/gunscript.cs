using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class gunscript : MonoBehaviour
{
    public GameObject laser1;
    public GameObject shootpoint;
    public GameObject ship;
    public Camera mainCam;
    public AudioSource pew;
    public AudioClip pewclip;

    
    private int bulletforce = 1000;
    private int despawnTime = 7;

    private bool raycast;
    private Ray ray;
    private Vector3 origin;
    private RaycastHit hit;

    public Image ammoIndicator;
    public float Maxammo=100f; // if different ships have different ammo counts in the future
    private float previousTime;

    private bool shootBool;
    private bool prevShootBool = false;

    private player_script player;
    // Start is called before the first frame update


    void Start()
    {
        player = ship.GetComponent<player_script>();
        previousTime = Time.time;
        pew.playOnAwake = false;
        //InvokeRepeating("shoot", 0f, 0.5f);
    }


    private void Update()
    {
        if (prevShootBool != shootBool)
        {
            if (shootBool) InvokeRepeating("shoot", 0f, 0.2f);
            else CancelInvoke();
        }
        prevShootBool = shootBool;
    }
    private IEnumerator DespawnBullet(GameObject clone)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(clone);
    }
    public void shootHandler(InputAction.CallbackContext context)
    {
        if (!player.paused) shootBool = context.ReadValue<float>() >= 0.5 ? true : false;

    }
    public void shoot()
    {
        if (ammoIndicator.fillAmount >= 0.001f)
        {
            ammoIndicator.fillAmount -= 1f / Maxammo;

            origin = mainCam.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            raycast = Physics.Raycast(origin, mainCam.transform.forward, out hit, Mathf.Infinity);
            GameObject clone = Instantiate(laser1, shootpoint.transform.position, ship.transform.rotation);
            clone.transform.Rotate(90f, 0f, 0f);
            //clone.GetComponent<Rigidbody>().velocity = new Vector3(ship.GetComponent<player_script>().ActiveForwardSpeed, ship.GetComponent<player_script>().ActiveStrafeSpeed, ship.GetComponent<player_script>().ActiveHoverSpeed );
            Vector3 shootdirection;

            pew.PlayOneShot(pewclip);
            if (raycast)
            {
                Debug.DrawLine(shootpoint.transform.position, hit.point, Color.red, 1f);
                shootdirection = hit.point - shootpoint.transform.position;
                shootdirection = Vector3.Normalize(shootdirection);
                clone.GetComponent<Rigidbody>().velocity = shootdirection * bulletforce;
                StartCoroutine(DespawnBullet(clone));
            }
            else
            {
                ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
                shootdirection = ray.GetPoint(1000) - shootpoint.transform.position;
                shootdirection = shootdirection.normalized;
                clone.GetComponent<Rigidbody>().velocity = shootdirection * bulletforce;
                StartCoroutine(DespawnBullet(clone));
            }
        }
        
        
    }
}
/*
            activehitscan = Instantiate(laser1);
            //activehitscan.transform.parent = transform;
            activehitscan.transform.localPosition = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(0, 10, 0);
            fired = true;

        }
        if ((fired == true) && (despawned == false) )
        {
            //StartCoroutine(TravelBullet());
            activehitscan.transform.position += activehitscan.transform.forward * 30f * Time.deltaTime;
        }
        if (despawned)
        {
            Destroy(activehitscan);
            despawned = false;
        }

        IEnumerator TravelBullet()
        {
            yield return new WaitForSeconds(10f);
            despawned = true;

        }
    }
}
    */
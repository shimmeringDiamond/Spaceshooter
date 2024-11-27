using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class smallShoot : MonoBehaviour
{
    public GameObject shootPoint;
    public GameObject laser;
    public GameObject self;
    private basicEnemy aimingScript;

    private GameObject thisLaser;
    private Vector3 calcVelocity;


    private RaycastHit hit;
    private bool ray;
    private float despawnTime = 5f;

    public float shootdelay= 0.5f;
    private NavMeshAgent agent;
    public AudioSource enemyPew;
    public AudioClip enemyPewClip;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, shootdelay + Random.Range(-0.2f,0.2f));
        agent = GetComponent<NavMeshAgent>();
        aimingScript = self.GetComponentInChildren<basicEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shoot()
    {
        ray = Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, Mathf.Infinity);
        if (ray)
        {
            if (aimingScript.aimingAt != null)
            {
                if (hit.collider.gameObject.name == aimingScript.aimingAt)
                {
                    enemyPew.PlayOneShot(enemyPewClip);
                    thisLaser = Instantiate(laser, shootPoint.transform.position, shootPoint.transform.rotation);
                    calcVelocity = shootPoint.transform.forward.normalized * 300f;
                    thisLaser.GetComponent<Rigidbody>().velocity = calcVelocity;
                    StartCoroutine(DespawnBullet(thisLaser));
                }
            }
        }
        
    }
    private IEnumerator DespawnBullet(GameObject clone)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(clone);
    }
}

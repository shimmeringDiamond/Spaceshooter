using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class basicEnemy : MonoBehaviour
{

    public GameObject self;
    public Rigidbody selfrb;
    public GameObject shootPoint;
    public GameObject laser;

    private Vector3 calcForce;
    private Vector3 playerPos;
    public float forceMulti;

    public int health;

    private GameObject player;
    private Rigidbody playerRb;

    //public GameObject IndicatorHandler;

    private GameObject mothership;
    private Rigidbody mothershipRb;

    public GameObject explosion;
    public string aimingAt = "nothing";
    public GameObject sparks;

    private Vector3 force = new Vector3(0, 0, 1);

    public int scoreValue;
    private TextMeshProUGUI text;

    Vector3 playerDist;
    Vector3 mothershipDist;
    Vector3 selfPos;

    Vector3 boostForce;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        playerRb = player.GetComponent<Rigidbody>();
        text = GameObject.Find("scoreContainer").GetComponentInChildren<TextMeshProUGUI>();

        mothership = GameObject.Find("mothership(Clone)");
        mothershipRb = mothership.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //point ship towards player
        //add force in that direction
        selfPos = selfrb.position;
        playerDist = playerRb.position - selfPos;
        //playerDist =playerRb.position - selfPos;

        if (playerDist.magnitude < 600f)
        {
            boostForce = player.transform.position - selfPos;
            boostForce = boostForce.normalized;
            selfrb.AddForce(boostForce * 20);
            aimingAt = player.name;
        }
        //Debug.Log(playerDist.magnitude);
        if (playerDist.magnitude < 200f)
        {
            selfrb.transform.LookAt(player.transform);
            aimingAt = player.name;
            selfrb.AddRelativeForce(force * forceMulti);
        }
        else
        {
            mothershipDist = mothershipRb.position - selfPos;
            selfrb.transform.LookAt(mothership.transform);
            aimingAt ="Spaceship2";
            if (mothershipDist.magnitude > 200f)
            {
                
                selfrb.AddRelativeForce(force * forceMulti);
            }
            else
            {
                selfrb.velocity = new Vector3(0, 0, 0);
            }
           
        }


    }
    private void OnTriggerEnter(Collider collision)
    {
       if (collision.attachedRigidbody.name == "Capsule(Clone)")
        {
            Debug.Log("hit");
            health -= 1;
            Instantiate(sparks, selfPos, Quaternion.identity);
            Destroy(collision.gameObject);
            if (health <= 0) 
            {
                Instantiate(explosion, selfPos, Quaternion.identity);
                Debug.Log(text.text.Substring(6));
                if (text.text.Length > 7) text.text = "Score : " + (Int32.Parse(text.text.Substring(7)) + scoreValue).ToString();
                else text.text = "Score : " + scoreValue.ToString();
                self.SetActive(false);
                GameObject.Find("enemyIndicating").GetComponent<EnemyIndicatorHandler>().Destroy(self.name);
                

                Debug.Log(text.text);
                Destroy(self);            

            }

        }
    }
}

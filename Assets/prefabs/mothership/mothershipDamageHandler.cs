using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mothershipDamageHandler : MonoBehaviour
{
    public GameObject sparks;
    public GameObject self;
    private Vector3 selfPos;
    private player_script player;

    public float maxHealth;
    private float health;
    private Image HealthBar;

    public GameObject exlosion;
    private GameObject myExplosion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponentInChildren<player_script>();
        health = maxHealth;
        selfPos = self.transform.position;
        HealthBar = GameObject.Find("mothershipHealthContainer").GetComponentInChildren<Image>();
        HealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Instantiate(sparks, collision.transform.position, Quaternion.identity);
        if (collision.attachedRigidbody.tag == "EnemyAmmo")
        {
            //Debug.Log("IVE BEEN SNIPED");
            health-=1;
            HealthBar.fillAmount =health/maxHealth;
            if (health == 0)
            {
                myExplosion = Instantiate(exlosion, selfPos, Quaternion.identity);
                myExplosion.transform.localScale = Vector3.one * 1000;
                Time.timeScale = 0f;
                player.paused = true;
                player.HUD.enabled = false;
                player.GameOver.enabled = true;
                player.endScore.text = "The spaceship died (rip). Your final score Was :" + GameObject.Find("scoreContainer").GetComponentInChildren<TextMeshProUGUI>().text.Substring(7);
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION DETECTED ");
        Instantiate(sparks, collision.transform.position, Quaternion.identity);

    }
}

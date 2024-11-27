using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerupHandler : MonoBehaviour
{
    public string powerupName;
    public GameObject self;
    public float timeoutTime=10;

    private GameObject player;
    private Image ammoCount;
    private Image healthCount;

    public AudioSource collectionSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        player = GameObject.Find("player");
        Debug.Log(collision.attachedRigidbody.name);
        Debug.Log(player.GetComponentInChildren<Rigidbody>().name);
        StartCoroutine(Timeout());
        if(collision.attachedRigidbody.name == player.GetComponentInChildren<Rigidbody>().name)
        {
            if (powerupName == "ammo")
            {
                ammoCount = GameObject.Find("ammoContainer").GetComponentInChildren<Image>();
                ammoCount.fillAmount = 1;
                collectionSound.Play();

                self.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.000001f);
                Destroy(self, 5);
            }
            if (powerupName == "health")
            {
                healthCount = GameObject.Find("healthContainer").GetComponentInChildren<Image>();
                healthCount.fillAmount = 1;
                collectionSound.Play();

                self.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.000001f);
                Destroy(self, 5);

            }
        }

       
    }
    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(timeoutTime);
        Destroy(self);
    }
 
}

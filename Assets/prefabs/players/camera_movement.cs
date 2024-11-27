using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camera_movement : MonoBehaviour
{
    public GameObject ship;
    public bool boosteffect;
    public bool forwardeffect;
    public Camera cam;
    public ParticleSystem trailLeft;
    public ParticleSystem trailRight;
    public bool istriengine;
   public ParticleSystem mainEngine;

    private int enginecount;

    //these 2 variables are passed down from player_controls script. dont define them in this script
    public float boostTimecam;
    private bool boostonce;

    public float subtractor = 0.7f;
    private bool lerpingdone = false;
  
    //boost time is handled in player control script

    public float fov = 60f;
    // Start is called before the first frame update
    void Start()
    {
        subtractor = 0.7f;
        var mainl = trailLeft.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (boosteffect == false)
        {
            
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, 0.01f);
            var mainl2 = trailLeft.main;
            var mainr2 = trailRight.main;
            if (lerpingdone == false)
            {
                subtractor = subtractor-0.01f;
                mainl2.startLifetime = subtractor;
                mainr2.startLifetime = subtractor;
            }
            if (subtractor <= 0.4) {
                lerpingdone = true;
                mainl2.startLifetime = 0.4f;            }
            //trailLeft.Particle.remaininglifetime = trailLeft.Particle.startLifetime - 0.4f;

        }
        //triggeres when player is boosting
        if ((boosteffect))
        {

            //triggers once on player boost event
            if (boostonce == false)
            {
                StartCoroutine(removeboosteffect());
                var mainl = trailLeft.main;
                var mainr = trailRight.main;
               
                mainl.simulationSpeed = 2f;
                mainr.simulationSpeed = 2f;
            }
            boostonce = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov + 10f, 0.2f);
            var mainl2 = trailLeft.main;
            var mainr2 = trailRight.main;
            mainl2.startLifetime = Mathf.Lerp(0.4f, 0.7f, 0.6f);
            mainr2.startLifetime = Mathf.Lerp(0.4f, 0.7f, 0.6f);





        }
        //triggeres when player is going forward without boosting
        if ((forwardeffect) && (boosteffect == false))
        {
            // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov+5f, 0.01f);
        }
        //triggeres when player stops going forward
        if ((forwardeffect == false) && (boosteffect == false))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, 0.2f);

            trailLeft.Stop();
            trailRight.Stop();
            if (istriengine) mainEngine.Stop();



        }
        if ((forwardeffect) || (boosteffect))
        {
            trailRight.Play();
            trailLeft.Play();
            if (istriengine) mainEngine.Play();
           
           
        }


    }
    //triggeres to remove boost effect
    IEnumerator removeboosteffect()
    {

        yield return new WaitForSeconds(boostTimecam );
        boostonce = false;
        lerpingdone = false;
        subtractor = 0.7f;




    }
}

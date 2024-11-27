

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;


public class player_script : MonoBehaviour
{
    public Rigidbody rb;
    public camera_movement camscript;
    public GameObject ship;

    public float ForwardSpeed = 90f, StrafeSpeed = 90f, HoverSpeed = 90f, RollSpeed = 90f, DashSpeed = 200f;
    public float ActiveForwardSpeed, ActiveStrafeSpeed, ActiveHoverSpeed, ActiveRollSpeed;
    private float ForwardAceleration = 2.5f, StrafeAcceleration = 2f, HoverAcceleration = 2f, RollAcceleration = 2f, DashAcceleration = 80f;

    private float LookRateSpeed = 120f;
    private Vector2 LookInput, ScreenCenter, MouseDistance;

    public bool dash;
    public bool dashing;
    public bool debug_movement; // turns of movement for debug purposes

    //define boost time in start method not here it doesnt work otherwise (probably a bug)
    public float boostTime;
    public float boostcooldown;

    private bool controlsdisabled;

    public AudioSource dash_noise;
    public AudioSource oof;

    public Canvas HUD;
    public Canvas PauseMenu;
    public Canvas GameOver;
    public Image health;
    public TextMeshProUGUI endScore;

    public GameObject explosion;

    public Vector3 movement;
    public Vector2 mouseCoords;
    public float rollValue = 0;
    public float hoverValue = 0;
    public bool boostValue;
    private string InputDevice;

    public bool paused = false;
    
    public void updateControls(InputAction.CallbackContext context){
        if (!paused) movement = context.ReadValue<Vector2>();
    }
    public void updateRoll (InputAction.CallbackContext context){
        if (!paused) rollValue = context.ReadValue<float>();
    }
    
    public void updateLook (InputAction.CallbackContext context){
        if (!paused)
        {
            mouseCoords = context.ReadValue<Vector2>();
            InputDevice = context.control.displayName;
            if (context.control.displayName == "Right Stick")
            {
                MouseDistance.x = mouseCoords.x;
                MouseDistance.y = mouseCoords.y;
            }
        }
      
        
    }
    public void updateHover (InputAction.CallbackContext context){
        if (!paused) hoverValue = context.ReadValue<float>();
        
    }
    public void updateBoost (InputAction.CallbackContext context){
        if (!paused) boostValue = context.ReadValue<float>()==1f ? true : false;
    }
    public void pauseHandler()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            PauseMenu.enabled = false;
            HUD.enabled = true;
        } else
        {
            paused = true;
            Time.timeScale = 0f;
            PauseMenu.enabled = true;
            HUD.enabled = false;

        }
        
    }
    public void gotoMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(sceneName: "MainGame");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        paused = false;
        GameOver.enabled = false;
        PauseMenu.enabled = false ;
        oof.playOnAwake = false;
        
        movement = new Vector3(0,0,0);
        //define boost time and boost cooldown here in seconds
        boostTime = 1f;
        boostcooldown = 5f;
        controlsdisabled = false;
        ScreenCenter.x = Screen.width * .5f;
        ScreenCenter.y = Screen.height * .5f;
        dash = true;
        camscript.boostTimecam = boostTime;
        debug_movement = true;
        LookInput = new Vector2(0,0);
        MouseDistance = new Vector2(0,0);


    }
    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.name == "Spaceship2")
        {
            if (dashing)
            {
                StartCoroutine(stopspeed(true));
            }
        }
        controlsdisabled = true;
        ActiveForwardSpeed = -ActiveForwardSpeed + ActiveForwardSpeed * 0.5f;
        ActiveStrafeSpeed = -ActiveStrafeSpeed + ActiveStrafeSpeed * 0.5f;
        StartCoroutine(EnableControls());
        if (collision.gameObject.tag == "Enemy")
        {
            if (!dashing) health.fillAmount -= 0.2f;
            else
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }
       
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody.name == "capsuleRed(Clone)")
        {
            health.fillAmount -= 0.1f;
            Destroy(collision.gameObject);
            oof.Play();
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (InputDevice == "Position")
        {
            MouseDistance.x = (Mouse.current.position.ReadValue().x - ScreenCenter.x) / ScreenCenter.y;
            MouseDistance.y = (Mouse.current.position.ReadValue().y - ScreenCenter.y) / ScreenCenter.y;
            
            MouseDistance = Vector2.ClampMagnitude(MouseDistance, 1f);
        }
        if (health.fillAmount == 0)
        {
            Time.timeScale = 0f;
            paused = true;
            HUD.enabled = false;
            GameOver.enabled = true;
            endScore.text ="Your final score Was :" + GameObject.Find("scoreContainer").GetComponentInChildren<TextMeshProUGUI>().text.Substring(7);
        }
        //if(Input.GetKeyDown(KeyCode.Escape) && debug_movement) debug_movement = false;
        //else if(Input.GetKeyDown(KeyCode.Escape)&& !debug_movement) debug_movement = true;
        if (debug_movement == true)
        {
            

            //LookInput.x = Input.mousePosition.x;
            //LookInput.y = Input.mousePosition.y;

            if (movement.y > 0)
            {
                camscript.forwardeffect = true;
            }
            else
            {
                camscript.forwardeffect = false;
            }
            if (controlsdisabled == false)
            {
                if (dashing == false)
                {
                    ActiveForwardSpeed = Mathf.Lerp(ActiveForwardSpeed, movement.y * ForwardSpeed, ForwardAceleration * Time.deltaTime);
                }

                ActiveRollSpeed = Mathf.Lerp(ActiveRollSpeed, rollValue * RollSpeed, RollAcceleration * Time.deltaTime);
                ActiveStrafeSpeed = Mathf.Lerp(ActiveStrafeSpeed, movement.x * StrafeSpeed, StrafeAcceleration * Time.deltaTime);
                ActiveHoverSpeed = Mathf.Lerp(ActiveHoverSpeed, hoverValue * HoverSpeed, HoverAcceleration * Time.deltaTime);

                
            }
            transform.position += transform.forward * ActiveForwardSpeed * Time.deltaTime;
            transform.position += transform.right * ActiveStrafeSpeed * Time.deltaTime;
            transform.position += transform.up * ActiveHoverSpeed * Time.deltaTime;
            transform.Rotate(-MouseDistance.y * LookRateSpeed * Time.deltaTime, MouseDistance.x * LookRateSpeed * Time.deltaTime, -ActiveRollSpeed * Time.deltaTime * 3, Space.Self);

            if (boostValue && dash) StartCoroutine(dashcountdown());
            if (dashing)
            {
                ActiveForwardSpeed = Mathf.Lerp(ActiveForwardSpeed, ForwardSpeed, ForwardAceleration * Time.deltaTime);
            }

        }
        
    }

    IEnumerator dashcountdown()
    {
        dash = false;
        dashing = true;
        camscript.boosteffect = true;
        ForwardSpeed += DashSpeed;
        ForwardAceleration += DashAcceleration;
        StartCoroutine(stopspeed());
        dash_noise.Play();
        yield return new WaitForSeconds(boostcooldown);
        dash = true;
    }
    IEnumerator stopspeed(bool instant = false)
    {
        if (!instant) yield return new WaitForSeconds(boostTime);
        if (dashing)
        {
            camscript.boosteffect = false;
            ForwardAceleration -= DashAcceleration;
            ForwardSpeed -= DashSpeed;
            dashing = false;
        }
        

       
    }
    IEnumerator EnableControls()
    {
        yield return new WaitForSeconds(1);
        
        controlsdisabled = false;
    }
}
    





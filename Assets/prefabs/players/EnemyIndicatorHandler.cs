using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class EnemyIndicatorHandler : MonoBehaviour
{
    GameObject player;
    Canvas playerCanvas;

    GameObject[] enemies;
    GameObject[] imgObjects;

    public Texture2D tex;

    Renderer[] renderers;
    Vector3[] screenCoords;

    public GameObject spawner;

    public void LogNewEnemies()
    {
        player = GameObject.Find("player");
        playerCanvas = player.GetComponentInChildren<Canvas>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.Where(val => val.activeSelf == true).ToArray();
        enemies = enemies.Where(val => val != null).ToArray();


        //foreach (GameObject enemy in enemies) .Log(enemy.name);
        renderers = new Renderer[enemies.Length];
        imgObjects = new GameObject[enemies.Length];

        for (int i=0; i<enemies.Length; i++)
        {
            renderers[i] = enemies[i].GetComponentInChildren<Renderer>();
            renderers[i].name = enemies[i].name + "_rend";
            MakeNewArrow("arrow" + i.ToString(), i);
        }
        
    }
    public void Destroy(string destroying)
    {
        enemies = enemies.Where(val => val != null).ToArray();
        enemies = enemies.Where(val => val.activeSelf ==true).ToArray();
        
        enemies = enemies.Where(val => val.name != destroying).ToArray();
        //.Log(renderers.Length);
        //.Log(destroying);
        renderers = renderers.Where(val => val.name != destroying + "_rend").ToArray();
        //.Log(renderers.Length);
        Destroy(imgObjects[^1]);
        if (enemies.Length == 0)
        {
            Debug.Log("no more enemies left");
            summonNewWave();
        } else
        {
            foreach (GameObject thing in enemies)
            {
                Debug.Log(thing.name);
            }
        }
    }

    private void Update()
    {
        outputVisibleRenderers();
        if (screenCoords != null){
            for (int i = 0; i < screenCoords.Length; i++)
            {
                imgObjects[i].GetComponent<RectTransform>().position = new Vector2(screenCoords[i].x, screenCoords[i].y-20);
                
            }
            for (int i =0; i < screenCoords.Length - imgObjects.Length; i++)
            {
                Destroy(imgObjects[i]);
            }
        }
        
    }
    private void outputVisibleRenderers()
    {
        if (renderers != null)
        {
            screenCoords = new Vector3[renderers.Length];
            //names = new string[renderers.Length];
            int i = 0;
            foreach (var renderer in renderers)
            {
                if (renderer != null)
                {
                    if (renderer.isVisible)
                    {
                        screenCoords[i] = player.GetComponentInChildren<Camera>().WorldToScreenPoint(renderer.gameObject.transform.position);
                        i++;
                    }
                }
               
            }
        }
       
    } 
    private void MakeNewArrow(string Name, int i)
    {
        imgObjects[i] = new GameObject(Name);
        //imgObject.SetActive(false);
        imgObjects[i].tag ="Arrow";

        RectTransform trans = imgObjects[i].AddComponent<RectTransform>();
        trans.transform.SetParent(playerCanvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(1000f, 0f); // setting position, will be on center
        trans.sizeDelta = new Vector2(15, 20); // custom size

        Image image = imgObjects[i].AddComponent<Image>();
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObjects[i].transform.SetParent(playerCanvas.transform);
    }
    private void summonNewWave()
    {
        foreach (GameObject img in imgObjects) Destroy(img);
        imgObjects = new GameObject[0];
        screenCoords = new Vector3[0];
        spawner.GetComponent<SpawnRandomShip>().NewWave();
        //summon the new set of enemiess
    }
}

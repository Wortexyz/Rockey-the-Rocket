using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int CurrentIndexNo;
    AudioSource audiosource;
    [SerializeField] AudioClip fail, win;
    [SerializeField] ParticleSystem successParticle, failParticle;
    bool isControllable = true ,isCollidable=true;

   
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();

        CurrentIndexNo = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame) 
        {
            isCollidable = !isCollidable;
        }
    } 

    private void OnCollisionEnter(Collision collision)
    {if (!isControllable || !isCollidable) { return; }
       switch( collision.gameObject.tag)
        {
            case "Finish":
               
                audiosource.Stop();
                audiosource.PlayOneShot(win);
                successParticle.Play();
                CrashSequentAction("NextScene");
                break;

            case "Enemy":
                audiosource.Stop(); 
                audiosource.PlayOneShot(fail);
                failParticle.Play();
                CrashSequentAction("RestartTheLevel");
                break;

            case "Safe":
                Debug.Log("Its safe ");
                break;
            default:

                audiosource.Stop();
                audiosource.PlayOneShot(fail);
                failParticle.Play();
                CrashSequentAction("RestartTheLevel");
                break;
        }
    }

    private void CrashSequentAction(string methodName)
    {      
        isControllable=false;
        Invoke(methodName, 2f);
        GetComponent<Movement>().enabled = false;

    }

    private void RestartTheLevel()
    { 
        SceneManager.LoadScene(CurrentIndexNo);
       
    }

    void NextScene()
    {
       
        int nextsceneIndex = CurrentIndexNo + 1;
        if(nextsceneIndex== SceneManager.sceneCountInBuildSettings)
        {
            nextsceneIndex = 0;
           
           
        }
        
        
        SceneManager.LoadScene(nextsceneIndex);
    }


    
   
}

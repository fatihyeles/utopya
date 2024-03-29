using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] ScreenTint screenTint;

    [SerializeField] CameraConfiner cameraConfiner;
    string currentScene;
    AsyncOperation unload;
    AsyncOperation load;

    bool respawnTransition;

     void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;    
    }
    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to, targetPosition));
    }

    internal void Respawn(Vector3 respawnPointPosition, string respawnPointScene)
    {
        respawnTransition = true;

        if(currentScene != respawnPointScene)
        {
            InitSwitchScene(respawnPointScene, respawnPointPosition);
        }
        else
        {
            MoveCharacter(respawnPointPosition);
        }
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);
        SwitchScene(to , targetPosition);
        while (load != null & unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();
       
    }

    public void SwitchScene(string to, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        
        MoveCharacter(targetPosition);
    }

    private void MoveCharacter(Vector3 targetPosition)
    {
        Transform playerTransform = GameManeger.instance.player.transform;
        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();

        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

        playerTransform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            playerTransform.position.z
            );
        if (respawnTransition)
        {
            playerTransform.GetComponent<Character>().FullHeal();
            playerTransform.GetComponent<Character>().FullRest(0);
            playerTransform.GetComponent<DisableControls>().EnableControl();
            respawnTransition = false;
        }
    }
}

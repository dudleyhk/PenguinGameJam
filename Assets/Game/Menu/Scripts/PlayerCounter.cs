﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCounter : MonoBehaviour {

    [SerializeField]
    int numberOfActiveControllers, numberOfLockedInPlayers;
    bool gameStarted;

    public PlayerAdding[] possiblePlayers;

    [SerializeField]
    GameObject penguin;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        { 
            numberOfActiveControllers = 0;
            numberOfLockedInPlayers = 0;

            foreach (PlayerAdding player in possiblePlayers)
            {
                if (player.ControllerActive)
                {
                    numberOfActiveControllers++;
                }

                if (player.lockedIn)
                {
                    numberOfLockedInPlayers++;
                }

            }

            if (numberOfActiveControllers == numberOfLockedInPlayers &&
                numberOfActiveControllers > 1
                )
            {
                //Debug.Log("Ready to start");
                startGame();
            }
            else
            {
                //Debug.Log("Waiting for players . . .");
            }
        }

    }

    void startGame()
    {
        gameStarted = true;

        SceneManager.LoadScene(1);
        
        foreach(PlayerAdding player in possiblePlayers)
        {
            if(player.ControllerActive)
            {
                GameObject newPlayer;
                newPlayer = (GameObject)Instantiate(penguin, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                newPlayer.GetComponent<PlayerData>().PlayerIndex = player.thisIndex;
                DontDestroyOnLoad(newPlayer.gameObject);
            }
        }

    }

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSeal : MonoBehaviour {

    public int rand;
    public float speed;
    public float dist;
    public List<Transform> points;
    public SpriteRenderer fSprite;
    public GameObject silentKilla;

    bool newTarget;
    bool playerDetected = false;
    float smoothTime = 1.0f;
    float fraction = 0;
    Vector3 vel = new Vector3(10.0f,10.0f,10.0f);
    GameObject player;


	// Use this for initialization
	void Start () {
        speed = 0.5f;
        newTarget = true;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (!playerDetected)
        {
            MoveToTarget();
        }

        else if (playerDetected)
        {
            MoveToPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerDetected = false;
            newTarget = true;
        }
    }

    void MoveToPlayer()
    {
        //if this position x > target position = going right & undo Y sprite flip
        if (this.transform.position.x > player.transform.position.x)
        {
            fSprite.flipY = true;
        }
        //if this position x < target position = going left & apply Y sprite flip
        else if (this.transform.position.x < player.transform.position.x)
        {
            fSprite.flipY = false;
        }

        Vector2 dir = player.transform.position - this.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        smoothTime = 1.0f * Time.deltaTime;
        //float step = speed * Time.deltaTime;
        Vector3 targetPos = player.transform.TransformPoint(new Vector3(0, 0, 0));
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, smoothTime);
    }

    void MoveToTarget()
    {
        smoothTime = 1.0f;
        //if this position x > target position = going right & undo Y sprite flip
        if (this.transform.position.x > silentKilla.transform.position.x)
        {
            fSprite.flipY = true;
        }
        //if this position x < target position = going left & apply Y sprite flip
        else if (this.transform.position.x < silentKilla.transform.position.x)
        {
            fSprite.flipY = false;
        }

        //Get direction of player and look at it
        Vector2 dir = silentKilla.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //float step = speed * Time.deltaTime;
        Vector3 targetPos = silentKilla.transform.TransformPoint(new Vector3(0, 0, 0));
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref vel, smoothTime);

        dist = Vector2.Distance(this.transform.position, targetPos);
        if (dist < 30.0f)
        {
            //StopCoroutine(Delay());
            newTarget = true;
        }
    }
    //IEnumerator Delay()
    //{
    //    newTarget = false;
    //    yield return new WaitForSeconds(5);
    //    newTarget = true;
    //}
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class changeColorOnEnter : MonoBehaviour
{
    private Color pre = Color.red;
    private Color post = Color.green;
    Material mMaterial;

    private bool hasBeenGreened = false;
    private AudioSource audd;
    private TaskController taskControllerScript;
    private RecordTrackedAlias aliasControllerScript;

    private void Start()
    {
        mMaterial = GetComponent<Renderer>().material;
        mMaterial.color = pre;
        hasBeenGreened = false;
        aliasControllerScript = GameObject.Find("EventSystem").GetComponent<RecordTrackedAlias>();
        audd = GetComponent<AudioSource>();
        taskControllerScript = transform.parent.parent.GetComponent<TaskController>();
    }


    public void ResetToGreen()
    {
        mMaterial.color = pre;
        hasBeenGreened = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        
        mMaterial.color = post;
        if (hasBeenGreened == false)
        {
            hasBeenGreened = true;
            audd.Play();

            AddObservationToList();
        }
        
    }

    private void AddObservationToList()
    {
        var x = RecordTrackedAlias.Tracked6DString(aliasControllerScript.controllerR.transform);
        var timeString = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var combinedObservation = timeString + "," + x;
        taskControllerScript.taskObservations.Add(combinedObservation);
    }
}
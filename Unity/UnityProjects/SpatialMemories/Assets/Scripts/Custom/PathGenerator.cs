using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    //when grab happens disable the trigger, play audio
    //continually check if audio isPlaying 
    //search for coroutine 
    //vector from here to the 
    // Start is called before the first frame update
    public GameObject[] pathSpheres;
    public GameObject[] oldSpheres;
    public SphereCollider collider;
    public AudioSource audio;
    private bool didIStart = false;
    private bool memoriesAnnihilated = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying)
        {
            //UnityEngine.Debug.Log("ok we are playing");
            didIStart = true;
            AnnihilateOldMemories();
            //also disable trigger
        }

        if (!audio.isPlaying && didIStart)
        {
            UnityEngine.Debug.Log("Ok we are done playing");
            didIStart = false;
            //this means audio is finished
            GeneratePaths();
            //and hide the current memory by getting a reference to self and turning off the mesh renderer
            MeshRenderer m = gameObject.GetComponent<MeshRenderer>();
            m.enabled = false;
        }

    }

    void GeneratePaths()
    {
        for (int i = 0; i < pathSpheres.Length; i++)
        {
            //generate the spheres
            MeshRenderer m = pathSpheres[i].GetComponent<MeshRenderer>();
            m.enabled = true;
        }
    }

    void AnnihilateOldMemories()
    {
        if (!memoriesAnnihilated)
        {
            for (int i = 0; i < oldSpheres.Length; i++)
            {
                //generate the spheres
                MeshRenderer m = oldSpheres[i].GetComponent<MeshRenderer>();
                m.enabled = false;
            }
            memoriesAnnihilated = true;
        }
    }
}

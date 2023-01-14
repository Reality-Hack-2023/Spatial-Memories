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

    private enum PathGeneratorState
    {
        Unplayed,
        PlayingSound,
        ShowingNextPath,
        Done
    }
    private PathGeneratorState pathGeneratorState = PathGeneratorState.Unplayed;
    public GameObject[] pathSpheres;
    public GameObject[] oldSpheres;

    public SphereCollider collider;

    public AudioSource audio;

    private bool didIStart = false;
    private bool memoriesAnnihilated = false;

    private bool fadeOut = false;
    private bool fadeIn = false;

    public float fadeSpeed = 10.0f;

    public float fadeInDuration = 3.0f;

    private float currentFade = 0.0f;

    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0f,0f,0f, 0.0f);
        for (int i = 0; i < pathSpheres.Length; i++)
        {
            Color currentColorMaterial = pathSpheres[i].GetComponent<Renderer>().material.color;
            Color objectColor = new Color(currentColorMaterial.r, currentColorMaterial.g, currentColorMaterial.b, 0.0f);
            pathSpheres[i].GetComponent<Renderer>().material.color = objectColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying && pathGeneratorState == PathGeneratorState.Unplayed)
        {
            UnityEngine.Debug.Log("move to playing sound");
            pathGeneratorState = PathGeneratorState.PlayingSound;
        }

        if (pathGeneratorState == PathGeneratorState.PlayingSound)
        {
            if (!audio.isPlaying)
            {
                UnityEngine.Debug.Log("move to showing path");
                /*//UnityEngine.Debug.Log("ok we are playing");
                didIStart = true;*/
                //AnnihilateOldMemories();
                //also disable trigger
                pathGeneratorState = PathGeneratorState.ShowingNextPath;
            }
        }

        if (pathGeneratorState == PathGeneratorState.ShowingNextPath)
        {
            //GeneratePaths();

            //UnityEngine.Debug.Log("Showing path");

            currentFade += Time.deltaTime;

            float lerpedAlpha = Mathf.Lerp(0.0f, 1.0f, currentFade / fadeInDuration);

            UnityEngine.Debug.Log("Lerped alpha is:" + lerpedAlpha);

            for (int i = 0; i < pathSpheres.Length; i++)
            {
                Color currentColorMaterial = pathSpheres[i].GetComponent<Renderer>().material.color;
                Color objectColor = new Color(currentColorMaterial.r, currentColorMaterial.g, currentColorMaterial.b, lerpedAlpha);
                pathSpheres[i].GetComponent<Renderer>().material.color = objectColor;
            }

            if (lerpedAlpha >= 1.0f)
            {
                UnityEngine.Debug.Log("Done with path");
                pathGeneratorState = PathGeneratorState.Done;
            }

        }


        /*if (!audio.isPlaying && didIStart)
        {
            UnityEngine.Debug.Log("Ok we are done playing");
            didIStart = false;
            //this means audio is finished
            GeneratePaths();
            //and hide the current memory by getting a reference to self and turning off the mesh renderer
            MeshRenderer m = gameObject.GetComponent<MeshRenderer>();
            m.enabled = false;
        }*/

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

        /*public void FadeOutObject() {
        fadeOut = true;

    public void FadeInObject() {
        fadeIn = true;*/
    
}

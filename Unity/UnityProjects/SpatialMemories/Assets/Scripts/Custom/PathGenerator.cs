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
    public GameObject ringo;

    public GameObject nextMemory;

    public BoxCollider collider;

    public AudioSource audio;

    public float fadeSpeed = 5.0f;

    public float fadeInDuration = 3.0f;

    private float currentFade = 0.0f;

    void Start()
    {
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(0f,0f,0f, 0.0f);
        for (int i = 0; i < pathSpheres.Length; i++)
        {
            Color currentColorMaterial = pathSpheres[i].GetComponent<Renderer>().material.color;
            Color objectColor = new Color(currentColorMaterial.r, currentColorMaterial.g, currentColorMaterial.b, 0.0f);
            pathSpheres[i].GetComponent<Renderer>().material.color = objectColor;
        }
        Color currentRingoColor = ringo.GetComponent<Renderer>().material.color;
        Color ringoColor = new Color(currentRingoColor.r, currentRingoColor.g, currentRingoColor.b, 0.0f);
        ringo.GetComponent<Renderer>().material.color = ringoColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying && pathGeneratorState == PathGeneratorState.Unplayed)
        {
            //UnityEngine.Debug.Log("move to playing sound");
            pathGeneratorState = PathGeneratorState.PlayingSound;
            BoxCollider sphere = gameObject.GetComponent<BoxCollider>();
            sphere.enabled = false;
        }

        if (pathGeneratorState == PathGeneratorState.PlayingSound)
        {
            if (!audio.isPlaying)
            {
                //UnityEngine.Debug.Log("move to showing path");
                MeshRenderer m = gameObject.GetComponent<MeshRenderer>();
                m.enabled = false;
                //BoxCollider box = gameObject.GetComponent<BoxCollider>();
                //box.enabled = false;

                for (int i = 0; i < oldSpheres.Length; i++)
                {
                    //generate the spheres
                    MeshRenderer sphereM = oldSpheres[i].GetComponent<MeshRenderer>();
                    sphereM.enabled = false;
                }

                pathGeneratorState = PathGeneratorState.ShowingNextPath;
            }
        }

        if (pathGeneratorState == PathGeneratorState.ShowingNextPath)
        {
            currentFade += Time.deltaTime;

            float lerpedAlpha = Mathf.Lerp(0.0f, 1.0f, currentFade / fadeInDuration);

            //UnityEngine.Debug.Log("Lerped alpha is:" + lerpedAlpha);

            for (int i = 0; i < pathSpheres.Length; i++)
            {
                Color currentColorMaterial = pathSpheres[i].GetComponent<Renderer>().material.color;
                Color objectColor = new Color(currentColorMaterial.r, currentColorMaterial.g, currentColorMaterial.b, lerpedAlpha);
                pathSpheres[i].GetComponent<Renderer>().material.color = objectColor;
            }

            Color currentRingoColor = ringo.GetComponent<Renderer>().material.color;
            Color ringoColor = new Color(currentRingoColor.r, currentRingoColor.g, currentRingoColor.b, lerpedAlpha);
            ringo.GetComponent<Renderer>().material.color = ringoColor;

            if (lerpedAlpha >= 1.0f)
            {
                //UnityEngine.Debug.Log("Done with path");
                if (nextMemory) {
                    MeshRenderer nextM = nextMemory.GetComponent<MeshRenderer>();
                    nextM.enabled = true;
                }
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

    /*void GeneratePaths()
    {
        for (int i = 0; i < pathSpheres.Length; i++)
        {
            //generate the spheres
            MeshRenderer m = pathSpheres[i].GetComponent<MeshRenderer>();
            m.enabled = true;
        }
    }*/

        /*public void FadeOutObject() {
        fadeOut = true;

    public void FadeInObject() {
        fadeIn = true;*/
    
}

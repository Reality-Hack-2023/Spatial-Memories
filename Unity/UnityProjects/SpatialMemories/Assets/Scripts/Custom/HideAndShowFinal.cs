using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShowFinal : MonoBehaviour
{
    public GameObject[] finalParent;
    public GameObject canvas;
    public AudioSource audio;

    private enum HideAndShowState
    {
        Unplayed,
        Unshown,
        Shown
    }

    private HideAndShowState hideAndShowState = HideAndShowState.Unplayed;
    //foreach(Renderer r in gunGameObject.GetComponentsInChildren<Renderer>()) { r.enabled = false; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.Log(audio.isPlaying);
        if (audio.isPlaying && hideAndShowState == HideAndShowState.Unplayed)
        {
            UnityEngine.Debug.Log("moving to unshown");
            hideAndShowState = HideAndShowState.Unshown;
        }

        if (!audio.isPlaying && hideAndShowState == HideAndShowState.Unshown)
        {
            UnityEngine.Debug.Log("moving to shown");
            RenderFinalObjects();
            hideAndShowState = HideAndShowState.Shown;
        }
    }

    void RenderFinalObjects()
    {
        canvas.SetActive(true);
        for (int i = 0; i < finalParent.Length; i++) {
            UnityEngine.Debug.Log("kk each one");
            MeshRenderer m = finalParent[i].GetComponent<MeshRenderer>();
            UnityEngine.Debug.Log(m);
            m.enabled = true;
        }
        //foreach (MeshRenderer r in finalParent.GetComponent<MeshRenderer>()) { r.enabled = true; }
    }
}

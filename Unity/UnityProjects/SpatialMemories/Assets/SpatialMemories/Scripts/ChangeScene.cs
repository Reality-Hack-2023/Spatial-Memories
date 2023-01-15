using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

// changing scenes with same script
public class ChangeScene : MonoBehaviour
{   
    public void MoveToScene(int sceneID)
    { 
        SceneManager.LoadScene(1); 
    }
   
}

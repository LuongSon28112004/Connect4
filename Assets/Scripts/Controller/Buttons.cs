using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
     

    void Start()
    {
      Debug.Log("tren "+ gameObject.name);
    }
    public void LoadLevelButton(int x)
    {  
		  UnityEngine.Application.LoadLevel (x);
    }
 
    public void ExitButton()
    {
      SceneManager.LoadScene(0);
    }
}

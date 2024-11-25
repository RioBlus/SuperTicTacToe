using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonClick()
    {
        SceneManager.LoadScene(0);
    }
    
}

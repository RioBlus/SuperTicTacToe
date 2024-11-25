using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    float cnt = 0;
    void Start()
    {
        
    }
    private void Update()
    {

        RectTransform nw = GetComponent<RectTransform>();
        if (cnt < 500)
        {
            float deltatime = Time.deltaTime;
            cnt+= deltatime*1000;
            nw.Translate(new Vector3(0, 1000, 0) * deltatime);
        }
    }
    public void ButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void exitgame()
    {
        Application.Quit();
    }
}

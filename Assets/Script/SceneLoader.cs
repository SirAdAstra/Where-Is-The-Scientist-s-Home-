using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public void Настройки()
  {
        SceneManager.LoadScene(2);
  }
  public void Старт()
    {
        SceneManager.LoadScene(4);
    }
    public void Назадвменю()
  {
        SceneManager.LoadScene(1);
  }   
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    
    public void Правила()
    {
        SceneManager.LoadScene(3);
    }    
    
}

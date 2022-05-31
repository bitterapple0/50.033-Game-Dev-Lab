using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    private PlayerController playerScript;
    private Button buttonComponent;
    void Awake()
    {
        Time.timeScale = 0.0f;
        playerScript = FindObjectOfType<PlayerController>();
    }

      public void StartButtonClicked()
  {
      if(playerScript.restartFlag) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      foreach (Transform eachChild in transform)
      {
          if (eachChild.name != "Score")
          {
              eachChild.gameObject.SetActive(false);
              Time.timeScale = 1.0f;
          }
      }
  }
    // Start is called before the first frame update
    void Start()
    {
         buttonComponent = GameObject.Find("Start/Restart").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart(){
        buttonComponent.GetComponentInChildren<Text>().text = "Restart";
        foreach (Transform eachChild in transform)
      	{	
          if (eachChild.name != "Score")
          {
              eachChild.gameObject.SetActive(true);
          }
      	}
    }
}

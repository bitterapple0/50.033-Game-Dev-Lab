using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public PlayerController playerScript;
    public EnemyController enemyScript;
    void Awake()
    {
        Time.timeScale = 0.0f;
        playerScript = FindObjectOfType<PlayerController>();
        enemyScript = FindObjectOfType<EnemyController>();
    }

      public void StartButtonClicked()
  {
      if (playerScript != null) playerScript.Restart();
      if (enemyScript != null) enemyScript.Restart();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart(){
        foreach (Transform eachChild in transform)
      	{	
          if (eachChild.name != "Score")
          {
              eachChild.gameObject.SetActive(true);
          }
      	}
    }
}

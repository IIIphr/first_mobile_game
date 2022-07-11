using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_handler_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void start_game()
    {
        SceneManager.LoadScene("game_scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

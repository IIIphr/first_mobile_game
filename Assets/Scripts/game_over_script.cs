using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void try_again()
    {
        SceneManager.LoadScene("game_scene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}


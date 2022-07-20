using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class menu_handler_script : MonoBehaviour
{
    [SerializeField] GameObject[] main_menu_items;
    [SerializeField] GameObject[] settings_menu_items;
    [SerializeField] TextMeshProUGUI gold_text;
    int gold_amount;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < main_menu_items.Length; i++)
        {
            main_menu_items[i].SetActive(true);
        }
        for (int i = 0; i < settings_menu_items.Length; i++)
        {
            settings_menu_items[i].SetActive(false);
        }
        gold_amount = PlayerPrefs.GetInt("gold", 0);
        set_gold_text();
    }

    void set_gold_text()
    {
        gold_text.text = "gold: " + gold_amount;
    }

    public void load_main()
    {
        for (int i = 0; i < main_menu_items.Length; i++)
        {
            main_menu_items[i].SetActive(true);
        }
        for (int i = 0; i < settings_menu_items.Length; i++)
        {
            settings_menu_items[i].SetActive(false);
        }
    }

    public void load_settings()
    {
        for (int i = 0; i < main_menu_items.Length; i++)
        {
            main_menu_items[i].SetActive(false);
        }
        for (int i = 0; i < settings_menu_items.Length; i++)
        {
            settings_menu_items[i].SetActive(true);
        }
    }

    public void start_game()
    {
        SceneManager.LoadScene("game_scene");
    }

    public void set_hard()
    {
        PlayerPrefs.SetInt("difficulty", 2);
    }
    public void set_medium()
    {
        PlayerPrefs.SetInt("difficulty", 1);
    }
    public void set_easy()
    {
        PlayerPrefs.SetInt("difficulty", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

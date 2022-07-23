using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class deck_handler_script : MonoBehaviour
{
    [SerializeField] GameObject red_field;
    [SerializeField] GameObject green_field;
    [SerializeField] GameObject blue_field;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TextMeshProUGUI gold_text;
    int gold_amount;

    // Start is called before the first frame update
    void Start()
    {
        message.text = "";
        set_fields();
        gold_amount = PlayerPrefs.GetInt("gold", 0);
        set_gold_text();
    }

    void set_gold_text()
    {
        gold_text.text = "gold: " + gold_amount;
    }

    void set_fields()
    {
        string[] deck = PlayerPrefs.GetString("deck", "red,red,red,green,green,green,blue,blue,blue").Split(",");
        int red_count = 0, blue_count = 0, green_count = 0;
        for (int i = 0; i < deck.Length; i++)
        {
            if (deck[i] == "red")
            {
                red_count++;
            }
            else if (deck[i] == "blue")
            {
                blue_count++;
            }
            else if (deck[i] == "green")
            {
                green_count++;
            }
        }
        red_field.GetComponent<TMP_InputField>().text = "" + red_count;
        green_field.GetComponent<TMP_InputField>().text = "" + green_count;
        blue_field.GetComponent<TMP_InputField>().text = "" + blue_count;
    }

    public void apply_deck()
    {
        int red_count = int.Parse(red_field.GetComponent<TMP_InputField>().text);
        int green_count = int.Parse(green_field.GetComponent<TMP_InputField>().text);
        int blue_count = int.Parse(blue_field.GetComponent<TMP_InputField>().text);
        if (gold_amount < 50)
        {
            message.text = "not enough gold";
        }
        else if(red_count + blue_count + green_count != 9)
        {
            message.text = "invalid combination";
        }
        else
        {
            bool flag = false;
            string temp = "";
            for(int i=0; i<red_count; i++)
            {
                if (!flag)
                {
                    temp += "red";
                    flag = true;
                }
                else
                {
                    temp += ",red";
                }
            }
            for (int i = 0; i < green_count; i++)
            {
                if (!flag)
                {
                    temp += "green";
                    flag = true;
                }
                else
                {
                    temp += ",green";
                }
            }
            for (int i = 0; i < blue_count; i++)
            {
                if (!flag)
                {
                    temp += "blue";
                    flag = true;
                }
                else
                {
                    temp += ",blue";
                }
            }
            PlayerPrefs.SetString("deck", temp);
            PlayerPrefs.SetInt("gold", gold_amount - 50);
            gold_amount = PlayerPrefs.GetInt("gold", 0);
            message.text = "applied!";
            set_fields();
            set_gold_text();
        }
    }

    public void add_g()
    {
        PlayerPrefs.SetInt("gold", gold_amount + 100);
        gold_amount = PlayerPrefs.GetInt("gold", 0);
        set_gold_text();
    }

    public void main_menu()
    {
        SceneManager.LoadScene("main_menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemy_script : MonoBehaviour
{
    [SerializeField] Image health_bar;
    [SerializeField] TextMeshProUGUI health_number;
    [SerializeField] float max_hp = 10;
    [SerializeField] ArrayList used_pile = new ArrayList();
    [SerializeField] ArrayList deck_pile = new ArrayList();
    bool did_deck_filled = false;
    float hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        set_health_text();
    }

    public void fill_deck()
    {
        string[] cards = PlayerPrefs.GetString("enemy_deck", "red,red,red,red,blue,blue,blue,blue,blue").Split(",");
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject temp = Instantiate(transform.parent.GetComponent<enemy_cont_script>()
            .get_card_template());
            temp.GetComponent<swipe_controller>().set_can_spawn(false);
            temp.GetComponent<swipe_controller>().set_can_move(false);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 0;
            if (cards[i] == "red")
            {
                temp.GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<enemy_cont_script>()
                    .red_card;
            }
            else if (cards[i] == "green")
            {
                temp.GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<enemy_cont_script>()
                    .green_card;
            }
            else if (cards[i] == "blue")
            {
                temp.GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<enemy_cont_script>()
                    .blue_card;
            }
            deck_pile.Add(temp);
        }
        did_deck_filled = true;
    }

    public void set_health_text()
    {
        health_number.text = hp + "/" + max_hp;
    }

    public void do_turn()
    {
        if(!did_deck_filled)
        {
            fill_deck();
        }
        if (transform.parent.GetComponent<enemy_cont_script>().is_spell_full())
        {
            transform.parent.GetComponent<enemy_cont_script>().do_spell();
        }
        else if (deck_pile.Count > 0)
        {
            int rand_index = Random.Range(0, deck_pile.Count);
            this.transform.parent.GetComponent<enemy_cont_script>()
                .add_card((GameObject)deck_pile[rand_index]);
            used_pile.Add(deck_pile[rand_index]);
            deck_pile.RemoveAt(rand_index);
        }
        else
        {
            for(int i=0; i<used_pile.Count; i++)
            {
                deck_pile.Add(used_pile[i]);
            }
            used_pile.Clear();
        }
    }

    public void set_max_hp(float number)
    {
        hp = number;
        max_hp = number;
    }

    public void die()
    {
        this.gameObject.GetComponentInParent<enemy_cont_script>()
            .died(this.gameObject);
        this.gameObject.SetActive(false);
        //memory leak
        deck_pile.Clear();
        used_pile.Clear();
    }

    public void damaged(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            die();
        }
        health_bar.fillAmount = hp / max_hp;
        set_health_text();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

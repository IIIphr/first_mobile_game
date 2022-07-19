using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cont_script : MonoBehaviour
{
    [SerializeField] GameObject enemy_template;
    [SerializeField] GameObject card_container;
    [SerializeField] GameObject spawn_button;
    [SerializeField] GameObject game_handler;
    [SerializeField] Sprite red_card;
    [SerializeField] Sprite blue_card;
    [SerializeField] Sprite green_card;
    [SerializeField] GameObject first_spell_ing;
    [SerializeField] GameObject second_spell_ing;
    [SerializeField] GameObject third_spell_ing;
    GameObject card_at_first_ing = null;
    GameObject card_at_second_ing = null;
    GameObject card_at_third_ing = null;
    bool is_first_full = false;
    bool is_second_full = false;
    bool is_third_full = false;
    int difficulty = 1;
    ArrayList current_enemies = new ArrayList();
    float reg_hp = 10;
    float multiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void damage_to_player(float damage)
    {
        game_handler.GetComponent<game_handler_script>()
            .damage_to_player(damage);
    }

    public void pass_turn()
    {
        if (current_enemies.Count > 0)
        {
            ((GameObject)current_enemies[0])
                .GetComponent<enemy_script>()
                .do_turn();
        }
        else
        {
            print("no enemy");
        }
    }

    Vector3 new_card_pos(GameObject card)
    {
        if (is_first_full)
        {
            if (is_second_full)
            {
                if (is_third_full)
                {
                    return Vector3.zero;
                }
                card_at_third_ing = card;
                is_third_full = true;
                return third_spell_ing.transform.position;
            }
            card_at_second_ing = card;
            is_second_full = true;
            return second_spell_ing.transform.position;
        }
        card_at_first_ing = card;
        is_first_full = true;
        return first_spell_ing.transform.position;
    }

    int get_color(GameObject card)
    {
        if (card.GetComponent<SpriteRenderer>().sprite == red_card)
        {
            return 1;
        }
        if (card.GetComponent<SpriteRenderer>().sprite == green_card)
        {
            return 2;
        }
        if (card.GetComponent<SpriteRenderer>().sprite == blue_card)
        {
            return 3;
        }
        return 0;
    }

    void spell_action(Vector3 spell)
    {
        if (spell == new Vector3(1, 1, 1))
        {
            damage_to_player(3 * multiplier);
            multiplier = 1;
        }
        else if (spell == new Vector3(1, 1, 3) || spell == new Vector3(1, 3, 1) || spell == new Vector3(3, 1, 1))
        {
            damage_to_player(2 * multiplier);
            multiplier = 1;
        }
        else if (spell == new Vector3(1, 3, 3) || spell == new Vector3(3, 3, 1) || spell == new Vector3(3, 1, 3))
        {
            damage_to_player(1 * multiplier);
            multiplier = 1;
        }
        else if (spell == new Vector3(3, 3, 3))
        {
            multiplier *= 2;
        }
    }

    void do_spell()
    {
        Vector3 spell = new Vector3(
            get_color(card_at_first_ing),
            get_color(card_at_second_ing),
            get_color(card_at_third_ing)
            );
        spell_action(spell);
        is_first_full = false;
        is_second_full = false;
        is_third_full = false;
        Destroy(card_at_first_ing);
        Destroy(card_at_second_ing);
        Destroy(card_at_third_ing);
        card_at_first_ing = null;
        card_at_second_ing = null;
        card_at_third_ing = null;
    }

    public void add_card(string card)
    {
        GameObject temp = Instantiate(card_container.GetComponent<card_container_script>()
                .card_template);
        Vector3 pos = new_card_pos(temp);
        if (pos != Vector3.zero)
        {
            temp.GetComponent<swipe_controller>().set_rest_pos(pos);
            temp.GetComponent<swipe_controller>().set_can_spawn(false);
            temp.GetComponent<swipe_controller>().set_can_move(false);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 0;
            if (card == "red")
            {
                temp.GetComponent<SpriteRenderer>().sprite = red_card;
            }
            else if (card == "green")
            {
                temp.GetComponent<SpriteRenderer>().sprite = green_card;
            }
            else if (card == "blue")
            {
                temp.GetComponent<SpriteRenderer>().sprite = blue_card;
            }
            temp.SetActive(true);
        }
        else
        {
            do_spell();
        }
    }

    public void set_diffculty(int diff)
    {
        difficulty = diff;
        reg_hp = difficulty == 0 ? 5 : (difficulty == 1 ? 10 : 20);
    }

    public void start_spawning()
    {
        add_enemy();
    }

    public void add_enemy()
    {
        GameObject temp = Instantiate(enemy_template);
        temp.transform.SetParent(this.gameObject.transform);
        temp.GetComponent<enemy_script>().set_max_hp(reg_hp);
        temp.SetActive(true);
        current_enemies.Add(temp);
        spawn_button.SetActive(false);
    }

    public void died(GameObject enemy)
    {
        current_enemies.Remove(enemy);
        if(current_enemies.Count == 0)
        {
            spawn_button.SetActive(true);
        }
    }

    public void deal_damage(float damage)
    {
        if(current_enemies.Count > 0)
        {
            ((GameObject)current_enemies[0])
                .GetComponent<enemy_script>()
                .damaged(damage);
        }
        else
        {
            print("no enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

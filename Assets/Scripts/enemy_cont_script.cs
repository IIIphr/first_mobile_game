using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cont_script : MonoBehaviour
{
    [SerializeField] GameObject enemy_template;
    [SerializeField] GameObject card_container;
    [SerializeField] GameObject spawn_button;
    [SerializeField] GameObject game_handler;
    int difficulty = 1;
    ArrayList current_enemies = new ArrayList();
    float reg_hp = 10;

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

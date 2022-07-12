using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_handler_script : MonoBehaviour
{
    [SerializeField] GameObject cards_container;
    [SerializeField] GameObject enemies_container;
    [SerializeField] float player_max_hp = 10;
    float player_hp = 10;
    int difficulty = 1;
    // 0 : easy
    // 1 : medium
    // 2 : hard

    // Start is called before the first frame update
    void Start()
    {
        player_hp = player_max_hp;
        difficulty = PlayerPrefs.GetInt("difficulty", 1);
        cards_container.GetComponent<card_container_script>().fill_deck(5);
        enemies_container.GetComponent<enemy_cont_script>().set_diffculty(difficulty);
        enemies_container.GetComponent<enemy_cont_script>().start_spawning();
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void pass_turn()
    {
        enemies_container.GetComponent<enemy_cont_script>().pass_turn();
    }

    public void damage_to_player(float damage)
    {
        player_hp -= damage;
        if(player_hp <= 0)
        {
            SceneManager.LoadScene("game_over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

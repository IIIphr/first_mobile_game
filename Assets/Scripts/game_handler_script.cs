using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_handler_script : MonoBehaviour
{
    [SerializeField] GameObject cards_container;
    [SerializeField] GameObject enemies_container;

    // Start is called before the first frame update
    void Start()
    {
        cards_container.GetComponent<card_container_script>().fill_deck(5);
        enemies_container.GetComponent<enemy_cont_script>().add_enemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

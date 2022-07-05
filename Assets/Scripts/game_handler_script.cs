using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_handler_script : MonoBehaviour
{
    [SerializeField] GameObject cards_container;

    // Start is called before the first frame update
    void Start()
    {
        cards_container.GetComponent<card_container_script>().fill_deck(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

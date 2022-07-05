using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_container_script : MonoBehaviour
{
    [SerializeField] GameObject card_template;
    ArrayList cards = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void spawn_card_at_pos(Vector3 pos)
    {
        GameObject temp = Instantiate(card_template);
        temp.transform.SetParent(transform, false);
        temp.GetComponent<swipe_controller>().set_rest_pos(pos);
        temp.GetComponent<swipe_controller>().set_can_spawn(false);
        cards.Add(temp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

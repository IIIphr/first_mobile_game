using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_container_script : MonoBehaviour
{
    [SerializeField] GameObject card_template;
    [SerializeField] Vector3 deck_rest_pos = Vector3.zero;
    ArrayList hand_cards = new ArrayList();
    ArrayList discard_pile = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void fill_deck(int number_of_cards)
    {
        Vector3 position = deck_rest_pos;
        int order = number_of_cards - 1;
        for(int i=0; i<number_of_cards; i++)
        {
            if (i == 0)
            {
                spawn_card_at_pos(position, true, order, true);
            }
            else
            {
                spawn_card_at_pos(position, true, order, false);
            }
            position += new Vector3(-0.1f, 0, 0);
            order--;
        }
    }

    public void spawn_card_at_pos(Vector3 pos, bool active, int order = 0, bool can_move = false)
    {
        GameObject temp = Instantiate(card_template);
        temp.transform.SetParent(transform, false);
        temp.GetComponent<swipe_controller>().set_rest_pos(pos);
        temp.GetComponent<swipe_controller>().set_can_spawn(false);
        temp.GetComponent<swipe_controller>().set_can_move(can_move);
        temp.GetComponent<SpriteRenderer>().sortingOrder = order;
        temp.SetActive(active);
        hand_cards.Add(temp);
    }

    public void used(GameObject card)
    {
        hand_cards.Remove(card);
        discard_pile.Add(card);
        card.SetActive(false);
        card.GetComponent<swipe_controller>().set_can_move(false);
        for (int i = 0; i < hand_cards.Count; i++)
        {
            ((GameObject)hand_cards[i]).GetComponent<swipe_controller>()
                .set_rest_pos(
                    ((GameObject)hand_cards[i]).GetComponent<swipe_controller>()
                        .get_rest_pos() + new Vector3(0.1f, 0, 0)
                );
        }
        if (hand_cards.Count > 0)
        {
            StartCoroutine(can_move_after_delay(0.001f));
        }
        else
        {
            shuffle_back();
        }
    }

    public void discarded(GameObject card)
    {
        hand_cards.Remove(card);
        discard_pile.Add(card);
        card.SetActive(false);
        card.GetComponent<swipe_controller>().set_can_move(false);
        for (int i = 0; i < hand_cards.Count; i++)
        {
            ((GameObject)hand_cards[i]).GetComponent<swipe_controller>()
                .set_rest_pos(
                    ((GameObject)hand_cards[i]).GetComponent<swipe_controller>()
                        .get_rest_pos() + new Vector3(0.1f, 0, 0)
                );
        }
        if (hand_cards.Count > 0)
        {
            StartCoroutine(can_move_after_delay(0.001f));
        }
        else
        {
            shuffle_back();
        }
    }

    public void shuffle_back()
    {
        Vector3 position = deck_rest_pos;
        int number_of_cards = discard_pile.Count;
        int order = number_of_cards - 1;
        for (int i = 0; i < number_of_cards; i++)
        {
            GameObject temp = ((GameObject)discard_pile[i]);
            temp.transform.SetParent(transform, false);
            temp.GetComponent<swipe_controller>().set_rest_pos(position);
            temp.GetComponent<swipe_controller>().set_can_spawn(false);
            if (i == 0)
            {
                temp.GetComponent<swipe_controller>().set_can_move(true);
            }
            else
            {
                temp.GetComponent<swipe_controller>().set_can_move(false);
            }
            temp.GetComponent<SpriteRenderer>().sortingOrder = order;
            temp.SetActive(true);
            hand_cards.Add(temp);
            position += new Vector3(-0.1f, 0, 0);
            order--;
        }
        discard_pile.Clear();
    }

    IEnumerator can_move_after_delay(float time)
    {
        yield return new WaitForSeconds(time);

        if(hand_cards.Count > 0)
        {
            ((GameObject)hand_cards[0]).GetComponent<swipe_controller>()
                    .set_can_move(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_container_script : MonoBehaviour
{
    [SerializeField] GameObject card_template;
    [SerializeField] Vector3 deck_rest_pos = Vector3.zero;
    [SerializeField] Sprite[] card_textures;
    [SerializeField] GameObject draw_deck;
    [SerializeField] GameObject discard_deck;
    [SerializeField] GameObject first_spell_ing;
    [SerializeField] GameObject second_spell_ing;
    [SerializeField] GameObject third_spell_ing;
    [SerializeField] GameObject button;
    GameObject card_at_first_ing = null;
    GameObject card_at_second_ing = null;
    GameObject card_at_third_ing = null;
    bool is_first_full = false;
    bool is_second_full = false;
    bool is_third_full = false;
    ArrayList hand_cards = new ArrayList();
    ArrayList discard_pile = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
    }

    public void use_spell()
    {
        discard_pile.Add(card_at_first_ing);
        discard_pile.Add(card_at_second_ing);
        discard_pile.Add(card_at_third_ing);
        card_at_first_ing.SetActive(false);
        card_at_second_ing.SetActive(false);
        card_at_third_ing.SetActive(false);
        card_at_first_ing.transform.position = discard_deck.transform.position;
        card_at_second_ing.transform.position = discard_deck.transform.position;
        card_at_third_ing.transform.position = discard_deck.transform.position;
        card_at_first_ing = null;
        card_at_second_ing = null;
        card_at_third_ing = null;
        is_first_full = false;
        is_second_full = false;
        is_third_full = false;
        button.SetActive(false);
    }

    public Vector3 new_ing_place(GameObject card)
    {
        if (is_first_full)
        {
            if (is_second_full)
            {
                if (is_third_full)
                {
                    return Vector3.zero;
                }
                is_third_full = true;
                card_at_third_ing = card;
                button.SetActive(true);
                return third_spell_ing.transform.position;
            }
            is_second_full = true;
            card_at_second_ing = card;
            return second_spell_ing.transform.position;
        }
        is_first_full = true;
        card_at_first_ing = card;
        return first_spell_ing.transform.position;
    }

    public void fill_deck(int number_of_cards)
    {
        Vector3 position = deck_rest_pos;
        int order = number_of_cards;
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
        temp.GetComponent<SpriteRenderer>().sprite = card_textures[Random.Range(0, card_textures.Length)];
        temp.SetActive(active);
        hand_cards.Add(temp);
    }

    public void used(GameObject card)
    {
        Vector3 dest = new_ing_place(card);
        if(dest == Vector3.zero)
        {
            //print("full!");
            return;
        }
        hand_cards.Remove(card);
        //discard_pile.Add(card);
        StartCoroutine(move_over_time(card, dest, 0.05f, false));
        card.GetComponent<SpriteRenderer>().sortingOrder = 0;
        card.GetComponent<swipe_controller>().can_be_disabled = true;
        card.GetComponent<swipe_controller>().set_can_move(false);
        if (hand_cards.Count > 0)
        {
            StartCoroutine(move_cards_to_front(hand_cards, deck_rest_pos, 0.05f));
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
        card.GetComponent<swipe_controller>().can_be_disabled = true;
        card.GetComponent<swipe_controller>().set_can_move(false);
        if (hand_cards.Count > 0)
        {
            StartCoroutine(move_over_time(card, discard_deck.transform.position, 0.05f, true));
            StartCoroutine(move_cards_to_front(hand_cards, deck_rest_pos, 0.05f));
        }
        else
        {
            shuffle_back();
        }
    }

    public void shuffle_list(ArrayList list)
    {
        // Fisher–Yates shuffle
        int current_len = list.Count;
        for(int i=1; i<list.Count; i++)
        {
            int chosen = Random.Range(0, current_len);
            object temp = list[current_len - 1];
            list[current_len - 1] = list[chosen];
            list[chosen] = temp;
            current_len--;
        }
    }

    public void shuffle_back()
    {
        Vector3 position = deck_rest_pos;
        int number_of_cards = discard_pile.Count;
        int order = number_of_cards - 1;
        shuffle_list(discard_pile);
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
            temp.GetComponent<swipe_controller>().can_be_disabled = false;
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

    public IEnumerator move_over_time(GameObject card, Vector3 destination, float duration, bool disable_after)
    {
        var pos_saver = card.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            card.transform.position = Vector3.Lerp(pos_saver, destination, t);
            yield return null;
        }
        if (disable_after && card.GetComponent<swipe_controller>().can_be_disabled == true)
        {
            card.SetActive(false);
        }
    }

    public IEnumerator move_cards_to_front(ArrayList cards, Vector3 destination, float duration)
    {
        float frames_for_animation = duration / Time.deltaTime;
        Vector3 displacement_per_frame = (destination - ((GameObject)cards[0]).transform.position) / frames_for_animation;
        ArrayList starting_positions = new ArrayList();
        for(int i=0; i<cards.Count; i++)
        {
            starting_positions.Add(((GameObject)cards[i]).transform.position);
            ((GameObject)cards[i]).GetComponent<swipe_controller>().set_can_move(false);
        }
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            for (int i = 0; i < cards.Count; i++)
            {
                ((GameObject)cards[i]).GetComponent<swipe_controller>()
                    .set_rest_pos(((GameObject)cards[i]).transform.position + displacement_per_frame);
            }
            yield return null;
        }
        ((GameObject)cards[0]).GetComponent<swipe_controller>().set_can_move(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

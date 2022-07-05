using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe_controller : MonoBehaviour
{
    [SerializeField] Vector3 card_rest_position = Vector3.zero;
    [SerializeField] public bool can_spawn = true;
    [SerializeField] public bool can_move = true;
    Vector3 touch_start, touch_end;
    double min_drag_length;

    // Start is called before the first frame update
    void Start()
    {
        min_drag_length = Screen.height * 10.0 / 100.0;
        transform.position = card_rest_position;
    }

    public void set_rest_pos(Vector3 pos)
    {
        card_rest_position = pos;
        transform.position = pos;
    }

    public Vector3 get_rest_pos()
    {
        return card_rest_position;
    }

    public void set_can_spawn(bool can)
    {
        can_spawn = can;
    }

    Vector3 card_transform_calc(Vector3 starting_touch_pos, Vector3 current_touch_pos)
    {
        return Camera.main.ScreenToWorldPoint(
            new Vector3(current_touch_pos.x, current_touch_pos.y, Camera.main.transform.position.z * -1)
            ) - Camera.main.ScreenToWorldPoint(
            new Vector3(starting_touch_pos.x, starting_touch_pos.y, Camera.main.transform.position.z * -1)
            );
    }

    void spanw_to(Vector3 pos)
    {
        if (can_spawn)
        {
            transform.parent
                .GetComponent<card_container_script>()
                .spawn_card_at_pos(card_rest_position + pos, true);
        }
    }

    public void set_can_move(bool can)
    {
        can_move = can;
    }

    // Update is called once per frame
    void Update()
    {
        if (can_move)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    touch_start = touch.position;
                    touch_end = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    touch_end = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    touch_end = touch.position;
                    if (Mathf.Abs(touch_start.x - touch_end.x) >= min_drag_length ||
                        Mathf.Abs(touch_start.y - touch_end.y) >= min_drag_length)
                    {
                        if (Mathf.Abs(touch_start.x - touch_end.x) > Mathf.Abs(touch_start.y - touch_end.y))
                        {
                            if (touch_start.x > touch_end.x)
                            {
                                print("left!");
                            }
                            else
                            {
                                print("right!");
                                transform.parent.GetComponent<card_container_script>().discarded(gameObject);
                            }
                        }
                        else
                        {
                            if (touch_start.y > touch_end.y)
                            {
                                print("down!");
                            }
                            else
                            {
                                print("up!");
                                transform.parent.GetComponent<card_container_script>().used(gameObject);
                            }
                        }
                    }
                    else
                    {
                        print("it was small!");
                    }
                    touch_start = card_rest_position;
                    touch_end = card_rest_position;
                }
                transform.localPosition = card_rest_position + card_transform_calc(touch_start, touch_end);
            }
            else
            {
                transform.localPosition = card_rest_position;
            }
        }
    }
}

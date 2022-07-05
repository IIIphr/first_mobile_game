using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe_controller : MonoBehaviour
{
    [SerializeField] Vector3 card_rest_position = Vector3.zero;
    Vector3 touch_start, touch_end;
    double min_drag_length;

    // Start is called before the first frame update
    void Start()
    {
        min_drag_length = Screen.height * 10.0 / 100.0;
    }

    Vector3 card_transform_calc(Vector3 starting_touch_pos, Vector3 current_touch_pos)
    {
        return Camera.main.ScreenToWorldPoint(
            new Vector3(current_touch_pos.x, current_touch_pos.y, Camera.main.transform.position.z * -1)
            ) - Camera.main.ScreenToWorldPoint(
            new Vector3(starting_touch_pos.x, starting_touch_pos.y, Camera.main.transform.position.z * -1)
            );
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                touch_start = touch.position;
                touch_end = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                touch_end = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                touch_end = touch.position;
                if(Mathf.Abs(touch_start.x - touch_end.x) >= min_drag_length ||
                    Mathf.Abs(touch_start.y - touch_end.y) >= min_drag_length)
                {
                    if(Mathf.Abs(touch_start.x - touch_end.x) > Mathf.Abs(touch_start.y - touch_end.y))
                    {
                        if(touch_start.x > touch_end.x)
                        {
                            print("left!");
                        }
                        else
                        {
                            print("right!");
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
                        }
                    }
                }
                else
                {
                    print("it was small!");
                }
            }
            transform.localPosition = card_rest_position + card_transform_calc(touch_start, touch_end);
        }
        else
        {
            transform.localPosition = card_rest_position;
        }
    }
}

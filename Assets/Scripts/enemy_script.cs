using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemy_script : MonoBehaviour
{
    [SerializeField] Image health_bar;
    [SerializeField] TextMeshProUGUI health_number;
    [SerializeField] float max_hp = 10;
    [SerializeField] float damage = 2;
    float hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        set_health_text();
    }

    public void set_health_text()
    {
        health_number.text = hp + "/" + max_hp;
    }

    public void do_turn()
    {
        // deal damage
        this.transform.parent.GetComponent<enemy_cont_script>()
            .add_card(Random.Range(0, 2) < 1 ? "red" : "blue");
    }

    public void set_max_hp(float number)
    {
        hp = number;
        max_hp = number;
    }

    public void die()
    {
        this.gameObject.GetComponentInParent<enemy_cont_script>()
            .died(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void damaged(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            die();
        }
        health_bar.fillAmount = hp / max_hp;
        set_health_text();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

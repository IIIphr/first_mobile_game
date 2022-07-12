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
    float hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        health_number.text = hp + "/" + max_hp;
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
        health_number.text = hp + "/" + max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

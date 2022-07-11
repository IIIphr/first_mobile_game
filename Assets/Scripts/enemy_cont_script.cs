using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cont_script : MonoBehaviour
{
    [SerializeField] GameObject enemy_template;
    [SerializeField] GameObject card_container;
    [SerializeField] GameObject spawn_button;
    ArrayList current_enemies = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void add_enemy()
    {
        GameObject temp = Instantiate(enemy_template);
        temp.transform.SetParent(this.gameObject.transform);
        temp.SetActive(true);
        current_enemies.Add(temp);
        spawn_button.SetActive(false);
    }

    public void died(GameObject enemy)
    {
        current_enemies.Remove(enemy);
        if(current_enemies.Count == 0)
        {
            spawn_button.SetActive(true);
        }
    }

    public void deal_damage(float damage)
    {
        if(current_enemies.Count > 0)
        {
            ((GameObject)current_enemies[0])
                .GetComponent<enemy_script>()
                .damaged(damage);
        }
        else
        {
            print("no enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

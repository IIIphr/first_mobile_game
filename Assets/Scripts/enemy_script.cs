using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    float hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(hp < 0)
        {
            die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

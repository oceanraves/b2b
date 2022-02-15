using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakHouse : MonoBehaviour
{

    //bool canBreak = false;
    [SerializeField]
    int houseLives;

    bool canBeHitAgain = true;
    public bool canBreak;


    private void OnTriggerEnter(Collider other)
    {
        if (canBeHitAgain && canBreak)
        {
            if (other.tag == "PlayerHitBox" && houseLives > 1)
            {
                houseLives -= 1;
                canBeHitAgain = false;
            }
            else if (other.tag == "PlayerHitBox" && houseLives <= 1)
            {
                GameObject.Find("Enemies").transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerHitBox")
        {
            canBeHitAgain = true;
        }
    }
}

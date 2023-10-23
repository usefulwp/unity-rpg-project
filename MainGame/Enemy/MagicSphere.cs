using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour {
    private SphereCollider sphereCollider;
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    public void CauseDamage(float attack ,Transform player)
    {
        Collider[] colArray = Physics.OverlapSphere(transform.position, sphereCollider.radius);
        if (colArray.Length > 0)
        {
            for (int i = 0; i < colArray.Length; i++)
            {
                if (colArray[i].tag.Equals(Tags.enemy))
                {
                    colArray[i].GetComponent<Enemy>().TakeDamage(attack,player);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadScript : MonoBehaviour
{
    [SerializeField] int TimeDead;



    private void Start()
    {
        StartCoroutine(IEDead());
    }

    IEnumerator IEDead()
    {
        yield return new WaitForSeconds(TimeDead);
        Destroy(gameObject);
    }
}

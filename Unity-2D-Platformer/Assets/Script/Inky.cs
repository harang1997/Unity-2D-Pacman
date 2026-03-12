using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inky : Enemy
{
    public Transform blinky;
    public Color inky;

    void Update()
    {
        Status();
        Action();
    }


    public override void Status()
    {
        base.Status();
    }
    public override void Action()
    {
        base.Action();
        if (status == eStatus.CHASE)
        {
            Vector3 pos = 2 * (target.position) - blinky.position;
            agent.SetDestination(pos);
        }
        if (status == eStatus.RUN)
        {
            StartCoroutine(Status_Run());
        }
    }
    IEnumerator Status_Run()
    {
        PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if (pc.IsEat == true)
        {
            Vector3 dir = this.transform.position - target.position;
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
            agent.SetDestination(dir);
        }
        yield return new WaitForSeconds(3f);
        if (pc.IsEat == false)
        {
            Vector3 dir = target.position - this.transform.position;
            dir = dir.normalized;
            gameObject.GetComponent<Renderer>().material.color = inky;
            agent.SetDestination(target.position);
        }

    }
}

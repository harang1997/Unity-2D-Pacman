using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clyde : Enemy
{
    public Transform blinky;
    public Color clyde;

    void Update()
    {
        Status();
        Action();
    }


    public override void Status()
    {
        PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if (Vector3.Distance(this.transform.position, target.position) < 10f && pc.IsEat == false)
        {
            status = eStatus.CHASE;
        }
        else if (Vector3.Distance(this.transform.position, target.position) < 1f && status == eStatus.CHASE)
        {
            agent.SetDestination(target.position);
        }
        else if (pc.IsEat == true)
        {
            status = eStatus.RUN;
        }
        else
        {
            if (waitingTime > 1)
            {
                waitingTime -= 1;
                wanderingPos = new Vector3(Random.Range(-35f, 35f), 1.5f, Random.Range(-33f, 55f));
            }
            status = eStatus.WANDER;
        }
    }
    public override void Action()
    {
        base.Action();
        if (status == eStatus.CHASE)
        {
            int s = Random.Range(0, 3);
            if (s == 0)
            {
                Vector3 dir = target.position - this.transform.position;
                dir = dir.normalized;
                agent.SetDestination(target.position);
            }
            if (s == 1)
            {
                Vector3 dir = target.position - this.transform.forward;
                dir = dir.normalized;
                agent.SetDestination(target.position);
            }
            if (s == 2)
            {
                Vector3 pos = 2 * (target.position) - blinky.position;
                agent.SetDestination(pos);
            }
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
            gameObject.GetComponent<Renderer>().material.color = clyde;
            agent.SetDestination(target.position);
        }

    }

}

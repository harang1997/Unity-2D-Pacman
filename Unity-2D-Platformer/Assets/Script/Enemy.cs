using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum eStatus
{
    RUN,//도망
    CHASE,//플레이어 추적
    WANDER//주변 배회
}

public class Enemy : MonoBehaviour
{
    public eStatus status = eStatus.WANDER;
    public Transform target;//인식할 대상
    public float enemySpeed = 5;//적의 속도

    protected Vector3 wanderingPos = Vector3.zero; //배회할때의 목적지

    protected float waitingTime = 0;//wander로 바뀔때
    protected NavMeshAgent agent;

    protected void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

    }

    public virtual void Status()
    {
        PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if (Vector3.Distance(this.transform.position, target.position) < 10f && pc.IsEat == false)
        {
            status = eStatus.CHASE;
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
    public virtual void Action()
    {
        switch (status)
        {
            case eStatus.CHASE:
                if (status == eStatus.CHASE)
                {
                    Vector3 dir = target.position - this.transform.position;
                    dir = dir.normalized;
                    agent.SetDestination(target.position);
                }
                break;
            case eStatus.RUN:
                if (status == eStatus.RUN)
                {
                    PlayerController pc = GameObject.Find("Player").GetComponent<PlayerController>();
                    if (pc.IsEat == true)
                    {
                        Vector3 dir = this.transform.position - target.position;
                        gameObject.GetComponent<Renderer>().material.color = Color.gray;
                        agent.SetDestination(dir);
                    }
                }
                break;
            case eStatus.WANDER:
                if (status == eStatus.WANDER)
                {
                    Vector3 dir = wanderingPos - this.transform.position;
                    dir = dir.normalized;
                    agent.SetDestination(target.position);
                    waitingTime += Time.deltaTime;
                    if (Vector3.Distance(wanderingPos, this.transform.position) < 0.1)
                    {
                        waitingTime = 1;
                    }
                }
                break;
        }
    }

}
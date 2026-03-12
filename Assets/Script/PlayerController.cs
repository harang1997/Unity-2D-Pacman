using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 100f;
    public Transform Point;
    public Transform spawnPoint;//적이 다시 스폰할 위치
    public Text scoretext;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public bool IsEat = false;
    public List<Transform> itemList;
    public List<Transform> powerItemList;
    public int itemCount;
    public int powerItemCount;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        IsEat = false;
        itemList = new List<Transform>();
        powerItemList = new List<Transform>();
        ItemCount();
        PowerItemCount();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Point.localPosition = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 move = new Vector3(horizontal, 0, vertical) * speed;
        rb.velocity = move;
    }

    public void ItemCount()
    {
        var items = GameObject.FindGameObjectsWithTag("Item");

        foreach (var item in items)
        {
            itemList.Add(item.GetComponent<Transform>());
        }
        itemCount = itemList.Count;

    }

    public void PowerItemCount()
    {
        var powerItems = GameObject.FindGameObjectsWithTag("PowerItem");

        foreach (var powerItem in powerItems)
        {
            powerItemList.Add(powerItem.GetComponent<Transform>());
        }
        powerItemCount = powerItemList.Count;

    }
    IEnumerator IsEAT()
    {
        IsEat = true;
        yield return new WaitForSeconds(5f);
        IsEat = false;
    }

    IEnumerator Die(Transform enemy)
    {
        enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawnPoint.position);
        enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(spawnPoint.position);
        enemy.GetComponent<Enemy>().status = eStatus.WANDER;

        yield return new WaitForSeconds(3f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            scoretext.gameObject.GetComponent<ScoreScr>().Score();
            other.gameObject.SetActive(false);

            if (itemList.Contains(other.transform))
            {
                itemList.Remove(other.transform);
            }

            itemCount -= 1;
            ItemCountDec();

        }
        if (other.gameObject.CompareTag("PowerItem"))
        {
            scoretext.gameObject.GetComponent<ScoreScr>().PowerItemScore();
            StartCoroutine(IsEAT());
            other.gameObject.SetActive(false);

            if (powerItemList.Contains(other.transform))
            {
                powerItemList.Remove(other.transform);
            }
            powerItemCount -= 1;
            ItemCountDec();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (IsEat == true)
            {
                scoretext.gameObject.GetComponent<ScoreScr>().EnemyScore();
                StartCoroutine(Die(other.transform));
            }
            else if (IsEat == false)
            {
                PlayerPrefs.DeleteKey("Key_Int");
                this.enabled = false;
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
        }
    }

    void ItemCountDec()
    {
        /* itemList.Count == itemCount */
        // if(itemList.Count <= 0)
        if (itemCount <= 0 && powerItemCount <= 0)
        {
            //PlayerPrefs.DeleteKey("Key_Int");
            Time.timeScale = 0;
            gameClearPanel.SetActive(true);
        }
    }
}

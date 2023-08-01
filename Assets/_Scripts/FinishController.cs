
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public Transform jumpPos;


    [Header("Test")] public GameObject sick;
    public Transform spawnPos;
    public bool asd;

    public DOTweenAnimation dt;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (asd)
        {
            StartCoroutine(StockGo());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StockGo());

            GameManager.Instance.anim.SetTrigger("Idle");
            GameManager.Instance.gameState = GameManager.GameState.Win;
        }
        
        if (other.gameObject.CompareTag("Collected"))
        {
            other.gameObject.transform.DOScale(0.3f, 1f);
            dt.DORestart(); 
            other.gameObject.transform.DOJump(jumpPos.position, 0.5f, 1, 1f)
                .OnComplete(() =>
                {
                    
                    Destroy(other.gameObject); 
                });
        }
    }

    private IEnumerator StockGo()
    {
        for (int i = 0; i <= GameManager.Instance.stocked; i++)
        {
            GameObject a = Instantiate(sick, spawnPos.transform.position, Quaternion.identity);
            
            a.gameObject.transform.DOScale(0.3f, 1f);
            dt.DORestart();
            a.transform.DOJump(jumpPos.position,0.5f,1,1).OnComplete(() =>
            {
                Destroy(a.gameObject); 
            });
            
            if (i == GameManager.Instance.stocked)
            {
                GameManager.Instance.anim.SetTrigger("Win");
                GameManager.Instance.GameWin();
            }
            
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    private void StockCameFinishEffect()
    {
        //TODO: Particle
    }

    
}

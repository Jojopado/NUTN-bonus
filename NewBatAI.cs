using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBatAI : MonoBehaviour
{
    public RoomDetecter roomdetecter;
    private bool inRoom;

    [Header("朝著的目標")]
    [Tooltip("抵達時間")]
    public GameObject posA;

    public float ArrivedTime;
    public GameObject posB;
    
    public float distance;

    public LayerMask whatissolid;

     public bool throwBomb;
    public GameObject Bomb;
    public bool inRange;

    void Start()
    {
        transform.DOMoveX(posA.transform.position.x + 1 , ArrivedTime)
        .SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        if(inRoom && throwBomb == false)
        {
            throwBomb = true;
            StartCoroutine(ThrowBomb());
        }
     
        CheckIfPlayerIsInRoom();
        RaycastEvent();
    }
    #region updatefun

    
    IEnumerator ThrowBomb()
    {
        if (throwBomb)
        {
            Instantiate(Bomb, new Vector3(transform.position.x,
             transform.position.y - 1f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(3f);
            throwBomb = false;
            Debug.Log("BombCountDOwn");
        }
    }
    void CheckIfPlayerIsInRoom()
    {
        if (roomdetecter.detected)
        {
            inRoom = true;
        }
        else
        {
            inRoom = false;
        }
    }
    
    void OnChangeSpriteFacing(bool right)
    {
        //true is face left A B
            if(gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = !right;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = right;
            }
            distance *= -1;
    }
    void RaycastEvent()
    {
        Debug.DrawRay(transform.position ,Vector3.left,Color.red,distance);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.left , distance , whatissolid) ;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("turnBlock"))
            {
                OnChangeSpriteFacing(true);
            }
        }
    }
    #endregion
}

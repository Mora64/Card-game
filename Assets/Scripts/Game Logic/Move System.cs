using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool isBeingHeld;
    private CardShift cardShift;
    private GameObject cardOfPlayer;
    private GameObject hand;
    private GameObject copyOfCard;
    private Vector3 startCardPos;
    private bool isScaled = false;
    private CardHandler cardHandler;
    private float maxY;
    private float minY;
    private void Start()
    {
        startCardPos = transform.position;
        cardHandler = GameObject.FindGameObjectWithTag("BattleGroundShop").GetComponent<CardHandler>();
        GetCorners();
    }
    void Update()
    {
        if (isBeingHeld) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
            if(transform.position.y > maxY|| transform.position.y < minY)
                if (GetComponent<CardState>().state != CardState.State.None) cardHandler.Remove(transform);
          /*  cardHandler.CheckPlace();*/
        }

    }
    private void GetCorners()
    {
        BoxCollider2D boxCollider2D = this.GetComponent<BoxCollider2D>();
        maxY = boxCollider2D.bounds.max.y;
        minY = boxCollider2D.bounds.min.y;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cardHandler.CardUnscale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<CardState>().state == CardState.State.HandCard)
        {
            cardHandler.CardScale(this.transform);
        }
        
    }
   

public void OnPointerUp(PointerEventData eventData)
    {
        if(GetComponent<CardState>().moveable == true)
        {
            isBeingHeld = false;
            cardHandler.CardMove(this.transform, startCardPos);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GetComponent<CardState>().moveable == true)
        {
            startCardPos = transform.position;
            if (Input.GetMouseButton(0))
            {
                isBeingHeld = true;
            }
        }
        cardHandler.CardUnscale();  
    }
}

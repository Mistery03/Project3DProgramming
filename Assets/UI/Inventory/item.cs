using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool resetPositionOnRealese = true;

    Vector2 _startPosition;
    Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(resetPositionOnRealese)
            _startPosition = transform.position;
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,hits);

        var hit = hits.FirstOrDefault(t => t.gameObject.CompareTag("Droppable"));
        var item = hits.FirstOrDefault(t => t.gameObject.CompareTag("item"));
        if (hit.isValid && !item.isValid)
        {
            return;
        }

        if (resetPositionOnRealese)
        {
            transform.position = _startPosition;
           
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

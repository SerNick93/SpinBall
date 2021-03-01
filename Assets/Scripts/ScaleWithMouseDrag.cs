using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider))]
public class ScaleWithMouseDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject anchor;
    private Camera mainCamera;
    private float CameraZDistance;
    private Vector3 initialScale;
    [SerializeField]
    float distance;
    Vector3 MouseScreenPosition;
    Vector3 MouseWorldPosition;
    bool b_isPlaced = false;
    bool b_isPlacing = false;
    BoxCollider bc;
    Vector3 middlePoint;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        bc = GetComponent<BoxCollider>();
    }

    void LateUpdate()
    {
        MouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);

        MouseWorldPosition = mainCamera.ScreenToWorldPoint(MouseScreenPosition);

        distance = Vector3.Distance(anchor.transform.position, MouseWorldPosition);
        middlePoint = (anchor.transform.position + MouseWorldPosition) / 2f;



        if (b_isPlacing == false && b_isPlaced == false)
        {
            anchor.transform.position = MouseWorldPosition;
        }

    }    
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = MouseWorldPosition;
        OnBeginDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("MouseDrag");

        transform.localScale = new Vector3(initialScale.x, distance / anchor.transform.localScale.y, initialScale.z);

        middlePoint = (anchor.transform.position + MouseWorldPosition) / 2f;
        transform.position = middlePoint;

        Vector3 rotationDirection = MouseWorldPosition - anchor.transform.position;
        transform.up = rotationDirection;
        bc.center = Vector3.zero;
        bc.size = (new Vector3(transform.localScale.x, transform.localScale.y / distance, transform.localScale.z));

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        b_isPlacing = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        b_isPlacing = false;
        b_isPlaced = true;
        GameManager.MyInstance.Newline();
        StartCoroutine(DestroyLine());

    }

    IEnumerator DestroyLine()
    {
        yield return new WaitForSecondsRealtime(3f);
        Destroy(anchor);
    }

    // Update is called once per frame

}

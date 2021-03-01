using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum GameStateEnum { SpinState, GameState, PauseState, MenuState};
public class GameManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Transform anchorObject;
    ScaleWithMouseDrag scale;
    Camera mainCamera;
    float CameraZDistance;
    Vector3 MouseScreenPosition, MouseWorldPosition;
    bool b_isPlaced = false;
    Transform boardInstance;
    GameStateEnum gameState;

    public static GameManager myInstance;
    public static GameManager MyInstance 
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<GameManager>();
            }
            return myInstance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        //boardInstance = Instantiate(anchorObject, MouseWorldPosition, Quaternion.identity);
        gameState = GameStateEnum.SpinState;

    }
    private void FixedUpdate()
    {
        MouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        MouseWorldPosition = mainCamera.ScreenToWorldPoint(MouseScreenPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameState == GameStateEnum.GameState)
        {
            Newline();
        }
    }
    public void Newline()
    {
        boardInstance = Instantiate(anchorObject, MouseWorldPosition, Quaternion.identity);
    }
}

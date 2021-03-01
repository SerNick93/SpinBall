using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallReleaser : MonoBehaviour, IPointerDownHandler
{
    bool b_isSpinning = false;
    [SerializeField]
    GameObject ball;
    Transform releaseShoot;
    [SerializeField]
    float spinSpeed;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Releasing Ball");
        b_isSpinning = true;
        StartCoroutine(BallPreRelease());
    }

    // Start is called before the first frame update
    void Start()
    {
        releaseShoot = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (b_isSpinning)
        {
            transform.Rotate(0, spinSpeed * Time.deltaTime, 0f);
        }
    }
    IEnumerator BallPreRelease()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        b_isSpinning = false;
        Instantiate(ball, releaseShoot.position, Quaternion.identity);
    }

}

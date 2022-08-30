using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDecoration : MonoBehaviour
{

    private Vector3 initialPosition;
    public float speedX;
    public float speedY;
    public float resetTimer;
    public float initialTimerDelay;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        Invoke(nameof(ResetPosition), resetTimer + initialTimerDelay);
    }

    private void Update() {
        transform.position = transform.position + new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }
    
    private void ResetPosition()
    {
        transform.position = initialPosition;
        Invoke(nameof(ResetPosition), resetTimer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyMovementHandler : MonoBehaviour
{
    public Transform PlayerTransform;
    public float JumpThreshold = 0.01f;
    public float RefreshTime = 0.1f;
    public float JumpForce = 2f;
    public float JumpTime = 0.8f;
    public AudioSource JumpSound;

    private float lastYPosition;
    private float timeSinceLastJump = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastYPosition = PlayerTransform.position.y;
    }

    private void OnEnable()
    {
        StartCoroutine(CheckYPositionDifferenceCoroutine());
    }

    private void Update()
    {
        timeSinceLastJump += Time.deltaTime;
    }

    IEnumerator CheckYPositionDifferenceCoroutine()
    {
        while (true)
        {
            if ((PlayerTransform.position.y - lastYPosition) > JumpThreshold && (timeSinceLastJump > JumpTime))
            {
                rb.AddForce(0f, JumpForce, 0f);
                timeSinceLastJump = 0f;
                JumpSound.Play();
            }

            lastYPosition = PlayerTransform.position.y;

            yield return new WaitForSeconds(RefreshTime);
        }
    }
}

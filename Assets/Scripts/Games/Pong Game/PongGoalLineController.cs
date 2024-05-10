using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGoalLineController : MonoBehaviour
{
    public int LastControllerToHit = 0; // 0 = none, 1 = left, 2 = right
    public bool LastCollisionWasFirstCollsion = true;

    private PongPlayerCounter LeftCounter;
    private PongPlayerCounter RightCounter;

    private void Start()
    {
        Invoke("FindPlayers", 0.1f);
    }

    void FindPlayers()
    {
        GameObject[] Controllers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject controller in Controllers)
        {
            FlatscreenPlayerTransformHandler flatscreenPlayerTransformHandler = controller.transform.parent.GetComponentInChildren<FlatscreenPlayerTransformHandler>();

            if(flatscreenPlayerTransformHandler != null)
            {
                if (flatscreenPlayerTransformHandler.ThisPlayersController.CompareTag("CustomGameControllerLeft"))
                {
                    LeftCounter = controller.transform.parent.GetComponentInChildren<PongPlayerCounter>();
                }
                else
                {
                    RightCounter = controller.transform.parent.GetComponentInChildren<PongPlayerCounter>();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(LeftCounter == null || RightCounter == null)
        {
            return;
        }

        if(LastCollisionWasFirstCollsion)
        {
            LastCollisionWasFirstCollsion = false;
            return;
        }

        if (collision.gameObject.CompareTag("PongBall"))
        {
            if (LastControllerToHit == 1)
            {
                LeftCounter.IncreaseCounter();
            }
            if (LastControllerToHit == 2)
            {
                RightCounter.IncreaseCounter();
            }
        }
    }
}

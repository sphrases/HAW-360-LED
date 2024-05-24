using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallCollisionHandler : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public GameObject GoalLine;
    MeshRenderer goalLineMeshRender;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        goalLineMeshRender = GoalLine.GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent.GetComponentInChildren<FlatscreenPlayerTransformHandler>().ThisPlayersController.CompareTag("CustomGameControllerLeft"))
            {
                GoalLine.SetActive(true);
                GoalLine.GetComponent<PongGoalLineController>().LastControllerToHit = 1;
                GoalLine.GetComponent<PongGoalLineController>().LastCollisionWasFirstCollsion = true;
                GoalLine.GetComponent<BoxCollider>().enabled = true;
                goalLineMeshRender.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                meshRenderer.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                GoalLine.transform.position = new Vector3(transform.position.x, GoalLine.transform.position.y, GoalLine.transform.position.z);
            }
            else if (collision.transform.parent.GetComponentInChildren<FlatscreenPlayerTransformHandler>().ThisPlayersController.CompareTag("CustomGameControllerRight"))
            {
                GoalLine.SetActive(true);
                GoalLine.GetComponent<PongGoalLineController>().LastControllerToHit = 2;
                GoalLine.GetComponent<PongGoalLineController>().LastCollisionWasFirstCollsion = true;
                GoalLine.GetComponent<BoxCollider>().enabled = true;
                goalLineMeshRender.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                meshRenderer.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                GoalLine.transform.position = new Vector3(transform.position.x, GoalLine.transform.position.y, GoalLine.transform.position.z);
            }
        }
    }
}
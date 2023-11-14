using UnityEngine;
using UnityEngine.Serialization;

public class CylinderToFlatscreenPosition : MonoBehaviour
{
    public GameObject cylinder;
    public GameObject flatscreen;

    /* public Vector2 GetPlayerPosition()
    {
        var localTransformPosition = transform.position;

        Debug.Log("localTransformPosition");
        Debug.Log(localTransformPosition);

        var closestPositionOnCylinderToController = GetClosestPositionOnCylinderToController(localTransformPosition);
        // var positionOnFlatscreen = TransferCylinderPositionToFlatscreen(closestPositionOnCylinderToController);
        return positionOnFlatscreen;
    } */ 

    Vector3 GetClosestPositionOnCylinderToController(Vector3 controllerPosition)
    {
        var cylinderTransform = cylinder.transform; // this is more efficient then accessing the value repeatedly
        var cylinderTransformPosition = cylinderTransform.position; // this is more efficient then accessing the value repeatedly

        var cylinderPositionWithoutY = new Vector3(cylinderTransformPosition.x, 0, cylinderTransformPosition.z); // Cyl pos without height
        var controllerPositionWithoutY = new Vector3(controllerPosition.x, 0, controllerPosition.z); // controller pos without height
        var rayDirection = cylinderPositionWithoutY - controllerPositionWithoutY; // get direction from controller to cyl

        RaycastHit hitInfo;

        Physics.Raycast(controllerPositionWithoutY, rayDirection, out hitInfo); // shoot ray from controller to cyl
        var clampedYPosition = Mathf.Clamp(controllerPosition.y, cylinderTransformPosition.y - cylinderTransform.localScale.y, cylinderTransformPosition.y + cylinderTransform.localScale.y); // limit height by height of cylinder
        var closestPosition = new Vector3(hitInfo.point.x, clampedYPosition, hitInfo.point.z);
        return closestPosition; // under the assumption that in the area of the controllers only the cylinder will have a collider
    }

    public Vector2 TransferCylinderPositionToFlatscreen(GameObject controllerGameObject)
    {
        var controllerPosition = controllerGameObject.transform.position;
        var cylinderPosition = cylinder.transform.position;
        var angle = 180 + Mathf.Rad2Deg * Mathf.Atan2((controllerPosition.z - cylinderPosition.z), (controllerPosition.x - cylinderPosition.x)); // this is for unitCircle, position.x and position.y need to be substracted in case center of cylinder is not at 0,0

        if (angle < 0)
        {
            angle += 360f;
        }

        var normalizedY = controllerPosition.y / (cylinder.transform.localScale.y * 2);
        var flatscreenRectTransform = flatscreen.GetComponent<RectTransform>();
        var xOnFlatscreen = ((angle / 360) * flatscreenRectTransform.rect.width) - (flatscreenRectTransform.rect.width / 2); // x coordinate is basically a percentage of the screenWidth measured by percentage of angle to 360° (assumption that center of screen is 0,0)
        var yOnFlatscreen = (normalizedY * flatscreenRectTransform.rect.height) - (flatscreenRectTransform.rect.height / 2); // y coordinate is basically a percentage of screenHeight (assumption that center of screen is 0,0)
        return new Vector2(xOnFlatscreen, yOnFlatscreen);
    }
}
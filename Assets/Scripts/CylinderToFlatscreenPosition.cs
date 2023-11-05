using UnityEngine;

public class CylinderToFlatscreenPosition : MonoBehaviour
{
    public GameObject Cylinder;
    public GameObject Flatscreen;

    public Vector2 GetPlayerPosition()
    {
        Vector3 _closestPositionOnCylinderToController = GetClosestPositionOnCylinderToController(transform.position);
        Vector2 _positionOnFlatscreen =  TransferCylinderPositionToFlatscreen(_closestPositionOnCylinderToController);
        return _positionOnFlatscreen;
    }

    Vector3 GetClosestPositionOnCylinderToController(Vector3 _controllerPosition)
    {
        Vector3 _cylinderPositionWithoutY = new Vector3(Cylinder.transform.position.x, 0, Cylinder.transform.position.z);
        Vector3 _controllerPositionWithoutY = new Vector3(_controllerPosition.x, 0, _controllerPosition.z);
        Vector3 _rayDirection = _cylinderPositionWithoutY - _controllerPositionWithoutY;
        RaycastHit _hitInfo;
        Physics.Raycast(_controllerPositionWithoutY, _rayDirection, out _hitInfo);
        float _clampedYPosition = Mathf.Clamp(_controllerPosition.y, Cylinder.transform.position.y - Cylinder.transform.localScale.y, Cylinder.transform.position.y + Cylinder.transform.localScale.y);
        Vector3 _closestPosition = new Vector3(_hitInfo.point.x, _clampedYPosition, _hitInfo.point.z);
        return _closestPosition; // under the assumption that in the area of the controllers only the cylinder will have a collider
    }

    Vector2 TransferCylinderPositionToFlatscreen(Vector3 _closestPositionOnCylinderToController)
    {
        Vector3 _cylinderPosition = Cylinder.transform.position;
        float _angle = Mathf.Rad2Deg * Mathf.Atan2((_closestPositionOnCylinderToController.z - _cylinderPosition.z), (_closestPositionOnCylinderToController.x - _cylinderPosition.x)); // this is for unitCircle, position.x and position.y need to be substracted in case center of cylinder is not at 0,0
        
        if(_angle < 0)
        {
            _angle += 360f;
        }

        float _normalizedY = _closestPositionOnCylinderToController.y / (Cylinder.transform.localScale.y * 2);
        RectTransform _flatscreenRectTransform = Flatscreen.GetComponent<RectTransform>();
        float _xOnFlatscreen = ((_angle/360) * _flatscreenRectTransform.rect.width) - (_flatscreenRectTransform.rect.width / 2); // x coordinate is basically a percentage of the screenWidth measured by percentage of angle to 360° (assumption that center of screen is 0,0)
        float _yOnFlatscreen = (_normalizedY * _flatscreenRectTransform.rect.height) - (_flatscreenRectTransform.rect.height / 2); // y coordinate is basically a percentage of screenHeight (assumption that center of screen is 0,0)
        return new Vector2(_xOnFlatscreen, _yOnFlatscreen);
    }
}

using UnityEngine;

public class CylinderToFlatscreenPosition : MonoBehaviour
{
    public enum TrackingModeOptions
    {
        RayCastDirect,
        ClosestPosition
    }


    public TrackingModeOptions TrackingMode = TrackingModeOptions.RayCastDirect;
    public GameObject Cylinder;
    public GameObject CalculationCanvas;
    public FlatscreenPlayerTransformHandler CorrespondingInGamePlayer;
    public float RealCylinderLowerEdge = 0.7f;

    private Vector2 oldScreenPosition = new Vector2(0, 0);


    public float GetPlayerZRotationEuler()
    {
        return transform.rotation.eulerAngles.z;
    }

    public Vector2 TransferCylinderPositionToFlatscreen()
    {
        Vector3 closestPositionOnCylinderToController = transform.position; // this would be the raycast position


        switch (TrackingMode)
        {
            case TrackingModeOptions.RayCastDirect:
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Debug.Log("Did Hit");

                    closestPositionOnCylinderToController = hit.point;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    //Debug.Log("Did not Hit");
                    return oldScreenPosition;
                }

                break;

            case TrackingModeOptions.ClosestPosition:
                // Do nothing, this is the standard case
                break;
        }


        Vector3 _cylinderPosition = Cylinder.transform.position;


        float _angle = 180 + Mathf.Rad2Deg * Mathf.Atan2((closestPositionOnCylinderToController.z - _cylinderPosition.z),
            (closestPositionOnCylinderToController.x - _cylinderPosition.x)); // this is for unitCircle, position.x and position.y need to be substracted in case center of cylinder is not at 0,0

        if (_angle < 0)
        {
            _angle += 360f;
        }

        float _anglePercentage = _angle;

        float _yPercentage = closestPositionOnCylinderToController.y - RealCylinderLowerEdge;


        RectTransform _flatscreenRectTransform = CalculationCanvas.GetComponent<RectTransform>();
        float _xOnFlatscreen = ((_anglePercentage / 360) * _flatscreenRectTransform.rect.width) - (_flatscreenRectTransform.rect.width / 2); // x coordinate is basically a percentage of the screenWidth measured by percentage of angle to 360� (assumption that center of screen is 0,0)
        float _yOnFlatscreen = (_yPercentage * _flatscreenRectTransform.rect.height) - (_flatscreenRectTransform.rect.height / 2); // y coordinate is basically a percentage of screenHeight (assumption that center of screen is 0,0)

        // Debug.Log("y percentage:  " + _yPercentage + "; Flatscreen Height:  " + _flatscreenRectTransform.rect.height + "; Y On Flatscreen:  " + _yOnFlatscreen);



        Vector2 newScreenPos = new Vector2(_xOnFlatscreen, _yOnFlatscreen);
        oldScreenPosition = newScreenPos;
        return newScreenPos;
    }
}
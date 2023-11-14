using UnityEngine;

namespace Pong_Game
{
    public class PongPaddleController : MonoBehaviour
    {
        public GameObject playerControllerInstance;
        public float maxDisplacement = 100f;

        private CylinderToFlatscreenPosition _cylToFlatScreen;
        // private Rigidbody2D _rb;

        private void Start()
        {
            // _rb = GetComponent<Rigidbody2D>();
            _cylToFlatScreen = playerControllerInstance.GetComponent<CylinderToFlatscreenPosition>();
        }

        private void Update()
        {
            var newPosition = _cylToFlatScreen.TransferCylinderPositionToFlatscreen(playerControllerInstance);
            var currentPosition = transform.position;
            Debug.Log("newPosition");
            Debug.Log(newPosition);


            // transform.position = 
            var newTransform = new Vector3(newPosition.x, newPosition.y, currentPosition.z);

            Vector3 displacement = newTransform - transform.position;


            transform.position = newTransform;

            if (displacement.magnitude > maxDisplacement)
            {
                transform.position = newTransform;
            }
            else
            {
                // var velocity = displacement / Time.fixedDeltaTime;
                // _rb.velocity = velocity;
            }
        }
    }
}
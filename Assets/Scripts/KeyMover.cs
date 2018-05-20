using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class KeyMover : MonoBehaviour
{

    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    private ImmediatePositionWithLocationProvider ipwlProvider;

    public float speed;             //Floating point variable to store the player's movement speed.

    private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
    private Rect cameraRect;

    float maxMovement = 0.5f;

    // Use this for initialization
    void Start()
    {
        var bottomLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        var topRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    void FixedUpdate()
    {
        if (ipwlProvider.enabled)
        {
            return;
        }
        // float moveHorizontal = Input.GetAxis ("Horizontal");
        // float moveVertical = Input.GetAxis ("Vertical");

        // Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        // rb.AddForce (movement * speed);

        float horizontal = 0f;     //Used to store the horizontal move direction.
        float vertical = 0f;       //Used to store the vertical move direction.

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        horizontal = Mathf.Clamp(horizontal, -1f * maxMovement, maxMovement);
        vertical = Mathf.Clamp(vertical, -1f * maxMovement, maxMovement);

        // //Check if moving horizontally, if so set vertical to zero.
        // if(horizontal != 0)
        // {
        // vertical = 0;
        // }
        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

Debug.Log(Input.GetAxisRaw ("Horizontal"));

        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }

            Vector2 touchEnd = myTouch.position;

            float x = (touchEnd.x - touchOrigin.x) * this.speed;
            float y = (touchEnd.y - touchOrigin.y) * this.speed;

            horizontal = Mathf.Clamp((Mathf.Abs(x) < 0.01f ? 0.0f : x) * 0.005f, -1f * maxMovement, maxMovement);
            vertical = Mathf.Clamp((Mathf.Abs(y) < 0.01f ? 0.0f : y) * 0.005f, -1f * maxMovement, maxMovement);
        }

#endif //End of mobile platform dependendent compilation section started above with #elif
        //Check if we have a non-zero value for horizontal or vertical

        if (horizontal != 0.0f || vertical != 0.0f)
        {
            Vector3 position = gameObject.transform.position;
            position.x += horizontal;
            position.z += vertical;
            gameObject.transform.position = position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    #region Inspector Variables

    [SerializeField]
    private float hopAngle;

    [SerializeField]
    private float hopForce;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float groundCheckDistance;

    #endregion

    #region Private Variables

    private float hopAngleTrue;

    private Rigidbody2D rb;

    private float horizontal;

    private Vector2 hopAngleUnitVector;

    private bool grounded = true;

    private Color groundedDebugColor;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    private void Awake()
    {
        hopAngleTrue = hopAngle * Mathf.Deg2Rad; 
        rb = GetComponent<Rigidbody2D>();
        hopAngleUnitVector = new Vector2(Mathf.Cos(hopAngleTrue), Mathf.Sin(hopAngleTrue));
    
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics2D.Linecast(transform.position, ((Vector2) transform.position) + (Vector2.down * groundCheckDistance));

        groundedDebugColor = Color.red;
        if (grounded)
        {
            groundedDebugColor = Color.green;
        }
        Debug.DrawLine(transform.position, ((Vector2)transform.position) + (Vector2.down * groundCheckDistance), groundedDebugColor);


        horizontal = Input.GetAxis("Horizontal");

        if (grounded)
        {
            Hop();

        }
        
    }

    #endregion

    #region Action Functions

    private void Hop()
    {
        if (horizontal != 0)
        {
            //rb.velocity = hopAngleUnitVector * horizontal.normalized * hopForce;
            rb.velocity = new Vector2(hopAngleUnitVector.x * GetHorizontalMultiplier(horizontal), hopAngleUnitVector.y) * hopForce;

        }

        // AddForce method

        //if (horizontal > 0)
        //{
        //    rb.AddForce(hopAngleUnitVector * hopForce, ForceMode2D.Impulse);
        //}
        //else if (horizontal < 0)
        //{
        //    rb.AddForce(Vector2.Reflect(hopAngleUnitVector, Vector2.left), ForceMode2D.Impulse);
        //}

    }

    private float GetHorizontalMultiplier(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            return 1f;
        }
        else if (horizontalInput < 0)
        {
            return -1f;
        }
        else
        {
            return horizontalInput;
        }
    }

    #endregion
}

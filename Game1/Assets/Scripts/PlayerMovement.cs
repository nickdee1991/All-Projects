using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // This is a reference to the Rigidbody component called "rb"
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
	// We marked this as "Fixed" Update because we
    // are using it to mess with physics
	void FixedUpdate ()
    {
        // add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);   // Add a force of 2000 on the z-axis

        if (Input.GetKey("d")   )
        {
            //add right force
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            //add left force
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}

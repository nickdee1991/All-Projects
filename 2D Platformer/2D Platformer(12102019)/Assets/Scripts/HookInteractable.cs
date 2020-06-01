using UnityEngine;

public class HookInteractable : MonoBehaviour
{
    private bool PlayerGrab;

    private void Start()
    {
        PlayerGrab = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("trigger hooked");
        if (Input.GetMouseButtonDown(0) && other.gameObject.CompareTag("Hinge"))
        {
            PlayerGrab = true;
            Debug.Log(PlayerGrab);
        }

        if (Input.GetMouseButton(0) && PlayerGrab == true)
        {
            GameObject closest = FindNearest();

            if (PlayerGrab)
            {
                Debug.Log(PlayerGrab + "has grabbed");
                closest.GetComponentInChildren<HingeJoint2D>().connectedBody =
                    gameObject.GetComponentInParent<Rigidbody2D>();
                PlayerGrab = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject[] Hinges;
            Hinges = GameObject.FindGameObjectsWithTag("Hinge");

            foreach (GameObject go in Hinges)
            {
                go.GetComponentInChildren<HingeJoint2D>().connectedBody = null;
            }
        }
    }
    
    GameObject FindNearest()
    {
        GameObject[] Hinges;
        Hinges = GameObject.FindGameObjectsWithTag("Hinge");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in Hinges)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

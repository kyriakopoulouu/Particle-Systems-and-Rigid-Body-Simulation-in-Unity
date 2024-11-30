using UnityEngine;

public class Seeker : MonoBehaviour
{
    public Color targetColor;
    public float chaseSpeed = 10 ;
    void Update()
    {
        // find the gameobject with the same color
        GameObject target = null;
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.GetComponent<Renderer>() && go.GetComponent<Renderer>().material.color == targetColor)
            {
                target = go;
                break;
            }
        }

        // move towards the gameobject with the same color
        if (target)
        {
            // calculate the direction to move towards the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // move towards the target at a fixed speed
            
            transform.position = transform.position + direction * chaseSpeed * Time.deltaTime;
        }
    }
}
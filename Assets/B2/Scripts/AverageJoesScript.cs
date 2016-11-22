using UnityEngine;
using System.Collections;

public class AverageJoesScript : MonoBehaviour {

    NavMeshAgent agent;
    RaycastHit hitInfo = new RaycastHit();

    private bool selected = false;
    private bool move = false;
    private Vector3 targetPosition;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	

	void Update () {
      /*  if (selected && move)
        {
            move = false;
            agent.SetDestination(targetPosition);
        } */

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }

    }

    void Select(int x)
    {
        selected = true;
    }

    void Destination(Vector3 dest)
    {
        targetPosition = dest;
        move = true;
    }
}

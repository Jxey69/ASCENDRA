using UnityEngine;

public class ClimbingRope2 : MonoBehaviour
{
    public Transform anchorPoint;
    public Transform swingingObject; // Assign itself
    public LineRenderer lineRenderer;

    void Start()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();

        // Convert world position of anchor to local space of the Swinger
        Vector3 localAnchor = transform.InverseTransformPoint(anchorPoint.position);
        hinge.anchor = localAnchor;

        // Optional: Ensure Axis is set for side swing
        hinge.axis = Vector3.forward; // (0, 0, 1)
    }

    void Update()
    {
        lineRenderer.SetPosition(0, anchorPoint.position);
        lineRenderer.SetPosition(1, swingingObject.position);
    }
}

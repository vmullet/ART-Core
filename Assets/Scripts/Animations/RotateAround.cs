using UnityEngine;

public class RotateAround : MonoBehaviour {

    private Vector3 startOrientation;
    private Transform[] stages;

    [SerializeField]
    private Transform center;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool clockWise;

    const float DEFAULT_ROTATE_SPEED = 10f;

    private void Awake()
    {
        stages = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            stages[i] = transform.GetChild(i).transform;
        }
    }

    private void Start ()
    {
        startOrientation = clockWise ? new Vector3(0, 0, -1) : new Vector3(0, 0, 1);
        if (rotateSpeed <= 0)
            rotateSpeed = DEFAULT_ROTATE_SPEED;
	}


	private void Update ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i %2 == 0)
                stages[i].RotateAround(center.position,startOrientation, rotateSpeed * Time.deltaTime);
            else
                stages[i].RotateAround(center.position,-startOrientation, rotateSpeed * Time.deltaTime);
        }
    }
}

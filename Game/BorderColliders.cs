using UnityEngine;

public class BorderColliders : MonoBehaviour
{
	[SerializeField]private Vector3 borderDimension;
	[SerializeField]private Transform border;

	void Start ()
	{
		borderDimension = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		borderDimension.z = 0;
		border.transform.localScale = new Vector3 (borderDimension.x * 2, borderDimension.y * 2, borderDimension.z);
	}
}
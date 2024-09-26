using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxColliderScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
    }

	private void OnValidate()
	{
        GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
	}
}

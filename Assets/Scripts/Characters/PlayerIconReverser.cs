using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconReverser : MonoBehaviour
{
	public Transform self;
	public Transform playerTransform;

    void Update()
    {
		self.localScale = new Vector3(Mathf.Abs(self.localScale.x) * playerTransform.localScale.x, self.localScale.y, self.localScale.z);
    }
}

using System;
using UnityEngine;

public class GamerMovement : MonoBehaviour
{
	public float moveSpeed;
	public void Update()
	{
		var hInput = Input.GetAxisRaw("Horizontal");
		var vInput = Input.GetAxisRaw("Vertical");

		transform.position += new Vector3(hInput, 0f, vInput) * (moveSpeed * Time.deltaTime);
	}
}

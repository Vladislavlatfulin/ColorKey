using System;
using UnityEngine;
using Zenject;

public class Platform : MonoBehaviour
{
	public Material PlatformMaterial { get; private set; }

	private void Awake()
	{
		PlatformMaterial = GetComponent<MeshRenderer>().material;
	}
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class Level : IInitializable, ILateDisposable
{
	[Inject] private List<ColorChecking> _verificationPlatforms;
	[Inject] private ChangePlayerColor _player; 
	
	public void Initialize()
	{
	}

	public void LateDispose()
	{
		
	}
	
	[MenuItem("My menu/Say hello")]
	public static void SayHello() 
	{
		Debug.Log("hello");
	}
}
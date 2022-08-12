using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
	[SerializeField] private List<ColorChecking> verificationPlatforms;
	[SerializeField] private ChangePlayerColor player;
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<Level>()
			.AsSingle()
			.NonLazy();

		Container.Bind<List<ColorChecking>>().FromInstance(verificationPlatforms);

		Container.BindInstance<ChangePlayerColor>(player);

	}
	
}
using System;
using System.Diagnostics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ChangePlayerColor : MonoBehaviour
{
	[SerializeField] private GameObject skelet;
	
	public Color Color => _playerMaterial.color;

	private Material _playerMaterial;
	private Material _platformMaterial;
	private Platform _platform;

	private TweenerCore<Color, Color, ColorOptions> _currentAnimation;

	private void Start()
	{
		_playerMaterial = skelet.GetComponent<SkinnedMeshRenderer>().material;
	}

	private void Update()
	{
		if (_platform)
		{
			_platformMaterial = _platform.PlatformMaterial;
			_playerMaterial.color = Color.Lerp(_playerMaterial.color,
				CombineColor(_playerMaterial.color, _platformMaterial.color),
				Time.deltaTime);
		}
	}
	
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_platform = hit.gameObject.GetComponent<Platform>();
	}
	
	private Color CombineColor(params Color[] colors)
	{
		Color result = new Color(0, 0, 0, 255)
		{
			r = CalculateColorPartAverage(colors[0].r, colors[1].r),
			g = CalculateColorPartAverage(colors[0].g, colors[1].g),
			b = CalculateColorPartAverage(colors[0].b, colors[1].b)
		};

		return result;
	}

	private float CalculateColorPartAverage(float a, float b)
	{
		return (a + b * b) / 2;
	}

	public void ResetColor()
	{
		_playerMaterial.DOColor(Color.white, 2);
	}

}
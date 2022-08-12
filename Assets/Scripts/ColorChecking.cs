using System;
using DG.Tweening;
using UnityEngine;
using Zenject;


public class ColorChecking : MonoBehaviour
{
	[Inject] private ChangePlayerColor _player;
	[SerializeField] private GameObject door;

	private Color _doorColor;
	private float _finallyPercent;
	public float FinallyPercent
	{
		get => _finallyPercent;
		private set
		{
			_finallyPercent = value;
			if (_finallyPercent >= 85)
			{
				OpenDoor();
				_player.ResetColor();
			}
		}
	}

	private void Awake()
	{
		_doorColor = door.GetComponent<MeshRenderer>().material.color;
	}

	private void OnTriggerEnter(Collider other)
	{
		ChangePlayerColor player = other.gameObject.GetComponent<ChangePlayerColor>();
		if (!player)
			return;
		
		var doorColorHex = _doorColor.ToHexString();
		var playerColorHex = player.Color.ToHexString();

		Debug.Log("Door color hex - " + doorColorHex);
		Debug.Log("player color hex - " + playerColorHex);

		var doorRGBColor = ConvertHexColorToRGBColor(doorColorHex);
		var playerRGBColor = ConvertHexColorToRGBColor(playerColorHex);

		var percentageOfColors = GetPercentageOfColors(doorRGBColor, playerRGBColor);

		CalculateFinallyPercent(percentageOfColors);
		Debug.Log("Finally percent - " + FinallyPercent);
	}

	private Vector3Int ConvertHexColorToRGBColor(string hexColor)
	{
		var rgbColor = new Vector3Int
		{
			x = Convert.ToInt32(hexColor.Substring(0,2), 16),
			y = Convert.ToInt32(hexColor.Substring(2,2), 16),
			z = Convert.ToInt32(hexColor.Substring(4,2), 16)
		};

		return rgbColor;
	}

	private Vector3 GetPercentageOfColors(Vector3Int firstColor, Vector3Int secondColor)
	{
		var percentageOfColor = new Vector3
		{
			x = GetPercentChannel(firstColor.x, secondColor.x),
			y = GetPercentChannel(firstColor.y, secondColor.y),
			z = GetPercentChannel(firstColor.z, secondColor.z)
		};

		return percentageOfColor;
	}

	private void CalculateFinallyPercent(Vector3 percentageOfColors)
	{
		FinallyPercent = (percentageOfColors.x + percentageOfColors.y + percentageOfColors.z) / 3;
		FinallyPercent *= 100;
		FinallyPercent = 100 - FinallyPercent;
	}

	private float GetPercentChannel(int doorChannel, int playerChannel)
	{
		var percent = Mathf.Abs(doorChannel - playerChannel);
		return (float)percent / 255;
	}

	private void OpenDoor()
	{
		door.transform.DOMove(new Vector3(9, 0, 0), 5).SetRelative().OnComplete(CloseDoor);
	}

	private void CloseDoor()
	{
		door.transform.DOMove(new Vector3(-9, 0, 0), 2).SetRelative().SetRelative();
	}
}
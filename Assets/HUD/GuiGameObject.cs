using UnityEngine;
using System.Collections;

public class GuiGameObject : MonoBehaviour
{
	public Texture _textureP1Pointer, _textureP2Pointer, _textureP3Pointer, _textureP4Pointer, _miniBoomerang;

	private float[] _pointerFullWidths = new float[] {17.0f, 23.0f, 22.0f, 23.0f};
	private float[] _pointerHalfWidths = new float[] {8.0f, 11.0f, 11.0f, 11.0f};
	private Transform[] _playerTransforms;
	private PlayerController[] _playerControllers;
	private Texture[] _pointerTextures;

	void Start()
	{
		//Screen.SetResolution(512,288,true);
		//Screen.SetResolution(1024,576,true);
		_pointerTextures = new Texture[] {_textureP1Pointer, _textureP2Pointer, _textureP3Pointer, _textureP4Pointer};

		PlayerController[] players = (PlayerController[])GameObject.FindObjectsOfType(typeof(PlayerController));
		_playerTransforms = new Transform[players.Length];
		_playerControllers = new PlayerController[players.Length];

		foreach (PlayerController player in players) {
			_playerTransforms[player.joystick - 1] = player.transform;
			_playerControllers[player.joystick - 1] = player.GetComponent<PlayerController>();
		}
	}

	void OnGUI()
	{
		for (int i = 0; i < _playerTransforms.Length; i += 1)
		{
			Vector3 playerPosition = Camera.main.WorldToScreenPoint(_playerTransforms[i].position);
			GUI.DrawTexture(new Rect(playerPosition.x - _pointerHalfWidths[i], (float)Screen.height - playerPosition.y - 66.0f, _pointerFullWidths[i], 30.0f), _pointerTextures[i]);

			float xOffset = playerPosition.x - 10.0f;
			float yOffset = (float)Screen.height - playerPosition.y - 78.0f;
			int tempFireableBoomerangs = _playerControllers[i]._fireableBoomerangs;
			//int tempFireableBoomerangs = 9;
			int tempCounter = 0;
			while (tempFireableBoomerangs > 0)
			{
				GUI.DrawTexture(new Rect(xOffset, yOffset, 6.0f, 10.0f), _miniBoomerang);
				xOffset += 7.0f;
				tempCounter += 1;
				if (tempCounter == 3)
				{
					tempCounter = 0;
					yOffset -= 11.0f;
					xOffset -= 21.0f;
				}
				tempFireableBoomerangs -= 1;
			}
		}
	}
}
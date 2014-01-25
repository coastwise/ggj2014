using UnityEngine;
using System.Collections;

public class GuiGameObject : MonoBehaviour
{
	public Texture _textureP1Pointer, _textureP2Pointer, _textureP3Pointer, _textureP4Pointer;

	private float[] _pointerFullWidths = new float[] {17.0f, 23.0f, 22.0f, 23.0f};
	private float[] _pointerHalfWidths = new float[] {8.0f, 11.0f, 11.0f, 11.0f};
	private Transform[] _playerTransforms;
	private Texture[] _pointerTextures;

	void Start()
	{
		_pointerTextures = new Texture[] {_textureP1Pointer, _textureP2Pointer, _textureP3Pointer, _textureP4Pointer};
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		_playerTransforms = new Transform[gos.Length];
		foreach (GameObject go in gos)
			_playerTransforms[go.GetComponent<PlayerController>().joystick - 1] = go.transform;
	}

	void OnGUI()
	{
		for (int i = 0; i < _playerTransforms.Length; i += 1)
		{
			Vector3 playerPosition = Camera.main.WorldToScreenPoint(_playerTransforms[i].position);
			GUI.DrawTexture(new Rect(playerPosition.x - _pointerHalfWidths[i], 576.0f - playerPosition.y - 66.0f, _pointerFullWidths[i], 30.0f), _pointerTextures[i]);
		}
	}
}
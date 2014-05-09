using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerStateManager))]
public class PlayerController : MonoBehaviour {

	private PlayerStateManager _playerStateManager;
	public PlayerStateManager PlayerStateManager {
		get { return _playerStateManager; }
	}

	private MultiJump _multiJump;
	public MultiJump MultiJump {
		get { return _multiJump; }
	}

	private int _joystick;
	public int Joystick
	{
		get { return _joystick; }
		set { _joystick = value;}
	}

	public List<Transform> spawnPoints;

	private AudioClip[] sounds;

	[SerializeField]
	private float _respawnTimeout = 2f; // seconds
	public float RespawnTimeout {
		get { return _respawnTimeout; }
	}

	private bool _wallRight;
	public bool wallRight {
		get; private set;
	}

	[SerializeField]
	private bool _isInvincible = false;
	public bool Invincible {
		get { return _isInvincible; }
		set { _isInvincible = value; }
	}


	private Color _cachedPlayerTint;

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	// "is false when preceded by its quotation" is false when preceded by its quotation

	[SerializeField]
	private int _fireableBoomerangs = 3;
	public int FireableBoomerangs {
		get { return _fireableBoomerangs; }
		set { _fireableBoomerangs = value; }
	}

	public GameObject _boomerangPrefab;

	[SerializeField]
	private int _killcount = 0;
	public int KillCount {
		get { return _killcount; }
	}

	void Awake () {
		_playerStateManager = GetComponent<PlayerStateManager> ();
		_multiJump = GetComponent<MultiJump> ();
		sounds = new AudioClip[4];
		sounds [(int)SoundEffects.Explosion] = 	(AudioClip)Resources.Load ("explosion");
		sounds [(int)SoundEffects.Jump] = 		(AudioClip)Resources.Load ("jump");
		sounds [(int)SoundEffects.Throw] = 		(AudioClip)Resources.Load ("boomerang1");
		sounds [(int)SoundEffects.Explosion] = 	(AudioClip)Resources.Load ("walljump");
	}

	void Start () {
		name = "Player " + Joystick;
		_cachedPlayerTint = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void Update () {

	}

	public void FinishedExploding () {
		gameObject.GetComponent<SpriteRenderer>().color = _cachedPlayerTint;
		gameObject.renderer.enabled = false;
	}

	public void Respawn () {
		_isInvincible = true;
		gameObject.renderer.enabled = true;
		transform.position = spawnPoints[Random.Range(0,spawnPoints.Count)].position;
		StartCoroutine(BlinkForAWhile());
	}

	public IEnumerator BlinkForAWhile () {
		for(float i = 0; i < 2; i += Time.deltaTime) {
			yield return null;
			gameObject.renderer.enabled = false;
			yield return null;
			gameObject.renderer.enabled = true;
		}
		_isInvincible = false;
	}

	public void PlaySound (SoundEffects sfx) {
		gameObject.audio.PlayOneShot (sounds [(int)sfx]);
	}

	public void IncrementKill () {
		_killcount++;
	}
}

public enum SoundEffects {
	Explosion,
	Jump,
	Throw,
	WallJump
};

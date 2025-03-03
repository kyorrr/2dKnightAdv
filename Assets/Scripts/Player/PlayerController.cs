using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> {

	public bool FacingLeft { get { return _facingLeft; } }

	[SerializeField] private float _moveSpeed = 4f;
	[SerializeField] private float _dashSpeed = 4f;
	[SerializeField] private TrailRenderer _trailRenderer;
	[SerializeField] private Transform _weaponCollider;

	private PlayerControls _playerControls;
	private Vector2 _movement;
	private Rigidbody2D _rb;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private float _startingMoveSpeed;

	private bool _facingLeft = false;
	private bool _isDashing = false;

	protected override void Awake() {
		base.Awake();
		_playerControls = new PlayerControls();
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		_playerControls.Combat.Dash.performed += _ => Dash();

		_startingMoveSpeed = _moveSpeed;
	}

	private void OnEnable() {
		_playerControls.Enable();
	}

	private void Update() {
		PlayerInput();
	}

	private void FixedUpdate() {
		AdjustPlayerFacingDirection();
		Move();
	}

	public Transform GetWeaponCollider() {
		return _weaponCollider;
	}

	private void PlayerInput() {
		_movement = _playerControls.Movement.Move.ReadValue<Vector2>();

		_animator.SetFloat("moveX", _movement.x);
		_animator.SetFloat("moveY", _movement.y);
	}

	private void Move() {
		_rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
	}

	private void AdjustPlayerFacingDirection() {
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

		if (mousePos.x < playerScreenPoint.x) {
			_spriteRenderer.flipX = true;
			_facingLeft = true;
		}
		else {
			_spriteRenderer.flipX = false;
			_facingLeft = false;
		}
	}

	private void Dash() {
		if (!_isDashing) {
			_isDashing = true;
			_moveSpeed *= _dashSpeed;
			_trailRenderer.emitting = true;
			StartCoroutine(EndDashRoutine());
		}
	}

	private IEnumerator EndDashRoutine() {
		float dashTime = 0.2f;
		float dashCD = 0.25f;
		yield return new WaitForSeconds(dashTime);
		_moveSpeed = _startingMoveSpeed;
		_trailRenderer.emitting = false;
		yield return new WaitForSeconds(dashCD);
		_isDashing = false;
	}
}

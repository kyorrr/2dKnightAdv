using UnityEngine;

public class Bow : MonoBehaviour, IWeapon {

	[SerializeField] private WeaponInfo _weaponInfo;

	public void Attack() {
		Debug.Log("Bow Attack");
	}

	public WeaponInfo GetWeaponInfo() {
		return _weaponInfo;
	}
}

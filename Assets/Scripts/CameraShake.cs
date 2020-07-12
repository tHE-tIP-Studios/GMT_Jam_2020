using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	Vector3 originalPos;
	
	public static CameraShake Instance {get; private set;}
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = transform;
		}

		Instance = this;
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	public void Shake(float duration, float amount, float decreaseFactor)
	{
		StartCoroutine(ShakeRoutine(duration, amount, decreaseFactor));
	}

	private IEnumerator ShakeRoutine(float duration, float amount, float decreaseFactor)
	{
		while(duration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * amount;
			duration -= Time.deltaTime * decreaseFactor;
			yield return null;
		}
		camTransform.localPosition = originalPos;
	}
}

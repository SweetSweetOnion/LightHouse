using UnityEngine;
using System.Collections;
[SerializeField]
public class WaveArc
{

	private float _duration;
	private float _speed;
	private float _thickness;
	private float _scale;
	private float _angle;
	private float _angle2;
	private Vector3 _centerPoint;

	public WaveArc(float duration, float speed, float thickness,Vector3 center,float angle,float angle2)
	{
		_duration = duration;
		_speed = speed;
		_thickness = thickness;
		_centerPoint = center;
		_angle = angle;
		_angle2 = angle2;
	}

	public void Update()
	{
		_duration -= Time.deltaTime;
		if(_duration > 0)
		{
			_scale += Time.deltaTime * _speed;
		}
		IsInside();
	}


	public bool IsInside()
	{
		Vector3 v1 = _centerPoint + new Vector3(Mathf.Cos(_angle) * _scale, 0, Mathf.Sin(_angle) * _scale);
		Vector3 v2 = _centerPoint + new Vector3(Mathf.Cos(_angle2) * _scale, 0, Mathf.Sin(_angle2) * _scale);

		
		Vector3 v1t = v1 +  (_centerPoint - v1).normalized * _thickness;
		Vector3 v2t = v2 + (_centerPoint - v2).normalized * _thickness;

		Debug.DrawLine(v1, v2);
		Debug.DrawLine(v2, v2t);
		Debug.DrawLine(v2t, v1t);
		Debug.DrawLine(v1t, v1);

		return true;

	}
}

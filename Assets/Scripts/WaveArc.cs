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
	private Vector3 _p1, _p2, _p3, _p4;


	public float duration => _duration;
	public float scale => _scale;
	public Vector3 p1 => _p1;
	public Vector3 p2 => _p2;
	public Vector3 p3 => _p3;
	public Vector3 p4 => _p4;


	//contructor
	public WaveArc(float duration, float speed, float thickness, Vector3 center, float angle, float angle2)
	{
		_duration = duration;
		_speed = speed;
		_thickness = thickness;
		_centerPoint = new Vector3(center.x, 0, center.z);
		_angle = angle;
		_angle2 = angle2;
	}

	#region internal

	private void UpdatePos()
	{
		_p1 = _centerPoint + new Vector3(Mathf.Cos(_angle) * _scale, 0, Mathf.Sin(_angle) * _scale);
		_p2 = _centerPoint + new Vector3(Mathf.Cos(_angle2) * _scale, 0, Mathf.Sin(_angle2) * _scale);


		_p3 = _p1 + (_centerPoint - _p1).normalized * _thickness;
		_p4 = _p2 + (_centerPoint - _p2).normalized * _thickness;
	}

	private bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
	{
		var j = polyPoints.Length - 1;
		var inside = false;
		for (int i = 0; i < polyPoints.Length; j = i++)
		{
			var pi = polyPoints[i];
			var pj = polyPoints[j];
			if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
				(p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
				inside = !inside;
		}
		return inside;
	}

	#endregion

	#region public method

	public bool IsArcAlive()
	{
		return _duration > 0;
	}


	public void Update()
	{
		_duration -= Time.deltaTime;
		if (_duration > 0)
		{
			_scale += Time.deltaTime * _speed;
		}
		UpdatePos();

	}

	public void DebugDisplay()
	{
		Color c = new Color(1, 1, 1, duration / 10);
		Debug.DrawLine(_p1, _p2, c);
		Debug.DrawLine(_p2, _p4, c);
		Debug.DrawLine(_p4, _p3, c);
		Debug.DrawLine(_p3, _p1, c);
	}


	public bool IsInside(Vector3 point)
	{
		Vector2[] vs = new Vector2[4];
		vs[0] = new Vector2(_p1.x, _p1.z);
		vs[1] = new Vector2(_p2.x, _p2.z);
		vs[2] = new Vector2(_p3.x, _p3.z);
		vs[3] = new Vector2(_p4.x, _p4.z);
		return ContainsPoint(vs, new Vector2(point.x, point.z));

	}

	public void StopArc()
	{
		_duration = 0;
	}

	public void AddDuration(float amount)
	{
		_duration += amount;
	}

	#endregion


}

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
	private Vector3 p1, p2, p3, p4;

	public float duration => _duration;

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
		p1 = _centerPoint + new Vector3(Mathf.Cos(_angle) * _scale, 0, Mathf.Sin(_angle) * _scale);
		p2 = _centerPoint + new Vector3(Mathf.Cos(_angle2) * _scale, 0, Mathf.Sin(_angle2) * _scale);


		p3 = p1 + (_centerPoint - p1).normalized * _thickness;
		p4 = p2 + (_centerPoint - p2).normalized * _thickness;
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
		Debug.DrawLine(p1, p2, c);
		Debug.DrawLine(p2, p4, c);
		Debug.DrawLine(p4, p3, c);
		Debug.DrawLine(p3, p1, c);
	}


	public bool IsInside(Vector3 point)
	{
		Vector2[] vs = new Vector2[4];
		vs[0] = new Vector2(p1.x, p1.z);
		vs[1] = new Vector2(p2.x, p2.z);
		vs[2] = new Vector2(p3.x, p3.z);
		vs[3] = new Vector2(p4.x, p4.z);
		return ContainsPoint(vs, new Vector2(point.x, point.z));

	}

	#endregion


}

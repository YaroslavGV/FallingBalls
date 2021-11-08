using System;
using UnityEngine;

public class RequireInterface : PropertyAttribute
{
	public readonly Type RequireType;

	public RequireInterface (Type requireType)
	{
		RequireType = requireType;
	}
}

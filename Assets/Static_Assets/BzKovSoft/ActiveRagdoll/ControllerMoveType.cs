using UnityEngine;

namespace BzKovSoft.ActiveRagdoll
{
	/// <summary>
	/// 
	/// </summary>
	public enum ControllerMoveType
	{
		/// <summary>
		/// Character moved using transform.position changing
		/// </summary>
		Transform,
		/// <summary>
		/// Character moved using by applying physics forces or rigidbody.position change
		/// </summary>
		Rigidbody,
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionFunctions {

	///<summary>
	/// The CleanupList function runs through a list and removes all NULL elements. 
	/// The remaining elements get moved towards the front of the list.
	/// </summary>
	public static List<T> CleanupList<T>(List<T> list) {
		List<T> temp = new List<T>();
		foreach(T l in list) {
			if(l != null)
				temp.Add(l);
		}
		return temp;
	}
}

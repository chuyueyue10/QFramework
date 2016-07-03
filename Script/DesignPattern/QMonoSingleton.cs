﻿using UnityEngine;

/// <summary>
/// 需要使用Unity生命周期的单例模式
/// </summary>
namespace QFramework {
	
	public abstract class QMonoSingleton<T> : MonoBehaviour where T : QMonoSingleton<T>
	{
		protected static T mInstance = null;

		public static T Instance()
		{
			if (mInstance == null)
			{
				mInstance = FindObjectOfType<T>();

				if (FindObjectsOfType<T>().Length > 1)
				{
					Debug.LogError ("More than 1!");

					return mInstance;
				}

				if (mInstance == null)
				{
					string instanceName = typeof(T).Name;

					Debug.LogError ("Instance Name: " + instanceName); 

					GameObject instanceGO = GameObject.Find(instanceName);

					if (instanceGO == null)
						instanceGO = new GameObject(instanceName);
					mInstance = instanceGO.AddComponent<T>();

					DontDestroyOnLoad(instanceGO);	

					Debug.LogError ("Add New Singleton " + mInstance.name + " in Game!");

				}
				else
				{
					Debug.LogError ("Already exist: " + mInstance.name);
				}
			}

			return mInstance;
		}


		protected virtual void OnDestroy()
		{
			mInstance = null;
		}
	}

}
// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using FMODUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R3;
using TSS.Core;

namespace TSS.Audio
{
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class AudioSystem : IRuntimeLoader
    {
	    public static class Volumes
	    {
		    public static float MasterVolume
		    {
			    get
			    {
				    _masterBus.getVolume(out var volume);
				    return volume;
			    }
			    set
			    {
				    _masterBus.setVolume(value);
				    PlayerPrefs.SetFloat("master_volume", value);
			    }
		    }

		    public static float GetVolume(int index)
		    {
			    _buses[index].getVolume(out var volume);
			    return volume;
		    }

		    public static void SetVolume(int index, float volume)
		    {
			    _buses[index].setVolume(volume);
			    _buses[index].getID(out var id);
			    PlayerPrefs.SetFloat("volume_of_" + id, volume);
		    }
	    }
	    
		public static class Global
		{
		}

		public static SoundEvent_UI_Hover UI_Hover { get; } = new();
		public static SoundEvent_UI_Slider UI_Slider { get; } = new();
		public static SoundEvent_UI_Click UI_Click { get; } = new();
		public static SoundEvent_Game_SlidingDoor Game_SlidingDoor { get; } = new();
		public static SoundEvent_Game_IceBiom Game_IceBiom { get; } = new();
		public static SoundEvent_MainMenu_OST MainMenu_OST { get; } = new();
		public static SoundEvent_Game_SandBiom Game_SandBiom { get; } = new();
		public static SoundEvent_Game_HubBiom Game_HubBiom { get; } = new();
		public static SoundEvent_Game_JungleBiom Game_JungleBiom { get; } = new();
		public static SoundEvent_Game_SnowBiom Game_SnowBiom { get; } = new();
		public static SoundEvent_Game_BuyUpgrade Game_BuyUpgrade { get; } = new();
		public static SoundEvent_Game_KeyCollect Game_KeyCollect { get; } = new();
		public static SoundEvent_Game_ResourceCollect Game_ResourceCollect { get; } = new();
		public static SoundEvent_Game_DroneChill Game_DroneChill { get; } = new();
		public static SoundEvent_Game_Death Game_Death { get; } = new();
		public static SoundEvent_Game_ChestOpnen Game_ChestOpnen { get; } = new();
    
		private System.IDisposable _focusDisposable;
		
		private static FMOD.Studio.Bus _masterBus;
		private static FMOD.Studio.Bus[] _buses;
		
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			RuntimeManager.LoadBank("Master.strings", true);
			RuntimeManager.LoadBank("Master", true);

            await UniTask.WaitUntil(() => FMODUnity.RuntimeManager.HaveAllBanksLoaded);
            await UniTask.WaitWhile(FMODUnity.RuntimeManager.AnySampleDataLoading);
            
            var volumesSettings = await Addressables.LoadAssetAsync<AudioVolumesSettings>("Audio Volumes")
	            .ToUniTask(cancellationToken: cancellationToken);

            _masterBus = FMODUnity.RuntimeManager.GetBus(volumesSettings.MasterBusPath);
            _masterBus.setVolume(PlayerPrefs.GetFloat("master_volume", volumesSettings.DefaultMasterVolume));

            _buses = new FMOD.Studio.Bus[volumesSettings.BusesPaths.Length];
            for (int i = 0; i < _buses.Length; i++)
            {
	            _buses[i] = FMODUnity.RuntimeManager.GetBus(volumesSettings.BusesPaths[i]);
	            _buses[i].getID(out var busId);
	            _buses[i].setVolume(PlayerPrefs.GetFloat("volume_of_" + busId, volumesSettings.DefaultVolume));
            }
            
            Addressables.Release(volumesSettings);
            
            _focusDisposable = Runtime.ObserveFocus().Subscribe(focus =>
            {
	            if (RuntimeManager.StudioSystem.isValid())
	            {
		            RuntimeManager.PauseAllEvents(!focus);

		            if (!focus)
			            RuntimeManager.CoreSystem.mixerSuspend();
		            else
			            RuntimeManager.CoreSystem.mixerResume();
	            }
            });
        }

        public void Dispose() => _focusDisposable.Dispose();
    }

	public class SoundEvent_UI_Hover : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 896916501, Data2 = 1233405146, Data3 = -1268089204, Data4 = 273580992 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Slider : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1108191865, Data2 = 1075715523, Data3 = -641858942, Data4 = 139831584 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Click : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -73757971, Data2 = 1092300725, Data3 = 530329010, Data4 = 2045083333 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_SlidingDoor : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1804026631, Data2 = 1157304860, Data3 = -58913141, Data4 = -886315384 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_IceBiom : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 193032;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1194279727, Data2 = 1126821306, Data3 = -334783058, Data4 = 318193930 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_MainMenu_OST : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 240048;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1598096075, Data2 = 1227682573, Data3 = -472549705, Data4 = 315814700 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_SandBiom : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 78545;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1126276919, Data2 = 1140179111, Data3 = -667644750, Data4 = 1493517472 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_HubBiom : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 162204;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1509577823, Data2 = 1227662135, Data3 = 100315294, Data4 = -1596912409 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_JungleBiom : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 145999;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 2023196770, Data2 = 1173036713, Data3 = -1753940094, Data4 = 1507800373 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_SnowBiom : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 96188;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 891484837, Data2 = 1184425778, Data3 = -1293233255, Data4 = 707320835 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_BuyUpgrade : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1263659994, Data2 = 1224875621, Data3 = 1437319074, Data4 = -698307156 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_KeyCollect : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1897264091, Data2 = 1294456426, Data3 = 1153287355, Data4 = 652695927 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_ResourceCollect : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1911356674, Data2 = 1131494033, Data3 = -44541799, Data4 = -294980644 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_DroneChill : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 118680;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1040862844, Data2 = 1215391110, Data3 = 1509386166, Data4 = 775366312 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_Death : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1760315403, Data2 = 1178156507, Data3 = -1130217079, Data4 = 1437921845 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_Game_ChestOpnen : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 445722104, Data2 = 1279156158, Data3 = 532219558, Data4 = 989050421 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

}
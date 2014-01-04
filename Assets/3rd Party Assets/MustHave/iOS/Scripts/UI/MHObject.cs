using UnityEngine;
using System.Collections;

public class MHObject {
	public int tag;
	
	public MHObject()
	{
		tag = MHiOSManager.CreateUniqueTagForObject(this);
	}
	
	~MHObject()
	{
		MHiOSManager.RemoveUniqueTagForObject(tag);
	}
	
	public MHObject init()
	{
		return MHiOSManager.Instance.init(tag);
	}
	
	public void release()
	{
		MHiOSManager.Instance.release(tag);
	}
}

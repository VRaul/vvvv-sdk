﻿using System;
using System.Runtime.InteropServices;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.Streams;

namespace VVVV.Hosting.Pins.Input
{
    [ComVisible(false)]
	public class DiffInputPin<T> : InputPin<T>, IDiffSpread<T>
	{
		public DiffInputPin(IPluginHost host, IPluginIn pluginIn, IInStream<T> stream)
			: base(host, pluginIn, stream)
		{
		}
		
		public event SpreadChangedEventHander<T> Changed;
		
		protected SpreadChangedEventHander FChanged;
        event SpreadChangedEventHander IDiffSpread.Changed
        {
            add
            {
                FChanged += value;
            }
            remove
            {
                FChanged -= value;
            }
        }
		
		protected virtual void OnChanged()
		{
			if (Changed != null)
				Changed(this);
			if (FChanged != null)
			    FChanged(this);
		}
		
		public bool IsChanged
		{
			get;
			private set;
		}
		
		public override bool Sync()
		{
			IsChanged = base.Sync();
			
			if (IsChanged)
			{
				OnChanged();
			}
			
			return IsChanged;
		}
	}
}

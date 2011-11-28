using System;
using System.ComponentModel.Composition;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.Streams;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

namespace VVVV.Nodes
{
	public abstract class UnzipNode<T> : IPluginEvaluate
	{
		[Input("Input", BinSize = 1)]
		protected IInStream<T> FInputStream;

		[Output("Output", IsPinGroup = true)]
		protected IIOStream<IOutStream<T>> FOutputStreams;
		
		private readonly T[] FBuffer = new T[128];
		
		public void Evaluate(int SpreadMax)
		{
			FOutputStreams.SetLengthBy(FInputStream);
			
			var outputStreamsLength = FOutputStreams.Length;
			using (var reader = FInputStream.GetReader())
			{
				int i = 0;
				foreach (var outputStream in FOutputStreams)
				{
					int numSlicesToWrite = Math.Min(outputStream.Length, FBuffer.Length);
					
					reader.Position = i++;
					using (var writer = outputStream.GetWriter())
					{
						while (!writer.Eos)
						{
							reader.ReadCyclic(FBuffer, 0, numSlicesToWrite, outputStreamsLength);
							writer.Write(FBuffer, 0, numSlicesToWrite);
						}
					}
				}
			}
		}
	}
	
	[PluginInfo(Name = "Unzip", Category = "Value", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class ValueUnzipNode : UnzipNode<double>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "Spreads", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class Value2dUnzipNode : UnzipNode<ISpread<double>>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "2d", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class Vector2DUnzipNode : UnzipNode<Vector2D>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "3d", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class Vector3DUnzipNode : UnzipNode<Vector3D>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "4d", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class Vector4DUnzipNode : UnzipNode<Vector4D>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "Color", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class ColorUnzipNode : UnzipNode<RGBAColor>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "String", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class StringUnzipNode : UnzipNode<string>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "Transform", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class TransformUnzipNode : UnzipNode<Matrix4x4>
	{
		
	}
	
	[PluginInfo(Name = "Unzip", Category = "Enumerations", Help = "Unzips a spread into multiple spreads", Tags = "")]
	public class EnumUnzipNode : UnzipNode<EnumEntry>
	{
		
	}
}

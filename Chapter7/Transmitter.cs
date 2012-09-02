using System;

namespace CleanCode.Chapter7
{
	public class Transmitter
	{
		Channel[] _channels;

		public Transmitter (
			int retry,
			params Channel[] channels
			)
		{
			Retry = retry;
			_channels = channels;
		}

		public void Send()
		{
			for(int i = 0; i < _channels.Length; i++)
			{
				bool suc = false;

				try
				{
					for(int j = 0; j < Retry; j++)
					{
						try
						{
							Console.WriteLine("using channel {0}", _channels[i].Name);
							_channels[i].Send();
							suc = true;
							break;
						}
						catch(GeneralException ge)
						{
							continue;
						}
					}
				}
				catch(FatalException)
				{
					continue;
				}
				if (suc)
				{
					break;
				}
			}
		}

//		public void Send()
//		{
//			for(int i = 0; i < _channels.Length; i++)
//			{
//				try
//				{
//					if (SendToChannel(_channels[i]))
//					{
//						break;
//					}
//				}
//				catch(FatalException)
//				{
//				}
//			}
//		}
//
//		private bool SendToChannel (Channel channel)
//		{
//			for (int i = 0; i < Retry; i++) {
//				try {
//
//					Console.WriteLine ("using channel {0}", channel.Name);
//					channel.Send ();
//					return true;
//
//				} catch (GeneralException ge) {
//				}
//			}
//			return false;
//		}

		public int Retry
		{
			get;
			set;
		}
	}
}


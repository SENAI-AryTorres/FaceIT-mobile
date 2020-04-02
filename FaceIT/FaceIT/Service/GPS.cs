using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceIT.Service
{
    public class GPS
    {
		//Se a posição esta disponivel
		public bool IsLocationAvailable()
		{
			if (!CrossGeolocator.IsSupported)
				return false;

			return CrossGeolocator.Current.IsGeolocationAvailable;
		}

		//Obter posição atual
		public async Task<Position> GetCurrentPosition()
		{
			Position position = null;
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 100;

				position = await locator.GetLastKnownLocationAsync();

				if (position != null)
				{
					//got a cahched position, so let's use it.
					return position;
				}

				if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
				{
					//not available or enabled
					return null;
				}

				position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

			}
			catch (Exception)
			{
				return position;
			}

			if (position == null)
				return null;



			return position;
		}
	}
}

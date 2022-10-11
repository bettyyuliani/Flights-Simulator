using Dapper;
using Npgsql;

public class MelbourneSydney
{
  private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";

  private double latitude = -37.663712;
  private double longitude = 144.844788;
  private double destinationLatitude = -33.947346;
  private double destinationLongitude = 151.179428;
  private double altitude = 100;
  private double speed = 1;
  private Random random = new Random();
  private int counter = 0;

  public void inject()
  {
    double diffLatitude = destinationLatitude - latitude;
    double diffLongitude = destinationLongitude - longitude;

    var Sqlparams = new DynamicParameters();
    Sqlparams.Add("flightnumber", "MS304");
    Sqlparams.Add("origin", "Melbourne");
    Sqlparams.Add("origincoordinate", latitude + "," + longitude);
    Sqlparams.Add("destination", "Sydney");
    Sqlparams.Add("destinationcoordinate", destinationLatitude + "," + destinationLongitude);

    while (Math.Round(this.latitude, 1) != Math.Round(this.destinationLatitude, 1) && Math.Round(this.longitude, 1) != Math.Round(this.destinationLongitude, 1))
    {
      Sqlparams.Add("latitude", latitude);
      Sqlparams.Add("longitude", longitude);

      latitude += diffLatitude / 5 / 50;
      longitude += diffLongitude / 5 / 50;
      //100
      if (counter < 50)
      {
        Sqlparams.Add("altitude", altitude);
        Sqlparams.Add("speed", speed);

        altitude += 20 + random.Next(0, 10);
        speed += 18 + random.Next(0, 10);
      }
      //50
      else if (counter >= 50 && counter < 100)
      {
        Sqlparams.Add("altitude", altitude);
        Sqlparams.Add("speed", speed);

        altitude += random.Next(0, 10) - 5;
        speed += random.Next(0, 10) - 5;
      }
      // 50
      else if (counter >= 100 && counter < 150)
      {
        Sqlparams.Add("altitude", altitude);
        Sqlparams.Add("speed", speed);

        altitude += random.Next(0, 10) - 5;
        speed += random.Next(0, 10) - 5;
      }
      //100
      else if (counter >= 150 && counter < 200)
      {
        Sqlparams.Add("altitude", altitude);
        Sqlparams.Add("speed", speed);

        altitude += random.Next(0, 10) - 5;
        speed += random.Next(0, 10) - 5;
      }
      else
      {
        Sqlparams.Add("altitude", altitude);
        Sqlparams.Add("speed", speed);

        altitude -= 20 + random.Next(0, 10);

        if (altitude < 10) altitude = 10;
        speed -= 18 + random.Next(0, 10);

        if (speed < 10) speed = 10;
      }

      counter++;

      if (counter > 250)
      {
        break;
      }

      string sql = @"INSERT INTO public.flightinfo(flightnumber, origin, origincoordinate, destination, destinationcoordinate, latitude, longitude, altitude, speed)" +
                    " VALUES(@flightnumber, @origin, @origincoordinate, @destination, @destinationcoordinate, @latitude, @longitude, @altitude, @speed);";
      using (var connection = new NpgsqlConnection(CONNECTION_STRING))
      {
        connection.ExecuteScalar(sql, Sqlparams);
      }
    }
  }
}


// === USING CURVE ===

// using Dapper;
// using Npgsql;

// public class MelbourneSydney
// {
//   private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";

//   private double latitude = -37.663712;
//   private double longitude = 144.844788;
//   private double destinationLatitude = -33.947346;
//   private double destinationLongitude = 151.179428;
//   private double altitude = 100;
//   private double speed = 1;
//   private Random random = new Random();
//   private int counter = 0;

//   public void inject()
//   {
//     var Sqlparams = new DynamicParameters();
//     Sqlparams.Add("flightnumber", "MS304");
//     Sqlparams.Add("origin", "Melbourne");
//     Sqlparams.Add("origincoordinate", latitude + "," + longitude);
//     Sqlparams.Add("destination", "Sydney");
//     Sqlparams.Add("destinationcoordinate", destinationLatitude + "," + destinationLongitude);

//     while (Math.Round(this.latitude, 1) != Math.Round(this.destinationLatitude, 1) && Math.Round(this.longitude, 1) != Math.Round(this.destinationLongitude, 1))
//     {
//       if (latitude < -36.72883 && longitude < 145.935256)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += 0.02;
//         longitude += 0.024;
//         altitude += 130 + random.Next(0, 10);
//         speed += 3 + random.Next(0,10);
//       }
//       else if (latitude >= -36.72883 && longitude >= 145.935256 && latitude < -35.819986 && longitude < 147.229609)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);


//         latitude += 0.02;
//         longitude += 0.029;
//         altitude += 50 + random.Next(0, 10);
//         speed += 8 + random.Next(0,10);
//       }
//       else if (latitude >= -35.819986 && longitude >= 147.229609 && latitude < -35.218922 && longitude < 148.245032)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += 0.02;
//         longitude += 0.033;
//         altitude += 20 + random.Next(0, 10);
//         speed += 5 + random.Next(0,10);
//       }
//       else if (latitude >= -35.218922 && longitude >= 148.245032 && latitude < -34.778560 && longitude < 149.119274)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += 0.011;
//         longitude += 0.02;
//         altitude -= random.Next(20, 50);
//         speed += random.Next(0,20) - 10;
//       }
//       else if (latitude >= -34.778560 && longitude >= 149.119274 && latitude < -34.380661 && longitude < 150.020666)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += 0.011;
//         longitude += 0.03;
//         altitude -= random.Next(60, 100);
//         speed -= random.Next(0,35);
//       }
//       else
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += 0.011;
//         longitude += 0.029;
//         altitude -= random.Next(150, 220);
//         speed -= random.Next(0,65);

//         if (speed < 0) speed = 10;
//       }
//       counter++;

//       if (counter > 200)
//       {
//         break;
//       }

//       string sql = @"INSERT INTO public.flightinfo(flightnumber, origin, origincoordinate, destination, destinationcoordinate, latitude, longitude, altitude, speed)" +
//                     " VALUES(@flightnumber, @origin, @origincoordinate, @destination, @destinationcoordinate, @latitude, @longitude, @altitude, @speed);";
//       using (var connection = new NpgsqlConnection(CONNECTION_STRING))
//       {
//         connection.ExecuteScalar(sql, Sqlparams);
//       }
//     }
//   }

// }

using Dapper;
using Npgsql;

public class BrisbaneMelbourne
{
  private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";

  private double latitude = -27.383333;
  private double longitude = 153.118332;
  private double destinationLatitude = -37.663712;
  private double destinationLongitude = 144.844788;
  private double altitude = 100;
  private double speed = 1;
  private Random random = new Random();
  private int counter = 0;

  public void inject()
  {
    double diffLatitude = destinationLatitude - latitude;
    double diffLongitude = destinationLongitude - longitude;

    var Sqlparams = new DynamicParameters();
    Sqlparams.Add("flightnumber", "BM289");
    Sqlparams.Add("origin", "Brisbane");
    Sqlparams.Add("origincoordinate", latitude + "," + longitude);
    Sqlparams.Add("destination", "Melbourne");
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

// using Dapper;
// using Npgsql;

// public class BrisbaneMelbourne
// {
//   private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";

//   private double latitude = -27.383333;
//   private double longitude = 153.118332;
//   private double destinationLatitude = -37.663712;
//   private double destinationLongitude = 144.844788;
//   private double altitude = 100;
//   private double speed = 1;
//   private Random random = new Random();
//   private int counter = 0;

//   public void inject()
//   {
//     var Sqlparams = new DynamicParameters();
//     Sqlparams.Add("flightnumber", "BM289");
//     Sqlparams.Add("origin", "Brisbane");
//     Sqlparams.Add("origincoordinate", latitude + "," + longitude);
//     Sqlparams.Add("destination", "Melbourne");
//     Sqlparams.Add("destinationcoordinate", destinationLatitude + "," + destinationLongitude);

//     while (Math.Round(this.latitude, 1) != Math.Round(this.destinationLatitude, 1) && Math.Round(this.longitude, 1) != Math.Round(this.destinationLongitude, 1))
//     {
//       //30
//       if (latitude > -27.759653 && longitude > 152.665248)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.013;
//         longitude += -0.015;
//         altitude += 90 + random.Next(0, 10);
//         speed += 10 + random.Next(0,10);
//       }
//       //40
//       else if (latitude <= -27.759653 && longitude <= 152.6652486 && latitude > -29.162082 && longitude > 151.063397)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.035;
//         longitude += -0.04;
//         altitude += 60 + random.Next(0, 10);
//         speed += 6 + random.Next(0,10);
//       }
//       // 50
//       else if (latitude <= -29.162082 && longitude <= 151.063397 && latitude > -30.207175 && longitude > 150.011860)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.021;
//         longitude += -0.021;
//         altitude += 50 + random.Next(0, 10);
//         speed += 3 + random.Next(0,10);
//       }
//       //100
//       else if (latitude <= -30.207175 && longitude <= 150.011860 && latitude > -32.547330 && longitude > 147.997710)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.0234;
//         longitude += -0.02;
//         altitude += 20 + random.Next(0, 10);
//         speed += random.Next(0,10) - 5;
//       }
//       //20
//       else if (latitude <= -32.547330 && longitude <= 147.997710 && latitude > -33.324326 && longitude > 147.389720)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.0385;
//         longitude += -0.03;
//         altitude += random.Next(0, 20) - 10;
//         speed += random.Next(0,10) - 5;
//       }
//       // 25
//       else if (latitude <= -33.324326 && longitude <= 147.389720 && latitude > -34.448440 && longitude > 146.629733)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.045;
//         longitude += -0.03;
//         altitude -= random.Next(10, 30);
//         speed -= random.Next(0,10);
//       }
//       //25
//       else if (latitude <= -34.448440 && longitude <= 146.629733 && latitude > -35.501400 && longitude > 145.980289)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.042;
//         longitude += -0.026;
//         altitude -= random.Next(30, 50);
//         speed -= random.Next(0,10);
//       }
//       //50
//       else if (latitude <= -35.501400 && longitude <= 145.980289 && latitude > -36.518529 && longitude >  145.399935)
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.02;
//         longitude += -0.01;
//         altitude -= random.Next(30, 80);
//         speed -= random.Next(0,10);
//       }
//       //50
//       else
//       {
//         Sqlparams.Add("latitude", latitude);
//         Sqlparams.Add("longitude", longitude);
//         Sqlparams.Add("altitude", altitude);
//         Sqlparams.Add("speed", speed);

//         latitude += -0.023;
//         longitude += -0.015;
//         altitude -= random.Next(50, 100);
//         speed -= random.Next(0,30);

//         if (speed < 10) speed = 10;
//       }
//       counter++;

//       if (counter > 420)
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

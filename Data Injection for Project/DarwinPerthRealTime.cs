using Dapper;
using Npgsql;

public class DarwinPerthRealTime
{
  private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";
  private double sourceLatitude = -12.415422;
  private double sourceLongitude = 130.88291;
  private double latitude = -12.415422;
  private double longitude = 130.88291;
  private double destinationLatitude = -31.940144;
  private double destinationLongitude = 115.967105;

  private double altitude = 100;
  private double speed = 1;
  private Random random = new Random();
  private int counter = 0;

  public void inject()
  {
    double diffLatitude = destinationLatitude - latitude;
    double diffLongitude = destinationLongitude - longitude;

    var Sqlparams = new DynamicParameters();
    Sqlparams.Add("flightnumber", "DP104");
    Sqlparams.Add("origin", "Darwin");
    Sqlparams.Add("origincoordinate", latitude + "," + longitude);
    Sqlparams.Add("destination", "Perth");
    Sqlparams.Add("destinationcoordinate", destinationLatitude + "," + destinationLongitude);

    while (true)
    {
      latitude = sourceLatitude;
      longitude = sourceLongitude;
      altitude = 100;
      speed = 1;
      while (Math.Round(this.latitude, 2) != Math.Round(this.destinationLatitude, 2) && Math.Round(this.longitude, 2) != Math.Round(this.destinationLongitude, 2))
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

        string sql = @"UPDATE public.flightinforealtime set latitude=@latitude, longitude=@longitude, altitude=@altitude, speed=@speed where id=3;";

        using (var connection = new NpgsqlConnection(CONNECTION_STRING))
        {
          connection.ExecuteScalar(sql, Sqlparams);
        }
        Thread.Sleep(1500);
      }
    }
  }
}

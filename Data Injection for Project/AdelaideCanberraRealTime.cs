using Dapper;
using Npgsql;

public class AdelaideCanberraRealTime
{
  private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=postgres";
  private double sourceLatitude = -34.944999;
  private double sourceLongitude = 138.530564;
  private double latitude = -34.944999;
  private double longitude = 138.530564;
  private double destinationLatitude = -35.306942;
  private double destinationLongitude = 149.194996;
  private double altitude = 100;
  private double speed = 1;
  private Random random = new Random();
  private int counter = 0;

  public void inject()
  {
    double diffLatitude = destinationLatitude - sourceLatitude;
    double diffLongitude = destinationLongitude - sourceLongitude;

    var Sqlparams = new DynamicParameters();
    Sqlparams.Add("flightnumber", "AC528");
    Sqlparams.Add("origin", "Adelaide");
    Sqlparams.Add("origincoordinate", latitude + "," + longitude);
    Sqlparams.Add("destination", "Canberra");
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

        latitude -= 0.361943 / 5 / 50;
        longitude += 10.664432 / 5 / 50;
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

        string sql = @"UPDATE public.flightinforealtime set latitude=@latitude, longitude=@longitude, altitude=@altitude, speed=@speed where id=4;";

        using (var connection = new NpgsqlConnection(CONNECTION_STRING))
        {
          connection.ExecuteScalar(sql, Sqlparams);
        }

        Thread.Sleep(1500);
      }
    }
  }
}

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.swing.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Scanner;

public class Map implements Runnable {

    final String API_KEY = <YOUR API KEY>;
    private final int id;
    private final String origin, destination, iconLink, fileName;
    private String flightID;
    public PlaneLocationBoard board;
    private Plane plane;

    public Map(int id, String origin, String destination, String iconLink, PlaneLocationBoard board, Plane plane) {
        this.id = id;
        this.origin = origin;
        this.destination = destination;
        this.board = board;
        this.iconLink = iconLink;
        this.plane = plane;
        fileName = "image" + id + ".jpg";
    }

    private String roundOffTo6DecPlaces(double val) {
        return String.format("%.6f", val);
    }

    @Override
    public void run() {
        PlaneLocationBoard.MapLabel label;
        JPanel panel = board.panel;
        JSONArray arr = null;

        try {
            URL _url = new URL("https://b9fe-103-27-175-138.au.ngrok.io/api/flight-points/" + origin + "/" + destination);
            HttpURLConnection conn = (HttpURLConnection) _url.openConnection();
            conn.setRequestMethod("GET");
            conn.connect();

            int responseCode = conn.getResponseCode();

            if (responseCode != 200) {
                throw new RuntimeException("HttpResponseCode: " + responseCode);
            } else {

                StringBuilder informationString = new StringBuilder();
                Scanner scanner = new Scanner(_url.openStream());

                while (scanner.hasNext()) {
                    informationString.append(scanner.nextLine());
                }
                scanner.close();

                JSONParser parse = new JSONParser();
                arr = (JSONArray) parse.parse(String.valueOf(informationString));
                JSONObject obj = (JSONObject) arr.get(0);
                flightID = obj.get("flightNumber").toString();
            }
        } catch (IOException e) {
            e.printStackTrace();
            System.exit(1);
        } catch (ParseException e) {
            throw new RuntimeException(e);
        }

        label = (PlaneLocationBoard.MapLabel) panel.getComponent(id);
        label.addMouseListener(new MouseListener() {
            @Override
            public void mouseClicked(MouseEvent e) {
                plane.simulationBoard.display("Passengers States in Plane " + flightID + " flying from " + origin + " to " + destination);
            }

            @Override
            public void mousePressed(MouseEvent e) {

            }

            @Override
            public void mouseReleased(MouseEvent e) {

            }

            @Override
            public void mouseEntered(MouseEvent e) {

            }

            @Override
            public void mouseExited(MouseEvent e) {

            }


        });

        while (true) {
            try {
                for (Object o : arr) {
                    JSONObject obj = (JSONObject) o;
                    String latitude = roundOffTo6DecPlaces((double) obj.get("latitude"));
                    String longitude = roundOffTo6DecPlaces((double) obj.get("longitude"));
                    String speed = obj.get("speed").toString();
                    String altitude = obj.get("altitude").toString();
                    String coordinate = latitude + "," + longitude;

                    String imageUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" +
                            coordinate + "&zoom=10&&markers=icon:" + iconLink + "%7C" + coordinate +
                            "&size=400x350&scale=2&maptype=roadmap&key=" + API_KEY;

                    URL url = new URL(imageUrl);
                    InputStream is = url.openStream();
                    OutputStream os = new FileOutputStream(fileName);

                    byte[] b = new byte[2048];
                    int length;

                    while ((length = is.read(b)) != -1) {
                        os.write(b, 0, length);
                    }

                    is.close();
                    os.close();

                    ImageIcon map = new ImageIcon(new ImageIcon(fileName).getImage().getScaledInstance(400, 350, java.awt.Image.SCALE_SMOOTH));
                    label.setIcon(map);
                    label.setText("<html>" + "Flight Number: " + flightID + "<br>" +
                            "Origin: " + origin + " | Destination: " + destination + "<br>" +
                            "Coordinate: " + coordinate + "<br>" +
                            "Speed: " + speed + " kph | " + "Altitude: " + altitude + "</html>");
                    try {
                        Thread.sleep(1500);
                    } catch (InterruptedException x) {
                        throw new RuntimeException(x);
                    }
                }
            } catch (MalformedURLException e) {
                throw new RuntimeException(e);
            } catch (IOException e) {
                e.printStackTrace();
                System.exit(1);
            }
        }
    }
}

//// SECOND IMPLEMENTATION
//// GETTING REAL-TIME API DATA BELOW
//
//import org.json.simple.JSONArray;
//import org.json.simple.JSONObject;
//import org.json.simple.parser.JSONParser;
//import org.json.simple.parser.ParseException;
//
//import javax.swing.*;
//import java.awt.event.MouseEvent;
//import java.awt.event.MouseListener;
//import java.io.*;
//import java.net.HttpURLConnection;
//import java.net.MalformedURLException;
//import java.net.URL;
//import java.util.Scanner;
//
//public class Map implements Runnable {
//
//    final String API_KEY = "AIzaSyCkoaeyCC8hhozMxy0OR_zBvlQvx1QuwAY";
//    private final int id;
//    private final String origin, destination, iconLink, fileName;
//    private String flightID;
//    public PlaneLocationBoard board;
//    private Plane plane;
//
//    public Map(int id, String origin, String destination, String iconLink, PlaneLocationBoard board, Plane plane) {
//        this.id = id;
//        this.origin = origin;
//        this.destination = destination;
//        this.board = board;
//        this.iconLink = iconLink;
//        this.plane = plane;
//        fileName = "image" + id + ".jpg";
//    }
//
//    private String roundOffTo6DecPlaces(double val) {
//        return String.format("%.6f", val);
//    }
//
//    @Override
//    public void run() {
//        PlaneLocationBoard.MapLabel label;
//        JPanel panel = board.panel;
//        JSONObject obj = null;
//
//        label = (PlaneLocationBoard.MapLabel) panel.getComponent(id);
//        label.addMouseListener(new MouseListener() {
//            @Override
//            public void mouseClicked(MouseEvent e) {
//                plane.simulationBoard.display("Passengers States in Plane " + "Flying from " + origin + " to " + destination);
//            }
//
//            @Override
//            public void mousePressed(MouseEvent e) {
//
//            }
//
//            @Override
//            public void mouseReleased(MouseEvent e) {
//
//            }
//
//            @Override
//            public void mouseEntered(MouseEvent e) {
//
//            }
//
//            @Override
//            public void mouseExited(MouseEvent e) {
//
//            }
//
//
//        });
//
//        while (true) {
//            try {
//                URL _url = new URL("https://b9fe-103-27-175-138.au.ngrok.io/api/flight-points/realtime/" + origin + "/" + destination);
//                HttpURLConnection conn = (HttpURLConnection) _url.openConnection();
//                conn.setRequestMethod("GET");
//                conn.connect();
//
//                int responseCode = conn.getResponseCode();
//
//                if (responseCode != 200) {
//                    throw new RuntimeException("HttpResponseCode: " + responseCode);
//                } else {
//
//                    StringBuilder informationString = new StringBuilder();
//                    Scanner scanner = new Scanner(_url.openStream());
//
//                    while (scanner.hasNext()) {
//                        informationString.append(scanner.nextLine());
//                    }
//                    scanner.close();
//
//                    JSONParser parse = new JSONParser();
//                    obj = (JSONObject) parse.parse(String.valueOf(informationString));
//                    flightID = obj.get("flightNumber").toString();
//                }
//                String latitude = roundOffTo6DecPlaces((double) obj.get("latitude"));
//                String longitude = roundOffTo6DecPlaces((double) obj.get("longitude"));
//                String speed = obj.get("speed").toString();
//                String altitude = obj.get("altitude").toString();
//                String coordinate = latitude + "," + longitude;
//
//                String imageUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + coordinate + "&zoom=10&&markers=icon:" + iconLink + "%7C" + coordinate + "&size=400x350&scale=2&maptype=roadmap&key=" + API_KEY;
//
//                URL url = new URL(imageUrl);
//                InputStream is = url.openStream();
//                OutputStream os = new FileOutputStream(fileName);
//
//                byte[] b = new byte[2048];
//                int length;
//
//                while ((length = is.read(b)) != -1) {
//                    os.write(b, 0, length);
//                }
//
//                is.close();
//                os.close();
//
//                ImageIcon map = new ImageIcon(new ImageIcon(fileName).getImage().getScaledInstance(400, 350, java.awt.Image.SCALE_SMOOTH));
//                label.setIcon(map);
//                label.setText("<html>" + "Flight Number: " + flightID + "<br>" +
//                        "Origin: " + origin + " | Destination: " + destination + "<br>" +
//                        "Coordinate: " + coordinate + "<br>" +
//                        "Speed: " + speed + " kph | " + "Altitude: " + altitude + "</html>");
//                try {
//                    Thread.sleep(1500);
//                } catch (InterruptedException x) {
//                    throw new RuntimeException(x);
//                }
//            } catch (MalformedURLException e) {
//                throw new RuntimeException(e);
//            } catch (IOException e) {
//                e.printStackTrace();
//                System.exit(1);
//            } catch (ParseException e) {
//                throw new RuntimeException(e);
//            }
//        }
//    }
//}

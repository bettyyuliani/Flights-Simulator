import org.json.simple.parser.ParseException;

public class FlightsSimulation {

    public static void main(String[] args) throws InterruptedException {

        PlaneLocationBoard board = new PlaneLocationBoard(4, 2, 2);

        Thread t1 = new Thread(new Map(0,"Melbourne", "Sydney", "https://i.ibb.co/W5y9sxD/plane-45.png", board, new Plane(54, new SimulationBoard(54))));
        Thread t2 = new Thread(new Map(1,"Brisbane", "Melbourne", "https://i.ibb.co/cbCBshg/plane-110.png", board, new Plane(63, new SimulationBoard(63))));
        Thread t3 = new Thread(new Map(2,"Darwin", "Perth", "https://i.ibb.co/KymYss5/plane-225.png", board, new Plane(72, new SimulationBoard(72))));
        Thread t4 = new Thread(new Map(3,"Adelaide", "Canberra","https://i.ibb.co/gvzzGR2/plane-100.png", board, new Plane(81, new SimulationBoard(81))));

        t1.start();
        t2.start();
        t3.start();
        t4.start();

        t1.join();
        t2.join();
        t3.join();
        t4.join();
    }
}

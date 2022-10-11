public class Plane {
    public int size;
    public SimulationBoard simulationBoard;
    public int unbuckledCount;
    public int leftSeatCount;
    public int callingFlightAttendantCount;
    private int counter = 0;

    public Plane(int size, SimulationBoard simulationBoard) {
        this.size = size;
        this.simulationBoard = simulationBoard;

        for (int i = 0; i < size; i++) {
            AddPassengers();
        }

    }

    private void AddPassengers(){
        Passenger passenger = new Passenger(this);
        Seat seat = new Seat(counter);
        counter++;
        passenger.Seat = seat;
        passenger.start();
    }
}

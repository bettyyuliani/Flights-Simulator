public abstract class PassengerActionState {
    abstract void setButtonLabel(SimulationBoard.Seat button, String id, String buckleState);
    void setOtherLabels(String currentState, String previousState, int leftSeatCount, int callFACount, Plane plane) {
        synchronized (Passenger.class) {
            if (previousState != null && previousState.equals(PassengerState.LEFT_SEATS.toString()))
            {
                leftSeatCount = --plane.leftSeatCount;
                plane.simulationBoard.leftSeatLabel.setText("<html> Passengers not on Seat: <br>" + leftSeatCount + "</html>");
            }
            if (previousState != null && previousState.equals(PassengerState.CALL.toString()))
            {
                callFACount = --plane.callingFlightAttendantCount;
                plane.simulationBoard.callingLabel.setText("<html> Passengers calling FA: <br>" + callFACount + "</html>");
            }
        }
    }
}
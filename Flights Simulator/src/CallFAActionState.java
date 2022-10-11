import java.awt.*;

public class CallFAActionState extends PassengerActionState {
    @Override
    public void setButtonLabel(SimulationBoard.Seat button, String id, String buckleState) {
        button.setBackground(Color.red);
        button.setText("<html>" + id + " : " + buckleState + "<br>" + " calling " + "flight attendant" + "</html>");
    }

    @Override
    public void setOtherLabels(String currentState, String previousState, int leftSeatCount, int callFACount, Plane plane) {
        synchronized (Passenger.class) {
            if (previousState != null && previousState.equals(PassengerState.LEFT_SEATS.toString()))
            {
                leftSeatCount = --plane.leftSeatCount;
                plane.simulationBoard.leftSeatLabel.setText("<html> Passengers not on Seat: <br>" + leftSeatCount + "</html>");
            }
            if (!currentState.equals(previousState))
            {
                callFACount = ++plane.callingFlightAttendantCount;
                plane.simulationBoard.callingLabel.setText("<html> Passengers calling FA: <br>" + callFACount + "</html>");
            }
        }
    }
}

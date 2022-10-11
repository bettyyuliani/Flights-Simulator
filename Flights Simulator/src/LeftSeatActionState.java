import java.awt.*;

public class LeftSeatActionState extends PassengerActionState {
    @Override
    void setButtonLabel(SimulationBoard.Seat button, String id, String buckleState) {
        button.setBackground(Color.blue);
        button.setText("<html>" + id  + " : " + buckleState + "<br>" + " left seats" + "</html>");
    }

    @Override
    void setOtherLabels(String currentState, String previousState, int leftSeatCount, int callFACount, Plane plane) {
        synchronized (Passenger.class) {
            if (!currentState.equals(previousState))
            {
                leftSeatCount = ++plane.leftSeatCount;
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

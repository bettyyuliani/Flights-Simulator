import java.awt.*;

public class SleepActionState extends PassengerActionState {
    @Override
    public void setButtonLabel(SimulationBoard.Seat button, String id, String buckleState) {
        button.setBackground(Color.GRAY);
        button.setText("<html>" + id + " : " + buckleState + "<br>" + " sleeping" + "</html>");
    }

    @Override
    void setOtherLabels(String currentState, String previousState, int leftSeatCount, int callFACount, Plane plane) {
        super.setOtherLabels(currentState, previousState, leftSeatCount, callFACount, plane);
    }
}
import java.awt.*;

public class AwakeActionState extends PassengerActionState {
    @Override
    public void setButtonLabel(SimulationBoard.Seat button, String id, String buckleState) {
        button.setBackground(Color.green);
        button.setText("<html>" + id + " : " + buckleState + "<br>" + " awake" + "</html>");
    }

    @Override
    void setOtherLabels(String currentState, String previousState, int leftSeatCount, int callFACount, Plane plane) {
        super.setOtherLabels(currentState, previousState, leftSeatCount, callFACount, plane);
    }
}

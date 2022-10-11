import javax.swing.*;
import java.util.Random;

public class Passenger extends Thread {
    private final Plane plane;
    private final int size;
    public Seat Seat;
    private Random rand = new Random();

    private String previousBuckledState = null;
    private String previousState = null;
    public Passenger(Plane plane) {
        this.plane = plane;
        size = plane.size;
    }

    private void waitRandomTime(int millis){
        int x = rand.nextInt(millis+1);
        try {
            sleep(millis + x);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void run(){

        SimulationBoard.Seat button;
        int componentID = -1;
        String displaySeatID = "";

        if (Seat.ID < size/3) {
            componentID = 0;
            displaySeatID = "A"+ (Seat.ID -(componentID*size/3));
        }
        else if (Seat.ID >= size/3 && Seat.ID < size*2/3)
        {
            componentID = 1;
            displaySeatID = "B"+ (Seat.ID -(componentID*size/3));
        }
        else
        {
            componentID = 2;
            displaySeatID = "C"+ (Seat.ID -(componentID*size/3));
        }

        button = (SimulationBoard.Seat) ((JPanel) plane.simulationBoard.getComponent(componentID)).getComponent(Seat.ID -(componentID*size/3));

        String buckle = "";
        int unbuckledCount = 0;

        while(true) {
            int randomState = rand.nextInt(4);
            int x = rand.nextInt(1001);
            if (x < 300 || randomState == 1)
            {
                synchronized (this) {
                    buckle = " Unbuckled";
                    if (!buckle.equals(previousBuckledState))
                    {
                        unbuckledCount = ++plane.unbuckledCount;
                        plane.simulationBoard.unbuckledLabel.setText("<html> Unbuckled Passengers: <br>" + unbuckledCount + "</html>");
                    }

                }
            }
            else
            {
                synchronized (this) {
                    buckle = " Buckled";
                    if (previousBuckledState != null && !buckle.equals(previousBuckledState)) {
                        unbuckledCount = --plane.unbuckledCount;
                        plane.simulationBoard.unbuckledLabel.setText("<html> Unbuckled Passengers: <br>" + unbuckledCount + "</html>");
                    }
                }
            }

            previousBuckledState = buckle;

            PassengerState stateEnum = PassengerState.values()[randomState];

            int leftSeatCount = 0;
            int callFACount = 0;
            switch(stateEnum)
            {
                case SLEEP:
                    PassengerActionState state = new SleepActionState();
                    state.setButtonLabel(button, displaySeatID, buckle);
                    state.setOtherLabels(stateEnum.toString(), previousState, leftSeatCount, callFACount, plane);
                    waitRandomTime(3000);
                    break;
                case LEFT_SEATS:
                    state = new LeftSeatActionState();
                    state.setButtonLabel(button, displaySeatID, buckle);
                    state.setOtherLabels(stateEnum.toString(), previousState, leftSeatCount, callFACount, plane);
                    waitRandomTime(1000);
                    break;
                case AWAKE:
                    state = new AwakeActionState();
                    state.setButtonLabel(button, displaySeatID, buckle);
                    state.setOtherLabels(stateEnum.toString(), previousState, leftSeatCount, callFACount, plane);
                    waitRandomTime(2000);
                    break;
                case CALL:
                    state = new CallFAActionState();
                    state.setButtonLabel(button, displaySeatID, buckle);
                    state.setOtherLabels(stateEnum.toString(), previousState, leftSeatCount, callFACount, plane);
                    waitRandomTime(1000);
                    break;
            }
            previousState = stateEnum.toString();
        }
    }
}

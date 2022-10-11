import javax.swing.*;
import java.awt.*;

public class SimulationBoard extends JPanel {
    public JLabel unbuckledLabel;
    public JLabel leftSeatLabel;
    public JLabel callingLabel;
    private JFrame frame;

    public SimulationBoard(int N) {
        super(new GridLayout(1,3));
        JPanel panel1 = new JPanel();
        panel1.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 25));
        JPanel panel2 = new JPanel();
        panel2.setBorder(BorderFactory.createEmptyBorder(5, 25, 5, 25));
        JPanel panel3 = new JPanel();
        panel3.setBorder(BorderFactory.createEmptyBorder(5, 25, 5, 5));
        panel1.setLayout(new GridLayout(0,3));
        panel2.setLayout(new GridLayout(0,3));
        panel3.setLayout(new GridLayout(0,3));
        this.setPreferredSize(new Dimension(1500, 1000));
        for (int i = 0; i < N/3; i++) {
            panel1.add(new Seat(i, 3/2));
        }
        for (int i = 0; i < N/3; i++) {
            panel2.add(new Seat(i, 3/2));
        }
        for (int i = 0; i < N/3; i++) {
            panel3.add(new Seat(i, 3/2));
        }
        unbuckledLabel = new JLabel("<html> Unbuckled Passengers: <br>" + "</html>");
        leftSeatLabel = new JLabel("<html> Passengers Not on Seat: <br>" + "</html>");
        callingLabel = new JLabel("<html> Passengers Calling FA: <br>" + "</html>");
        panel1.add(new JLabel(""));
        panel1.add(unbuckledLabel);
        panel2.add(new JLabel(""));
        panel2.add(leftSeatLabel);
        panel3.add(new JLabel(""));
        panel3.add(callingLabel);
        this.add(panel1);
        this.add(panel2);
        this.add(panel3);
    }


    public void display(String Title) {
        frame = new JFrame(Title);
        frame.setDefaultCloseOperation(JFrame.HIDE_ON_CLOSE);
        frame.add(this);
        frame.pack();
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
    }

    public static class Seat extends JButton {
        public Seat(int i, int N) {
            super(i / N + "," + i % N);
            this.setLayout(new BorderLayout());
            this.setOpaque(true);
            this.setBorderPainted(true);
            this.setHorizontalTextPosition(JButton.LEFT);
        }
    }
}



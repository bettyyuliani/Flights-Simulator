import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.IOException;

public class PlaneLocationBoard extends JFrame{
    public JPanel panel;

    public PlaneLocationBoard(int N, int rows, int cols) {
        this.setPreferredSize(new Dimension(845, 900));
        JPanel panel = new JPanel();
        panel.setLayout(new GridLayout(rows,cols));

        for (int i = 0; i < N; i++)
        {
            JLabel label = new MapLabel();
            label.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));;
            panel.add(label);
        }
        this.panel = panel;
        this.add(panel);
        this.setResizable(false);
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        this.setVisible(true);
        this.pack();
    }

    public static ImageIcon loadIcon(String iconName) throws IOException {
        ClassLoader loader = PlaneLocationBoard.class.getClassLoader();
        BufferedImage icon = ImageIO.read(loader.getResourceAsStream(iconName));
        return new ImageIcon(icon);
    }

    public static class MapLabel extends JLabel {
        public MapLabel() {
            ImageIcon imageIcon = null;
            try {
                imageIcon = new ImageIcon(PlaneLocationBoard.loadIcon("loading.jpg").getImage().getScaledInstance(480, 430, Image.SCALE_SMOOTH));
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
            this.setIcon(imageIcon);
            this.setText("Information Loading..");
            this.setHorizontalTextPosition(JLabel.CENTER);
            this.setVerticalTextPosition(JLabel.TOP);
        }
    }
}

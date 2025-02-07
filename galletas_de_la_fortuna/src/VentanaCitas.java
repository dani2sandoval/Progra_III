import javax.swing.*;
import java.awt.event.*;

public class VentanaCitas extends JFrame {
    private ProveedorDeMensajes provider;
    private JLabel etiquetaMensaje;
    private JButton botonGenerar;

    public VentanaCitas(ProveedorDeMensajes provider) {
        this.provider = provider;

        setTitle("Citas de la Suerte");
        setSize(500, 200);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLayout(null);


        etiquetaMensaje = new JLabel("Presiona el botón para un mensaje");
        etiquetaMensaje.setBounds(20, 20, 400, 25);
        add(etiquetaMensaje);

        botonGenerar = new JButton("Generar");
        botonGenerar.setBounds(250, 70, 200, 30);
        add(botonGenerar);


        botonGenerar.addActionListener(new ActionListener() {

            public void actionPerformed(ActionEvent e) {
                String mensaje = provider.obtenerMensajeAleatorio();
                etiquetaMensaje.setText(mensaje);
            }
        });

        setVisible(true);
    }
}

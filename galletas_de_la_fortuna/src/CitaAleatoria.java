import java.util.Random;

public class CitaAleatoria implements ProveedorDeMensajes {

    private String[] mensajes = {
            "El éxito comienza con un solo paso.",
            "Cada día es una nueva oportunidad para ser mejor.",
            "La felicidad se encuentra en las pequeñas cosas.",
            "Atrévete a soñar en grande y hazlo realidad.",
            "Hoy es el día perfecto para empezar de nuevo.",
            "Tu actitud determina tu altitud.",
            "Las grandes oportunidades llegan cuando menos las esperas.",
            "Eres más fuerte de lo que imaginas.",
            "La vida premia a los persistentes.",
            "Confía en tus instintos, ¡funcionan!"
    };


    public String obtenerMensajeAleatorio() {
        Random random = new Random();
        return mensajes[random.nextInt(mensajes.length)];
    }
}

public class Main {
    public static void main(String[] args) {
        ProveedorDeMensajes provider = new CitaAleatoria();
        new VentanaCitas(provider);
    }
}


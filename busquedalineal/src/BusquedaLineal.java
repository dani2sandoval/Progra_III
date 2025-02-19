public class BusquedaLineal {

    public static int busquedaLineal(int[] arreglo, int objetivo) {
        int n = arreglo.length;
        for (int i = 0; i < n; i++) {
            if (arreglo[i] == objetivo) {
                return i;
            }
        }
        return -1;
    }

}
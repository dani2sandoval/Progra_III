import java.util.Scanner;

public class metododebusqueda {

    public static void main(String[] args) {

        Scanner escaner = new Scanner(System.in);

        System.out.print("Ingrese el tamano del arreglo: ");
        int tamano = escaner.nextInt();

        int[] arreglo = new int[tamano];
        System.out.println("Ingrese los elementos del arreglo:");
        for (int i = 0; i < tamano; i++) {
            arreglo[i] = escaner.nextInt();
        }

        System.out.print("Ingrese el numero que quiere buscar: ");
        int objetivo = escaner.nextInt();

        int resultado = BusquedaLineal.busquedaLineal(arreglo, objetivo);

        if (resultado == -1) {
            System.out.println("El elemento no se encuentra en el arreglo");
        } else {
            System.out.println("Elemento encontrado en el indice " + resultado);
        }

        escaner.close();
    }

}

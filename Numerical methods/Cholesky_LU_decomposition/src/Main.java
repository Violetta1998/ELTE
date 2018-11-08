import java.util.Arrays;

import java.util.Random;

public class Main {

    public static void main(String[] args) {
        Random rnd = new Random();
        int size = 300;
        double[][] test = new double[size][size];
        double[][] test2 = {{-5, 0, 2}, {1, -2, 3}, {6, -2, 1}}; //non-invertible matrix

        //input 300*300 matrix with random numbers
        for (int i = 0; i < size; i++) {
            for (int k = 0; k < size; k++) {
                test[i][k] = rnd.nextDouble();
            }
        }

        try {
            long start = System.currentTimeMillis();
            double[][] res = Matrix.decomposition(test);
            long finish = System.currentTimeMillis();
            long timeConsumedMillis = finish - start;
            System.out.println("Time for inverse matrix in msc: " + timeConsumedMillis);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        int size2 = 500;
        double[][] test3 = Matrix.createSymmetricMatrix(size2);

        try {
            long start = System.currentTimeMillis();
            double[][] res = Matrix.choleskyDecomposition(test3);
            long finish = System.currentTimeMillis();
            long timeConsumedMillis = finish - start;
            System.out.println("Time for checking positive definite matrix in msc: " + timeConsumedMillis);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
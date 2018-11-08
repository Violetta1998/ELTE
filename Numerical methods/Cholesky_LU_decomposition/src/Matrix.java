import java.util.Random;

public class Matrix {
    //method for getting lower triangular matrix
    public static double[][] inverseLowerTriangularMatrix(double[][] L) {
        int i, j, k;
        int n = L.length;
        double sum;
        for (j = 0; j < n; j++) {
            L[j][j] = 1.0 / L[j][j];
            for (i = j + 1; i < n; i++) {
                sum = 0.0;
                for (k = j; k < i; k++) {
                    sum -= L[i][k] * L[k][j];
                }
                L[i][j] = sum / L[i][i];
            }
        }
        return L;
    }

    public static double[][] inverseUpperTriangularMatrix(double[][] L) {
        int i, j, k;
        int n = L.length;
        double sum;
        for (j = n - 1; j > -1; j--) {
            L[j][j] = 1.0 / L[j][j];
            for (i = j - 1; i > -1; i--) {
                sum = 0.0;
                for (k = j; k > i; k--) {
                    sum -= L[i][k] * L[k][j];
                }
                L[i][j] = sum / L[i][i];
            }
        }
        return L;
    }

    public static double[][] multiply(double[][] A, double[][] B) {
        int aRows = A.length;
        int aColumns = A[0].length;
        int bRows = B.length;
        int bColumns = B[0].length;

        if (aColumns != bRows) {
            throw new IllegalArgumentException("A:Rows: " + aColumns + " did not match B:Columns " + bRows + ".");
        }

        double[][] C = new double[aRows][bColumns];
        for (int i = 0; i < aRows; i++) {
            for (int j = 0; j < bColumns; j++) {
                C[i][j] = 0.00000;
            }
        }
        for (int i = 0; i < aRows; i++) { // aRow
            for (int j = 0; j < bColumns; j++) { // bColumn
                for (int k = 0; k < aColumns; k++) { // aColumn
                    C[i][j] += A[i][k] * B[k][j];
                }
            }
        }

        return C;
    }

    public static double[][] transposeMatrix(double[][] matrix) {
        double[][] newMatrix = new double[matrix.length][matrix[0].length];
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[0].length; j++) {
                newMatrix[j][i] = matrix[i][j];
            }
        }
        return newMatrix;
    }

    public static double[][] choleskyDecomposition(double[][] a) throws Exception {
        int m = a.length;
        double[][] l = new double[m][m];
        for (int i = 0; i < m; i++) {
            for (int k = 0; k < (i + 1); k++) {
                double sum = 0;
                for (int j = 0; j < k; j++) {
                    sum += l[i][j] * l[k][j];
                }
                if ((a[i][i] - sum) < 0) {
                    throw new Exception("The Cholesky decomposition cannot be done! Matrix is non positive definite!");
                }

                l[i][k] = (i == k) ? Math.sqrt(a[i][i] - sum) :
                        (1.0 / l[k][k] * (a[i][k] - sum));
            }
        }
        return l;
    }

    public static double[][] decomposition(double[][] a) throws Exception {
        int i, j, k;
        double[][] l = new double[a.length][a[0].length];
        double[][] u = new double[a.length][a[0].length];
        int MaxOrder = a.length;

        for (i = 0; i < MaxOrder; i++)
            l[i][i] = 1.0;

        for (j = 0; j < a.length; j++) {
            for (i = 0; i < a[0].length; i++) {
                if (i >= j) {
                    u[j][i] = a[j][i];
                    for (k = 0; k < j; k++)
                        u[j][i] = u[j][i] - u[k][i] * l[j][k];

                    if (i == j && ((double) Math.round(u[i][j] * 100000d) / 100000d) == 0) {//close to 0
                        throw new Exception("Matrix is non invertible!");
                    }
                }
                if (i > j) {
                    l[i][j] = a[i][j];
                    for (k = 0; k < j; k++)
                        l[i][j] = l[i][j] - u[k][j] * l[i][k];
                    l[i][j] = l[i][j] / u[j][j];
                }
            }
        }
        return multiply(inverseUpperTriangularMatrix(u), inverseLowerTriangularMatrix(l));
    }

    public static double[][] createSymmetricMatrix(int size) {
        double[][] m = new double[size][size];
        double[][] newM = new double[size][size];
        double value = 0.0;
        Random rnd = new Random();
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                m[i][j] = rnd.nextDouble();
            }
        }
        newM = multiply(m, transposeMatrix(m));
        return newM;
    }
}

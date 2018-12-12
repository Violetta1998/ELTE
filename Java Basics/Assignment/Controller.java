import java.util.ArrayList;
import java.util.Scanner;
import java.util.Date;
import java.util.Random;
import java.io.FileWriter;

import models.GoldenTicket;
import models.OompaLoompaSong;
import models.Product;
import models.Being;
import models.Kid;
import models.OompaLoompa;

public class Controller {

    static Scanner scanner = new Scanner(System.in);
    static String filePath = "products.txt";

    private static ArrayList<GoldenTicket> tickets = new ArrayList<>();
    private static ArrayList<OompaLoompa> beingsOompa = new ArrayList<>();
    private static ArrayList<Kid> beingsKids = new ArrayList<>();
    private static ArrayList<Product> products = new ArrayList<>();

    public Controller() {
    }

    public static void registerTickets() {
        System.out.println("Write the ticket code:");
        String code = scanner.next();
        System.out.println("Write the raffled date (format: yyyy/MM/dd):");
        String date = scanner.next();
        try {
            GoldenTicket newTicket = new GoldenTicket(code, date);
            tickets.add(newTicket);
            System.out.println("The Golden ticket is added successfully!");
        } catch (Exception ex) {
            System.out.println("Smth went wrong..." + ex.getMessage());
        }

    }

    public static void registerBeings() {
        System.out.println("Would you like to register a Kid or OompaLoompa? If a Kid - press 1, else - 2");
        int code = scanner.nextInt();
        if (code == 1) {
            System.out.println("Write Kid's code: ");
            int codeK = scanner.nextInt();

            System.out.println("Write his/her name: ");
            String name = scanner.next();
            System.out.println("Write the birthday date (format: yyyy/MM/dd): ");
            String bday = scanner.next();
            System.out.println("Write place of his/her birth: ");
            String place = scanner.next();

            try {
                beingsKids.add(new Kid(codeK, name, bday, place));
                System.out.println("The Kid is registered successfully!");
            } catch (Exception ex) {
                System.out.println("Smth went wrong..." + ex.getMessage());
            }

        }
        if (code == 2) {

            System.out.println("Write OompaLoompa's code: ");
            int codeO = scanner.nextInt();
            System.out.println("Write the height: ");
            int height = scanner.nextInt();

            System.out.println("Write OompaLoompa's name: ");
            String name = scanner.next();

            System.out.println("Write OompaLoompa's fabourite food: ");
            String food = scanner.next();

            try {
                beingsOompa.add(new OompaLoompa(codeO, name, height, food));
                System.out.println("The OompaLoompa is registered successfully!");
            } catch (Exception ex) {
                System.out.println("Smth went wrong..." + ex.getMessage());
            }

        }
    }

    public static void registerProducts() {
        System.out.println("Write Product's description: ");
        String description = scanner.nextLine();
        System.out.println("Write it's barcode: ");
        long barcode = scanner.nextInt();
        System.out.println("Write the serial number: ");
        String snum = scanner.next();

        System.out.println("If the product has golden ticket press  - 1, else - 2: ");
        int code = scanner.nextInt();
        if (code == 1) {
            System.out.println("Write the ticket's code: ");
            String ticketCode = scanner.next();
            int ticketIndex = 0;
            for (int i = 0; i < tickets.size(); i++) {
                if (tickets.get(i).getCode().equals(ticketCode)) {
                    ticketIndex = i;
                }
            }
            try {
                Product product = new Product(description, barcode, snum, tickets.get(ticketIndex));
                products.add(product);
                product.writeToFile(filePath);
                System.out.println("The Product is registered successfully!");
            } catch (Exception ex) {
                System.out.println("Smth went wrong..." + ex.getMessage());
            }
        }

        if (code == 2) {
            Product product = new Product(description, barcode, snum, null);

            try {
                products.add(product);
                product.writeToFile(filePath);
                System.out.println("The Product is registered successfully!");
            } catch (Exception ex) {
                System.out.println("Smth went wrong..." + ex.getMessage());
            }
        }
    }

    public static void registerSale() {
        System.out.println("Print the creature's code: ");
        int code = scanner.nextInt();
        System.out.println("Print the product's barcode: ");
        long barcode = scanner.nextInt();

        ArrayList<Product> randomProducts = new ArrayList<>();
        //randomly take one product from that barcode
        for (int i = 0; i < products.size(); i++) {
            if (products.get(i).getBarcode() == barcode) {
                randomProducts.add(products.get(i));
            }
        }

        Random rnd = new Random();
        int index = rnd.nextInt(randomProducts.size());
        Product product = randomProducts.get(index);

        // adds it to the kids products list
        for (int i = 0; i < beingsKids.size(); i++) {
            if (beingsKids.get(i).getCode() == code) {
                beingsKids.get(i).addProducts(product);
            }
        }

        // removes it from the general list of products
        for (int i = 0; i < products.size(); i++) {
            if (products.get(i).getBarcode() == barcode) {
                products.remove(products.get(i));
            }
        }
    }

    public static void ruffleTicket() {
        System.out.println("How many GoldenTickets should be ruffled: ");
        int number = scanner.nextInt();

        if (number >= tickets.size()) {
            for (int s = 0; s <= (number - tickets.size()); s++) {
                tickets.add(new GoldenTicket());
            }
        }

        Random rnd = new Random();
        int indexP = 0;
        int indexT = 0;
        if (products.size() > 0) {
            for (int i = 0; i < number; i++) {
                indexP = rnd.nextInt(products.size());
                indexT = rnd.nextInt(tickets.size() - 1);
                GoldenTicket ticket = tickets.get(indexT);
                products.get(indexP).setTicket(ticket);
            }
            listProducts();
        } else {
            System.out.println("There are no products for tickets, create them!");
        }
    }

    public static void listBeings() {
        System.out.println("Kids: ");
        for (int i = 0; i < beingsKids.size(); i++) {
            System.out.println(beingsKids.get(i).getClass() + "\n " + beingsKids.get(i));
        }

        System.out.println("OompaLoompas: ");
        for (int i = 0; i < beingsOompa.size(); i++) {
            System.out.println(beingsOompa.get(i).getClass() + "\n " + beingsOompa.get(i));
        }
        System.out.println();
    }

    public static void listProducts() {
        for (int i = 0; i < products.size(); i++) {
            System.out.println(products.get(i));
        }
        System.out.println();
    }

    public static void listTickets() {
        for (int i = 0; i < tickets.size(); i++) {
            System.out.println(tickets.get(i));
        }
        System.out.println();
    }

    public static void listRaffledTickets() {
        for (int i = 0; i < tickets.size(); i++) {
            if (tickets.get(i).isRaffled(products)) {
                System.out.println(tickets.get(i));
            }
        }
        System.out.println();
    }

    public static void listWinners() {
        for (int i = 0; i < beingsKids.size(); i++) {
            for (int j = 0; j < beingsKids.get(i).getProducts().size(); j++) {
                if (beingsKids.get(i).getProducts().size() != 0) {
                    System.out.println(beingsKids.get(i));
                }
            }
        }
    }

    public static void createSong() {
        System.out.println("Write the number of the lines:");
        int numberOfLines = scanner.nextInt();
        OompaLoompaSong song = new OompaLoompaSong(numberOfLines);
        song.singRandomly();
    }

    public static void writeToFile(String fileName, ArrayList array) {
        try {
            FileWriter fileWriter = new FileWriter(fileName, true);
            for (int i = 0; i < array.size(); i++) {
                fileWriter.write(array.get(i) + ", ");
                fileWriter.write("\r\n");
            }

            fileWriter.close();
        } catch (Exception ex) {
            System.out.println("smth went wrong...");
        }

    }

    public static void writeEverythingIntoFile() {
        writeToFile("tickets.txt", tickets);
        writeToFile("oompa.txt", beingsOompa);
        writeToFile("kids.txt", beingsKids);
    }
}
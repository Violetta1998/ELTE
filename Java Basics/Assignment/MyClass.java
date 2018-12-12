
import java.util.ArrayList;
import java.util.Scanner;

public class MyClass{

	public static void main(String[] args){
	    Scanner scanner = new Scanner(System.in);
		int option = 0;
		Controller ctrl = new Controller();
		do{
			
			System.out.println("Select from the list: "
				+"\n 1- Register golden ticket \n 2- List all prize tickets \n 3- List raffled tickets"
				+"\n 4- Create a new OompaLoompa Song \n 5 - Register Beings \n 6 - Register products" 
				+ "\n 7 - Ruffle ticket \n 8 - Register sale \n 9 - List winners \n 0- exit" );
			
			option = scanner.nextInt();
			
			switch(option){
				case 1:
					ctrl.registerTickets();
					break;
				case 2:
					ctrl.listTickets();
					break;
				case 3:
					ctrl.listRaffledTickets();
					break;
				case 4:
					ctrl.createSong();
					break;
				case 5:
					ctrl.registerBeings();
					break;
				case 6:
				    ctrl.registerProducts();
					break;
				case 7:
				    ctrl.ruffleTicket();
				    break;
				case 8:
				    ctrl.registerSale();
				    break;
				case 9:
				   ctrl.listWinners();
				   break;
				case 0:	
				   ctrl.writeEverythingIntoFile();			
				   break;
				default:
					System.out.println("wrong option");
			}
		}while(option!=0);

	}

}
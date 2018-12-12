package models;
import java.io.FileWriter;

public class Product{
	private String description;
	private long barcode;
	private String serialNumber;
	private GoldenTicket prizeTicket;

	public Product(String description, long barcode, String serialNumber,  GoldenTicket prizeTicket){
		this.description = description;
		this.barcode = barcode;
		this.serialNumber = serialNumber;
		this.prizeTicket = prizeTicket;//null if no golden ticket
	}

    //write the registered products to file
	public void writeToFile(String fileName){
		try{
			FileWriter fileWriter = new FileWriter(fileName, true);
			fileWriter.write("Description: " + description + ", Barcode: " 
				+ barcode + ", Serial Number: " + serialNumber + ", prize ticekt: " + prizeTicket);
            fileWriter.write("\r\n");
            fileWriter.close();
		}
		catch(Exception ex){
			System.out.println("smth went wrong");
		}
	}

	public long getBarcode(){
		return barcode;
	}


	public GoldenTicket getTicket(){
		return prizeTicket;
	}

	public void setTicket(GoldenTicket ticket){
	   this.prizeTicket = ticket;
	}

	@Override
	public String toString(){
		return description + ", barcode: " + barcode + ", serial number: " 
		+ serialNumber + ", prize ticket num.: " + prizeTicket.getCode();
	}

}
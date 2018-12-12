package models;
import java.util.Date;
import java.time.LocalDate;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class GoldenTicket{
	private String code;
	private Date registered;
	private SimpleDateFormat myFormat = new SimpleDateFormat("yyyy/MM/dd");
	
	//Constructors
	public GoldenTicket(){
		this.registered = new Date();
	}

	public GoldenTicket(String code, Date registered){
		this.code = code;
		this.registered = registered;
	}

	public GoldenTicket(String code, String registered) throws Exception{
		this.code = code;
		this.registered = myFormat.parse(registered);
	}

	//Getters
	public String getCode(){
		return code;
	}

	public Date getRaffled(){
		return registered;
	}

	//Setters
	public void setCode(String code){
		this.code = code;
	}

	public void setRaffled(Date registered){
		this.registered = registered;
	}

	@Override
	public String toString(){
		return "Ticket's code: " + code + ", Date registered: " + myFormat.format(registered);
	}


	public boolean isRaffled(ArrayList<Product> products){
		for(int i = 0;i<products.size(); i++){
			if(products.get(i).getTicket().getCode().equals(code)){
				System.out.println(products.get(i).getTicket().getCode());
				System.out.println(code);
				return true;
			}
		}
		return false;
	}

}
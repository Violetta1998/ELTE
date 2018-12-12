package models;
import java.util.Date;
import java.util.ArrayList;
import java.text.SimpleDateFormat;

public class Kid extends Being{
	//code, birthday, name, list of purchased products, place of birth
	private Date birthday;
	private ArrayList<Product> products;
	private String placeOfBirth;
	private SimpleDateFormat myFormat = new SimpleDateFormat("yyyy/MM/dd");

	public Kid(int code, String name, Date birthday, String placeOfBirth){
		super(code, name);
		this.birthday = birthday;
		this.placeOfBirth = placeOfBirth;
		products = new ArrayList<>();
	}

	public Kid(int code, String name, String birthday, String placeOfBirt) throws Exception{
		super(code, name);
		this.birthday = myFormat.parse(birthday);
		this.placeOfBirth = placeOfBirth;
		products = new ArrayList<>();
	}

	public void addProducts(Product product){
		products.add(product);
		System.out.println("The product is added successfully!");
	}

	public ArrayList<Product> getProducts(){
		return products;
	}

	@Override
	public String toString(){
		return super.toString() + "\n birthday and it's place: " + birthday + " " + placeOfBirth;
	}
}
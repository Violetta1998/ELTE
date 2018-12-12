package models;

public class OompaLoompa extends Being{
	//code, name, height, favorite food
	private int height;
	private String favouriteFood;

	public OompaLoompa(int code, String name, int height, String favouriteFood){
		super(code, name);
		this.height = height;
		this.favouriteFood = favouriteFood;
	}

	@Override
	public String toString(){
		return super.toString() + "\n height: " + height + "\n food: "+ favouriteFood;
	}
}
package models;
import java.io.FileWriter;

public abstract class Being{
	private int code;
	private String name;

	public Being(int code, String name){
		this.code = code;
		this.name = name;
	}

	public int getCode(){
		return code;
	}

	@Override
	public String toString(){
		return "Code: " + code + "\n  Name: " + name;
	}
}
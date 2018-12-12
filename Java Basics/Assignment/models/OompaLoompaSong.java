package models;
import java.util.Random;
import java.util.ArrayList;
import java.util.Scanner;
import java.io.File;

public class OompaLoompaSong{
	private int lines;
	private ArrayList<String> songLines;
	private String filePath = "OompaLoompaSong.txt";

	public OompaLoompaSong(int lines){
		this.lines = lines;
		songLines = new ArrayList<>();
	};  
	
	//Getter and Setter for filePath
	public String getFilePath(){
		return filePath;
	}   
	public void setFilePath(String path){
		this.filePath = path;
	}

	// Return a String with the song
	public void singRandomly(){
		int index = 0;
		readFile();
		for(int i = 0; i < lines; i++){
			Random rnd = new Random();
		    index = rnd.nextInt(songLines.size()-1);
			System.out.println(songLines.get(index));
		}
		System.out.println();
	}

	public void singSong(){
		for(int i = 0; i<songLines.size(); i++){
			System.out.println(songLines.get(i));
		}
		System.out.println();
	}

	public void readFile(){
		try {
			int index = 0;
			Scanner scanner = new Scanner( new File(filePath) );
			while ( scanner.hasNextLine() )  {
				songLines.add(scanner.nextLine());
				index++;
			}
		} catch (Exception e) {
			e.getMessage();
		} 
	}	

}
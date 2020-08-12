import java.util.Scanner;

public class Main {
    
    public static void main(String[] args) {
        //String path = "/media/hassan/new part/code-star/EnglishData";  // linux path
        String path= "D:\\Downloads\\SampleEnglishData\\EnglishData"; // Windows path
        App app = new App(new TerminalCommandReader(), new InvertedIndex());
        app.readFileToInvertedIndex(path);
        app.runCommandLine();
    }
}
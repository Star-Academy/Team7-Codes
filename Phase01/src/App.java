import java.util.HashSet;
import java.util.Scanner;
import java.util.function.Consumer;

public class App {
    public static void main(String[] args) throws Exception {
        InvertedIndex invertedIndex = new InvertedIndex();
        //FileReader fileReader = new FileReader(invertedIndex, "/media/hassan/new part/code-star/EnglishData"); //Linux path
        FileReader fileReader = new FileReader(invertedIndex, "D:\\Downloads\\SampleEnglishData\\EnglishData"); // Windows path
        fileReader.readAllFiles();
        Scanner scanner = new Scanner(System.in);
        System.out.print("Search : ");
        String words = scanner.nextLine();
        try {
            HashSet<String> result = invertedIndex.advanceFind(words.toLowerCase());
            printOutput(result, words);
        } catch(Exception e) {
            System.out.println(e.getMessage());
        }
        scanner.close();
    }

    public static void printOutput(HashSet<String> result, String words){
        System.out.println(result.size() + " documents with \"" + words + "\" : ");
        result.forEach(new Consumer<String>(){
            @Override
            public void accept(String t) {
                System.out.println("\t" + t);
            }
        });
    }
    
}

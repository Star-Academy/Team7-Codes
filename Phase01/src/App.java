import java.util.HashSet;
import java.util.Scanner;
import java.util.function.Consumer;

public class App {

    public static void runCommandLine() {
        InvertedIndex invertedIndex = new InvertedIndex();
        FileReader fileReader = new FileReader(invertedIndex, "/media/hassan/new part/code-star/EnglishData"); //Linux path
        // FileReader fileReader = new FileReader(invertedIndex, "D:\\Downloads\\SampleEnglishData\\EnglishData"); // Windows path
        fileReader.readAllFiles();
        Scanner scanner = new Scanner(System.in);
        System.out.print("Search : ");
        String words = scanner.nextLine();
        words = Tokenizer.normalize(words);
        try {
            HashSet<String> result = invertedIndex.advanceFind(words);
            printOutput(result, words);
        } catch(Exception e) {
            System.out.println(e.getMessage());
        }
        scanner.close();
    }

    private static void printOutput(HashSet<String> result, String words){
        System.out.println(result.size() + " documents with \"" + words + "\" : ");
        result.forEach(res->System.out.println("\t" + res)); 
    }  
}

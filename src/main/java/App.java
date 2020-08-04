import java.util.HashSet;
import java.util.Scanner;

public class App {

    public static void runCommandLine() {
        String path = "/media/hassan/new part/code-star/EnglishData";  // linux path
        //String path = "D:\\Downloads\\SampleEnglishData\\EnglishData"; // Windows path
        InvertedIndex invertedIndex = new InvertedIndex();
        FileReader fileReader = new FileReader(invertedIndex);
        fileReader.readAllFiles(path);
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
import java.io.File;
import java.util.HashSet;
import java.util.Scanner;

public class App {

    private final String path;
    private final InvertedIndex invertedIndex;
    private final FileReader fileReader;

    public App() {
        //path = "/media/hassan/new part/code-star/EnglishData";  // linux path
        path = "D:\\Downloads\\SampleEnglishData\\EnglishData"; // Windows path
        invertedIndex = new InvertedIndex();
        fileReader = new FileReader(invertedIndex);
        fileReader.readAllFiles(path);
    }

    public void runCommandLine() {
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

    private void printOutput(HashSet<String> result, String words){
        System.out.println(result.size() + " documents with \"" + words + "\" : ");
        result.forEach(res->System.out.println("\t" + res)); 
    }  
}

import java.util.HashSet;
import java.util.Scanner;
import java.util.function.Consumer;

public class App {
    public static void main(String[] args) throws Exception {
        InvertedIndex invertedIndex = new InvertedIndex();
        FileReader fileReader = new FileReader(invertedIndex);
        fileReader.readAllFiles();
        Scanner scanner = new Scanner(System.in);
        System.out.print("Search for a single word : ");
        String word = scanner.nextLine();
        try {
            HashSet<String> result = invertedIndex.findSingleWord(word);
            System.out.println(result.size() + " documents contain \"" + word + "\" : ");
            result.forEach(new Consumer<String>(){
                @Override
                public void accept(String t) {
                    System.out.println("\t" + t);
                }
            });
        } catch (Exception e) {
            System.out.println(e.getMessage());
        } 
        scanner.close();
    }
}

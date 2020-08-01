import java.util.HashSet;
import java.util.Scanner;
import java.util.function.Consumer;

public class App {
    public static void main(String[] args) throws Exception {
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex();
        Scanner scanner = new Scanner(System.in);
        System.out.println("Search for a single word : ");
        String word = scanner.nextLine();
        try {
            HashSet<String> result = invertedIndex.findSingleWord(word);
            result.forEach(new Consumer<String>(){
                @Override
                public void accept(String t) {
                    System.out.println(t);
                }
            });
        } catch (Exception e) {
            System.out.println(e.getMessage());
        } 
        scanner.close();
    }
}

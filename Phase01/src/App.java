import java.util.HashSet;
import java.util.Scanner;
import java.util.function.Consumer;

public class App {
    public static void main(String[] args) throws Exception {
        InvertedIndex invertedIndex = new InvertedIndex();
        FileReader fileReader = new FileReader(invertedIndex, "D:\\Downloads\\SampleEnglishData\\EnglishData");
        fileReader.readAllFiles();
        Scanner scanner = new Scanner(System.in);
        System.out.print("Search : ");
        String words = scanner.nextLine();


        // for(String a : withPluses){
        //     System.out.println(a);
        // }
        // System.out.println("%%%");
        // for(String a : regulares){
        //     System.out.println(a);
        // }
        // System.out.println("%%%");
        // for(String a : withMinuses){
        //     System.out.println(a);
        // }
        // System.out.println("%%%");


        try {
            HashSet<String> result = invertedIndex.advanceFind(words);
            System.out.println(result.size() + " documents with \"" + words + "\" : ");
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

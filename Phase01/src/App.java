import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;
import java.util.function.Consumer;

public class App {
    public static void main(String[] args) throws Exception {
        InvertedIndex invertedIndex = new InvertedIndex();
        FileReader fileReader = new FileReader(invertedIndex);
        fileReader.readAllFiles();
        Scanner scanner = new Scanner(System.in);
        System.out.print("Search for a single word : ");
        String word = scanner.nextLine();
        String[] subs = word.split(" ");
        HashSet<String> withPluses = new HashSet<String>();
        HashSet<String> regulares =new HashSet<String>();
        HashSet<String> withMinuses = new HashSet<String>();
        for (int i=0 ; i < subs.length ; i++){
            switch(subs[i].charAt(0)){
               case '+':
                    withPluses.add(subs[i].substring(1));
                    break;
                case '-':
                    withMinuses.add(subs[i].substring(1));
                    break;
                default: 
                regulares.add(subs[i]);
            }
        }


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
            HashSet<String> result = invertedIndex.advanceFind(regulares, withPluses, withMinuses);
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

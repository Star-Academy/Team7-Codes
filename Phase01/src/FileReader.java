import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;



public class FileReader {
    InvertedIndex invertedIndex;
    public FileReader(InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
    }

    public void readAllFiles(){
        File path = new File("/media/hassan/new part/code-star/EnglishData");
        File[] documents = path.listFiles();
        for(File document : documents)
            readFile(document);
    }

    public void readFile(File document) {
        try {
            Scanner input = new Scanner(document);
            // System.out.println(document.getName());
            while (input.hasNext()) {
                String line = input.next();
                invertedIndex.add(line, document.getName());
                // System.out.println(line);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

}

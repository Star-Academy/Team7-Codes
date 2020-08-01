import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;



public class FileReader {
    InvertedIndex invertedIndex = new InvertedIndex();
    public FileReader() {

    }

    public void readAllFiles(){
        File path = new File("/home/hassan/code-star/Team7-Codes/Phase01/src/documents/");
        File[] documents = path.listFiles();
        readFile(documents[0]);
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
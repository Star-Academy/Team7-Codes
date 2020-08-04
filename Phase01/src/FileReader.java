import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class FileReader {

    private InvertedIndex invertedIndex;

    public FileReader(InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
    }

    public void readAllFiles(String path){
        File file = new File(path);
        File[] documents = file.listFiles();
        for(File document : documents)
            readFile(document);
    }

    private void readFile(File document) {
        try {
            Scanner input = new Scanner(document);
            while (input.hasNext()) {
                String line = input.next();
                invertedIndex.add(line.toLowerCase(), document.getName());
            }
            input.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
}

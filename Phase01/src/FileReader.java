import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class FileReader {

    InvertedIndex invertedIndex;
    File file;

    public FileReader(InvertedIndex invertedIndex, String path) {
        this.invertedIndex = invertedIndex;
        file = new File(path);
    }

    public void readAllFiles(){
        File[] documents = file.listFiles();
        for(File document : documents)
            readFile(document);
    }

    public void readFile(File document) {
        try {
            Scanner input = new Scanner(document);
            while (input.hasNext()) {
                String word = input.next();
                word = Tokenizer.normalize(word);
                invertedIndex.add(word, document.getName());
            }
            input.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
}

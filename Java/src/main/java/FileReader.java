import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class FileReader {

    private final InvertedIndex invertedIndex;

    public FileReader(InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
    }

    public void readAllFiles(String path){
        File file = new File(path);
        File[] documents = file.listFiles();
        assert documents != null;
        for(File document : documents)
            readFile(document);
    }

    private void readFile(File document) {
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

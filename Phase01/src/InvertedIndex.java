import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

public class InvertedIndex {

    HashMap<String, HashSet<String>> dictionary;

    public InvertedIndex() {
        dictionary = new HashMap<>();
    }

    public void add(String word, String documentName) {
        if (dictionary.containsKey(word)) {
            dictionary.get(word).add(documentName);
            return;
        }
        HashSet<String> documentsName = new HashSet<>();
        documentsName.add(documentName);
        dictionary.put(word, documentsName);
    }

    public HashSet<String> findSingleWord(String word) throws Exception {
        if (dictionary.containsKey(word)) {
            return dictionary.get(word);
        }
        throw new Exception("No items match your search");
    }
}
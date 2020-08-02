import java.util.HashMap;
import java.util.HashSet;
import java.util.function.Consumer;

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

    public HashSet<String> advanceFind(HashSet<String> mustIncludeWords, HashSet<String> includeWords,
            HashSet<String> excludeWords) throws Exception{
        HashSet<String> result = new HashSet<>();
        mustIncludeWords.forEach(new Consumer<String>() {
            @Override
            public void accept(String t) {
                try {
                    HashSet<String> resultForSingleWord = findSingleWord(t);
                    if (result.size() == 0) {
                        result.addAll(resultForSingleWord);
                    } else {
                        result.retainAll(resultForSingleWord);
                    }
                } catch (Exception e) {
                    // Just no result for this word ... nothing important
                }
            }
        });
        includeWords.forEach(new Consumer<String>() {
            @Override
            public void accept(String t) {
                try {
                    HashSet<String> resultForSingleWord = findSingleWord(t);
                    result.addAll(resultForSingleWord);
                } catch (Exception e) {
                    // Just no result for this word ... nothing important
                }
            }
        });
        excludeWords.forEach(new Consumer<String>(){
            @Override
            public void accept(String t) {
                try {
                    HashSet<String> resultForSingleWord = findSingleWord(t);
                    result.removeAll(resultForSingleWord);
                } catch (Exception e) {
                    // Just no result for this word ... nothing important
                }
            }           
        });
        if (result.size() == 0){
            throw new Exception("No items match your search");
        }
        return result;
    }
}
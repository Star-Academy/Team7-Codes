import java.lang.reflect.Method;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.function.Consumer;

public class InvertedIndex {

    HashMap<String, HashSet<String>> dictionary;



    public InvertedIndex() {
        dictionary = new HashMap<>();
    }

    public void add(final String word, final String documentName) {
        if (dictionary.containsKey(word)) {
            dictionary.get(word).add(documentName);
            return;
        }
        final HashSet<String> documentsName = new HashSet<>();
        documentsName.add(documentName);
        dictionary.put(word, documentsName);
    }

    public HashSet<String> findSingleWord(final String word) throws Exception {
        if (dictionary.containsKey(word)) {
            return dictionary.get(word);
        }
        throw new Exception("No items match your search");
    }

    public SearchQuery stringToSearchQuery(final String query) {
        return SearchQuery.parseString(query);
    }

    public HashSet<String> advanceFind(final String query) throws Exception {
        final HashSet<String> result = new HashSet<>();
        final SearchQuery searchQuery = stringToSearchQuery(query);
        String first = (String) searchQuery.getMustIncludeWords().toArray()[0];
        result.addAll(findSingleWord(first));
        processSet(result, searchQuery.getMustIncludeWords(), HashSet.class.getMethod("retainAll", Collection.class));
        processSet(result, searchQuery.getIncludeWords(), HashSet.class.getMethod("addAll", Collection.class));
        processSet(result, searchQuery.getExcludeWords(), HashSet.class.getMethod("removeAll", Collection.class));
        if (result.size() == 0) {
            throw new Exception("No items match your search");
        }
        return result;
    }

    private void processSet(final HashSet<String> result, final HashSet<String> set, Method method) {
        set.forEach(new Consumer<String>(){
            @Override
            public void accept(final String t) {
                try {
                    final HashSet<String> resultForSingleWord = findSingleWord(t);
                    method.invoke(result, resultForSingleWord);
                } catch (Exception e) {
                }
            }           
        });
    }

}
import java.util.HashSet;

public class SearchQuery {

    private HashSet<String> mustIncludeWords;
    private HashSet<String> includeWords;
    private HashSet<String> excludeWords;

    private SearchQuery() {
        mustIncludeWords = new HashSet<>();
        includeWords = new HashSet<>();
        excludeWords = new HashSet<>();
    }

    public HashSet<String> getMustIncludeWords() {
        return mustIncludeWords;
    }

    public HashSet<String> getIncludeWords() {
        return includeWords;
    }

    public HashSet<String> getExcludeWords() {
        return excludeWords;
    }

    public static SearchQuery parseString(String query) {
        SearchQuery searchQuery = new SearchQuery();
        String[] subs = query.split(" ");
        addSingleWord(searchQuery, subs);
        return searchQuery;
    }

    private static void addSingleWord(SearchQuery searchQuery, String[] subs){
        for (String word : subs) {
            switch(word.charAt(0)){
                case '+':
                     searchQuery.includeWords.add(word.substring(1));
                     break;
                 case '-':
                     searchQuery.excludeWords.add(word.substring(1));
                     break;
                 default: 
                     searchQuery.mustIncludeWords.add(word);
             }
        }
    }
}
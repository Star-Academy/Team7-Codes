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
        for (int i=0 ; i < subs.length ; i++){
            switch(subs[i].charAt(0)){
               case '+':
                    searchQuery.includeWords.add(subs[i].substring(1));
                    break;
                case '-':
                    searchQuery.excludeWords.add(subs[i].substring(1));
                    break;
                default: 
                    searchQuery.mustIncludeWords.add(subs[i]);
            }
        }
        return searchQuery;
    }
}
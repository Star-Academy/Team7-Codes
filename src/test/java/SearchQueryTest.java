import org.junit.Before;
import org.junit.Test;

import java.util.Arrays;
import java.util.HashSet;

import static org.junit.Assert.*;

public class SearchQueryTest {

    private SearchQuery searchQuery;

    @Before
    public void init() {
        searchQuery = SearchQuery.parseString("first -second +third anotherNormal +anotherPlus -anotherMinus");
    }

    @Test
    public void mustIncludeWordsTest() {
        HashSet<String> expectedMustInclude = new HashSet<>(Arrays.asList("first", "anotherNormal"));
        HashSet<String> actualMustInclude = searchQuery.getMustIncludeWords();
        assertEquals(expectedMustInclude, actualMustInclude);
    }

    @Test
    public void includeWordsTest() {
        HashSet<String> expectedInclude = new HashSet<>(Arrays.asList("third", "anotherPlus"));
        HashSet<String> actualInclude = searchQuery.getIncludeWords();
        assertEquals(expectedInclude, actualInclude);
    }

    @Test
    public void excludeWordsTest() {
        HashSet<String> expectedExclude = new HashSet<>(Arrays.asList("second", "anotherMinus"));
        HashSet<String> actualExclude = searchQuery.getExcludeWords();
        assertEquals(expectedExclude, actualExclude);
    }
}
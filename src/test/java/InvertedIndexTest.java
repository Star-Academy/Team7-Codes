import org.junit.Before;
import org.junit.Test;

import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;

import static org.junit.Assert.*;

public class InvertedIndexTest {


    InvertedIndex invertedIndex = new InvertedIndex();

    HashSet<String> hassan = new HashSet<>(Arrays.asList("1", "2", "3", "4"));
    HashSet<String> parsa = new HashSet<>(Collections.singletonList("1"));
    HashSet<String> jalal = new HashSet<>(Collections.singletonList("2"));
    HashSet<String> jalil = new HashSet<>(Collections.singletonList("3"));
    HashSet<String> soheil = new HashSet<>(Arrays.asList("3", "4"));
    HashSet<String> mahdi = new HashSet<>(Collections.singletonList("1"));

    @Before
    public void initAddTest() {
        invertedIndex.add("hassn", "1");
        invertedIndex.add("hassan", "2");
        invertedIndex.add("hassan", "3");
        invertedIndex.add("hassan", "4");

        invertedIndex.add("parsa", "1");

        invertedIndex.add("jalal", "2");

        invertedIndex.add("jalil", "3");

        invertedIndex.add("soheil", "3");
        invertedIndex.add("soheil", "3");
        invertedIndex.add("soheil", "4");

        invertedIndex.add("mahdi", "1");

    }

    @Test
    public void testAdvanceFind1() {
        HashSet<String> temp = new HashSet<>();
        temp.addAll(hassan);
        temp.addAll(parsa);
        try {
            assertEquals(temp, invertedIndex.advanceFind("hassan +parsa"));
        } catch (Exception ignored) {
            fail();
        }
    }

    @Test
    public void testAdvanceFind2() {
        HashSet<String>temp = new HashSet<>();
        temp.addAll(parsa);
        temp.addAll(jalal);
        temp.removeAll(jalil);
        try {
            assertEquals(temp, invertedIndex.advanceFind("+parsa +jalal -jalil"));
        } catch (Exception ignored) {
            fail();
        }
    }

    @Test
    public void testAdvanceFind3() {
        HashSet<String> temp = new HashSet<>(parsa);
        temp.removeAll(mahdi);
        try {
            assertEquals(temp, invertedIndex.advanceFind("+parsa -mahdi"));
            fail();
        } catch (Exception ignored) {
            assertTrue(true);
        }
    }


    @Test
    public void testAdvanceFind4() {
        HashSet<String> temp = new HashSet<>();
        try {
            assertEquals(temp, invertedIndex.advanceFind("hamid"));
            fail();
        } catch (Exception ignored) {
            assertTrue(true);
        }
    }

    @Test
    public void testAdvanceFind5() {
        HashSet<String> temp = new HashSet<>();
        try {
            assertEquals(temp, invertedIndex.advanceFind("-hamid"));
            fail();
        } catch (Exception ignored) {
            assertTrue(true);
        }
    }

    @Test
    public void testAdvanceFind6() {
        HashSet<String> temp = new HashSet<>(soheil);
        temp.removeAll(hassan);
        try {
            assertEquals(temp, invertedIndex.advanceFind("+soheil -hassan"));
            fail();
        } catch (Exception ignored) {
            assertTrue(true);
        }
    }

    @Test
    public void testAdvanceFind7() {
        HashSet<String> temp = new HashSet<>(soheil);
        try {
            assertEquals(temp, invertedIndex.advanceFind("soheil"));
        } catch (Exception ignored) {
            fail();
        }
    }

    @Test
    public void testAdvanceFind8() {
        HashSet<String> temp = new HashSet<>(soheil);
        temp.retainAll(hassan);
        temp.addAll(parsa);
        temp.removeAll(jalil);
        try {
            assertEquals(temp, invertedIndex.advanceFind("soheil hassan -jalil +parsa"));
        } catch (Exception ignored) {
            fail();
        }
    }

    @Test
    public void testAdvanceFind9() {
        HashSet<String> temp = new HashSet<>(soheil);
        temp.retainAll(hassan);
        temp.retainAll(parsa);
        temp.addAll(jalil);
        try {
            assertEquals(temp, invertedIndex.advanceFind("soheil hassan -jalil parsa"));
            fail();
        } catch (Exception ignored) {
            assertTrue(true);
        }
    }
}

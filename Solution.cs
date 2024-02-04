public class Solution {
    public string MinWindow(string s, string t) {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t)) {
            return "";
        }

        Dictionary<char, int> targetFreq = new Dictionary<char, int>();
        foreach (char c in t) {
            if (targetFreq.ContainsKey(c)) {
                targetFreq[c]++;
            } else {
                targetFreq[c] = 1;
            }
        }

        int left = 0, right = 0;
        int requiredChars = targetFreq.Count;
        int formedChars = 0;
        Dictionary<char, int> windowFreq = new Dictionary<char, int>();

        int minLen = int.MaxValue;
        int minLeft = 0, minRight = 0;

        while (right < s.Length) {
            char currentChar = s[right];

            if (targetFreq.ContainsKey(currentChar)) {
                if (windowFreq.ContainsKey(currentChar)) {
                    windowFreq[currentChar]++;
                } else {
                    windowFreq[currentChar] = 1;
                }

                if (windowFreq[currentChar] == targetFreq[currentChar]) {
                    formedChars++;
                }
            }

            while (formedChars == requiredChars && left <= right) {
                if (right - left + 1 < minLen) {
                    minLen = right - left + 1;
                    minLeft = left;
                    minRight = right + 1;
                }

                char leftChar = s[left];
                if (targetFreq.ContainsKey(leftChar)) {
                    windowFreq[leftChar]--;
                    if (windowFreq[leftChar] < targetFreq[leftChar]) {
                        formedChars--;
                    }
                }

                left++;
            }

            right++;
        }

        return minLen == int.MaxValue ? "" : s.Substring(minLeft, minLen);
    }
}

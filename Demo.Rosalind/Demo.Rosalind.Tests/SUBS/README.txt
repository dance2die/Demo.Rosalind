http://rosalind.info/problems/subs/


Finding a Motif in DNA solved by 9638
July 1, 2012, 8 p.m. by Rosalind Team Topics: String Algorithms
← →
Combing Through the Haystackclick to expand

Problem

Given two strings ss and tt, tt is a substring of ss if tt is contained as a contiguous collection of symbols in ss (as a result, tt must be no longer than ss).

The position of a symbol in a string is the total number of symbols found to its left, including itself (e.g., the positions of all occurrences of 'U' in "AUGCUUCAGAAAGGUCUUACG" are 2, 5, 6, 15, 17, and 18). The symbol at position ii of ss is denoted by s[i]s[i].

A substring of ss can be represented as s[j:k]s[j:k], where jj and kk represent the starting and ending positions of the substring in ss; for example, if ss = "AUGCUUCAGAAAGGUCUUACG", then s[2:5]s[2:5] = "UGCU".

The location of a substring s[j:k]s[j:k] is its beginning position jj; note that tt will have multiple locations in ss if it occurs more than once as a substring of ss (see the Sample below).

Given: Two DNA strings ss and tt (each of length at most 1 kbp).

Return: All locations of tt as a substring of ss.

Sample Dataset

GATATATGCATATACTT
ATAT
Sample Output

2 4 10
Noteclick to collapse

Different programming languages use different notations for positions of symbols in strings. Above, we use 1-based numbering, as opposed to 0-based numbering, which is used in Python. For ss = "AUGCUUCAGAAAGGUCUUACG", 1-based numbering would state that s[1]s[1] = 'A' is the first symbol of the string, whereas this symbol is represented by s[0]s[0] in 0-based numbering. The idea of 0-based numbering propagates to substring indexing, so that s[2:5]s[2:5] becomes "GCUU" instead of "UGCU".

Note that in some programming languages, such as Python, s[j:k] returns only fragment from index jj up to but not including index kk, so that s[2:5] actually becomes "UGC", not "UGCU".
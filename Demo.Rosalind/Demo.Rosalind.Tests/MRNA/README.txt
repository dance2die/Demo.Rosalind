http://rosalind.info/problems/mrna/


Inferring mRNA from Protein solved by 3700
July 1, 2012, 8 p.m. by Rosalind Team Topics: Combinatorics
← →
Pitfalls of Reversing Translationclick to expand

Problem

For positive integers aa and nn, aa modulo nn (written amodnamodn in shorthand) is the remainder when aa is divided by nn. For example, 29mod11=729mod11=7 because 29=11×2+729=11×2+7.

Modular arithmetic is the study of addition, subtraction, multiplication, and division with respect to the modulo operation. We say that aa and bb are congruent modulo nn if amodn=bmodnamodn=bmodn; in this case, we use the notation a≡bmodna≡bmodn.

Two useful facts in modular arithmetic are that if a≡bmodna≡bmodn and c≡dmodnc≡dmodn, then a+c≡b+dmodna+c≡b+dmodn and a×c≡b×dmodna×c≡b×dmodn. To check your understanding of these rules, you may wish to verify these relationships for a=29a=29, b=73b=73, c=10c=10, d=32d=32, and n=11n=11.

As you will see in this exercise, some Rosalind problems will ask for a (very large) integer solution modulo a smaller number to avoid the computational pitfalls that arise with storing such large numbers.

Given: A protein string of length at most 1000 aa.

Return: The total number of different RNA strings from which the protein could have been translated, modulo 1,000,000. (Don't neglect the importance of the stop codon in protein translation.)

Sample Dataset

MA
Sample Output

12
Hintclick to collapse

What does it mean intuitively to take a number modulo 1,000,000?
http://rosalind.info/problems/orf/


Open Reading Frames solved by 2890
July 1, 2012, 8 p.m. by Rosalind Team Topics: Combinatorics
← →
Transcription May Begin Anywhereclick to expand

Problem

Either strand of a DNA double helix can serve as the coding strand for RNA transcription. Hence, a given DNA string implies six total reading frames, or ways in which the same region of DNA can be translated into amino acids: three reading frames result from reading the string itself, whereas three more result from reading its reverse complement.

An open reading frame (ORF) is one which starts from the start codon and ends by stop codon, without any other stop codons in between. Thus, a candidate protein string is derived by translating an open reading frame into amino acids until a stop codon is reached.

Given: A DNA string ss of length at most 1 kbp in FASTA format.

Return: Every distinct candidate protein string that can be translated from ORFs of ss. Strings can be returned in any order.

Sample Dataset

>Rosalind_99
AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG
Sample Output

MLLGSFRLIPKETLIQVAGSSPCNLS
M
MGMTPRLGLESLLE
MTPRLGLESLLE
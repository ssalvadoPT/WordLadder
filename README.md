# WordLadder

Word Ladder console application project.

This project intends to build a word ladder between two distinct words, of the same length.
Each step of the ladder must be a word with only one different character than the adjacent steps of the ladder.
All other characters must remain the same, and in the same position.

Ex: spin - spit - spot

Four arguments are to be given:

A path to a dictionary file
A start word
An end word
A path to a text file that will hold the solution

ie: WordLadder.exe "C:\Users\MyUserName\words-english.txt" "spin" "spot" "C:\Users\MyUserName\result.txt"



Explained Algorithm 
--------------------

In my algorithm explanation I refer a lot to "candidate" word. A candidate word is a word that is a possible combination to be a direct step (of the ladder) to another word.

So a word is candidate to another word, when it differs only in one character (the other characters must remain the same, and in the same position).

This is the requisit to be part of a ladder step. 

The objective of this solution was to find the shortest possible path.

The algorithm that allows me to know I have found the optimal path, is to iterate all possible combinations, and pick the one that has the least "steps".

The dictionary file is provided, so the first thing to do is to get rid of unneeded words that are only slowing performance, so I removed the words that aren't 4 characteres long, and the ones that weren't candidates to any word (these words can never be part of a word ladder, it would fall in the case of "No solution found").

To iterate all possible combinations, it means we must test all the candidates of all the candidates, until we reach the end word.

When we reach the end word, we must look back and find out what candidate lead me to the end word, and what candidate lead me to that candidate. Repeat theses steps until we get to the start word.

Reverse it, and we get our Word Ladder.

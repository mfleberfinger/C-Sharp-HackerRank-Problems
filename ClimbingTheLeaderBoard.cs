using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    public class Node {
        public int key;
        public bool red;
        public Node left;
        public Node right;
        public Node parent;
        // The number of nodes in this node's right subtree.
        // We track this so we can calculate rank.
        public int rightCount;
        // The number of nodes in this node's left subtree. We track this so
        // we can use it to update rightCounts when we are performing rotations
        // that move left and right subtrees around.
        public int leftCount;

        public Node(int key) {
            this.key = key;
            red = true;
            left = null;
            right = null;
            parent = null;
            rightCount = 0;
            leftCount = 0;
        }
    }

    public class RBTree {
        public Node root;

        public RBTree() {
            root = null;
        }

        // Insert a node and return its "rank" by adding up all of the nodes
        // into whose left subtrees it goes and all of the nodes in their right
        // subtrees.
        // Return the rank of the node we just inserted.
        public int Insert(int key) {
            Node current = root;
            Node parent = null;
            Node newNode = new Node(key);
            int rank = 1;

            while (current != null) {
                parent = current;
                if (key > current.key) {
                    // We're greater than the current node. Make its right count
                    // reflect that.
                    current.rightCount++;
                    current = current.right;
                }
                else if (key < current.key) {
                    // The current node and its right descendents are greater than us.
                    // Make our rank and its left count reflect that.
                    rank += current.rightCount + 1;
                    current.leftCount++;
                    current = current.left;
                }
                else
                    throw new InvalidOperationException("Key already exists in tree.");
            }

            if (parent == null) {
                // The root is always black.
                newNode.red = false;
                root = newNode;
            }
            else {
                newNode.parent = parent;
                if (key > parent.key)
                    parent.right = newNode;
                else
                    parent.left = newNode;
                
                // We now have a new red node in the tree.
                // Maintain the red-black rules.
                maintainBalance(newNode);
            }

            return rank;
        }

        // Search for the given key and return the rank of the node we find.
        // Return null if the key is not found.
        public int? Search(int key) {
            Node current = root;
            int? rank = 1;
            
            while (current != null && current.key != key) {
                if (key > current.key) {
                    current = current.right;
                }
                else {
                    rank += current.rightCount + 1;
                    current = current.left;
                }
            }

            // If we found the key, it means we tied with that score.
            // We still need to add up the number of scores above ours.
            if (current != null) {
                rank += current.rightCount;
            }
            // If we didn't find the key, we're not yet ranked.
            else {
                rank = null;
            }

            return rank;
        }

        private void maintainBalance(Node node) {
            Node uncle;
            // Starting at the given node, work our way up the tree until any
            // red-black tree rule violations are fixed.
            while (node != root && node.parent.red) {
                // If the node's grandparent is a left child.
                if (node.parent == node.parent.parent.left) {
                    uncle = node.parent.parent.right;
                    // Case 1:
                    //    The node's parent is red and its parent's sibling is red.
                    //    In this case, we just recolor. This will have no effect
                    //    on the nodes' rightCount values.
                    // If uncle is null, consider it black.
                    if (uncle?.red ?? false) {
                        node.parent.red = false;
                        uncle.red = false;
                        node.parent.parent.red = true;
                        node = node.parent.parent;
                    }
                    // Otherwise, we need to do more than just recolor.
                    else {
                        // Case 2:
                        //    The node's parent is red, the parent's sibling is black,
                        //    and the node's value is between the values of its
                        //    parent and grandparent.
                        if (node == node.parent.right) {
                            node = node.parent;
                            LeftRotate(node);
                        }
                        // Case 2 falls through to case 3 because the first part
                        // of the solution to case 2 is to turn it into case 3.

                        // Case 3:
                        //    Anything that isn't case 1 or 2.
                        node.parent.red = false;
                        node.parent.parent.red = true;
                        RightRotate(node.parent.parent);
                    }
                }
                // If the node's grandparent is a right child.
                // We handle the same cases regardless of whether the grandparent
                // is a left or right child but the member variables we modify are
                // different and we rotate in opposite directions.
                else {
                    uncle = node.parent.parent.left;
                    // Case 1:
                    if (uncle?.red ?? false) {
                        node.parent.red = false;
                        uncle.red = false;
                        node.parent.parent.red = true;
                        node = node.parent.parent;
                    }
                    else {
                        // Case 2
                        if (node == node.parent.left) {
                            node = node.parent;
                            RightRotate(node);
                        }
                        // Case 3
                        node.parent.red = false;
                        node.parent.parent.red = true;
                        LeftRotate(node.parent.parent);
                    }
                }
            }

            root.red = false;
        }

        // The rotation logic used here comes directly from the website
        // (https://brilliant.org/wiki/red-black-tree/) and should be correct.
        // In addition to the rotation, we must update of our "rightCount" values
        // as we move nodes around.
        private void LeftRotate(Node node) {
            // node will become risingChild's left child.
            // The parent of node will become risingChild's parent.
            // risingChild's left subtree will become node's right subtree.
            Node risingChild = node.right;
            node.right = risingChild.left;
            // We must update risingChild.leftCount and node.rightCount because
            // we are changing the number of nodes in those subtrees.
            node.rightCount = risingChild.leftCount;
            risingChild.leftCount = node.rightCount + node.leftCount + 1;
            if (risingChild.left != null)
                risingChild.left.parent = node;
            risingChild.parent = node.parent;
            if (node.parent == null) {
                root = risingChild;
            }
            else {
                if (node == node.parent.left)
                    node.parent.left = risingChild;
                else
                    node.parent.right = risingChild;
            }
            risingChild.left = node;
            node.parent = risingChild;
        }

        // This is not directly from the website. The website does not give an
        // implementation, pseudocode, or detailed explanation of this. It just
        // says "the right_rotate method is the exact same thing [as left_rotate]
        // in the other direction."
        // I'm assuming that just means "swap the references to 'left' and 'right'
        // that are in the LeftRotate method."
        private void RightRotate(Node node) {
            // node will become risingChild's right child.
            // The parent of node will become risingChild's parent.
            // risingChild's right subtree will become node's left subtree.
            Node risingChild = node.left;
            node.left = risingChild.right;
            // We must update risingChild.rightCount and node.leftCount because
            // we are changing the number of nodes in those subtrees.
            node.leftCount = risingChild.rightCount;
            risingChild.rightCount = node.rightCount + node.leftCount + 1;
            if (risingChild.right != null)
                risingChild.right.parent = node;
            risingChild.parent = node.parent;
            if (node.parent == null) {
                root = risingChild;
            }
            else {
                if (node == node.parent.right)
                    node.parent.right = risingChild;
                else
                    node.parent.left = risingChild;
            }
            risingChild.right = node;
            node.parent = risingChild; 
        }

        // Print values in the tree in ascending order.
        // "Left, current, right."
        public static void PrintInOrder(Node current) {
            if (current.left != null)
                PrintInOrder(current.left);
            Console.Write(current.key.ToString() + " ");
            if (current.right != null)
                PrintInOrder(current.right);
        }
    }
    /*
     * Complete the 'climbingLeaderboard' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY ranked
     *  2. INTEGER_ARRAY player
     */


    // For each node, store the number of children in its right subtree and update
    // whenever a larger value is added, while traversing the tree to insert.
    // Search the tree before attempting an insert, then do an insertion and
    // update counts if and only if the key doesn't already exist. Search and
    // insert should both return an integer: the rank of the searched or inserted
    // score. When traversing the tree during a search or insert, add up all of
    // the right-tree-counts of the nodes into whose left subtrees we descend.
    // This should give us a count of the nodes that have values greater than
    // the score we're inserting. Each time we enter a left subtree,
    // newRank += (number of nodes in right subtree) + 1. Each insertion would
    // still occur in O(lgk) time (where k is the number of nodes currently in
    // the tree) and the updates would take place during insertion, requiring no
    // extra traversal of the tree.
    
    // .NET has dictionary, set, and list structures that use binary
    // search trees internally but it doesn't have a balanced tree data
    // structure that can actually be used like a tree.
    // It seems like I will need to implement my own balanced binary search
    // tree for this.
    // If such a tree was available in .NET, it might not be usable anyway
    // because I need to modify the insert method of a typical red-black
    // tree to maintain the counts of nodes in each node's right subtree.
    
    // ranked: The leaderboard scores in descending order.
    // player: The player's scores in ascending order.
    // returns: The player's rank after each score in the player list.
    public static List<int> climbingLeaderboard(List<int> ranked, List<int> player)
    {
        List<int> ourRanks = new List<int>();
        RBTree tree = new RBTree();
        // Insert the ranked players. We don't care about the duplicates.
        foreach (int r in ranked) {
            if (!tree.Search(r).HasValue)
                Console.WriteLine(tree.Insert(r).ToString());
        }
        // Insert our new scores and get the rank each time.
        int? rank = null;
        foreach(int p in player) {
            rank = tree.Search(p);
            if (!rank.HasValue)
                rank = tree.Insert(p);
            ourRanks.Add(rank.Value);
        }
        
        return ourRanks;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int rankedCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> ranked = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(rankedTemp => Convert.ToInt32(rankedTemp)).ToList();

        int playerCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> player = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(playerTemp => Convert.ToInt32(playerTemp)).ToList();

        List<int> result = Result.climbingLeaderboard(ranked, player);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
